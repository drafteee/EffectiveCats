using BL.Finders;
using BL.Repositories;
using Domain.Entities.Account;
using Domain.Entitties.Account;
using Infrastructure;
using Infrastructure.Models;
using MediatR.Interfaces;
using MediatR.Models;
using MediatRL.Models;
using Microsoft.Extensions.Options;

namespace BLL.Services
{
    internal class UserMongoService : IUserService
    {
        private IUserMongoFinder _finder;
        private IUserRepository _repository;
        private IJWTUtils _jwtUtils;
        private JWTSettings _appSettings;
        private UserAccessor _userAccessor;

        public UserMongoService(
            IUserMongoFinder finder, IUserRepository repository,
            IJWTUtils jwtUtils,
            IOptions<JWTSettings> appSettings,
            UserAccessor userAccessor)
        {
            _finder = finder;
            _repository = repository;
            _jwtUtils = jwtUtils;
            _appSettings = appSettings.Value;
            _userAccessor = userAccessor;
        }

        private string GenerateHash(string password)
        {
            return null;
        }

        private async Task<bool> CheckPassword(UserMongo user, string password) => true;

        public RegisterResponse Register(string userName, string password)
        {
            var passwordHash = GenerateHash(password);

            var user = new UserMongo()
            {
                PasswordHash = passwordHash,
                UserName = userName,
            };

            _repository.Add(user);
            return new RegisterResponse();
        }

        public async Task<AuthenticateResponse> Authenticate(string userName, string password, string ipAddress)
        {
            var user = await _finder.GetByName(userName);

            if (user == null || !(await CheckPassword(user, password)))
                throw new UnauthorizedAccessException("Username or password is incorrect");

            var jwtToken = _jwtUtils.GenerateJwtToken(user.Id.ToString());
            _userAccessor.SetToken(jwtToken);

            var tokenIsUnique = false;

            while (!tokenIsUnique)
                tokenIsUnique = !_finder.TokenIsUnique(jwtToken);

            var refreshTokenString = _jwtUtils.GenerateRefreshToken();

            var refreshToken = new RefreshToken
            {
                Token = refreshTokenString,
                Expires = DateTime.UtcNow.AddDays(_appSettings.RefreshTokenTTL),
                Created = DateTime.UtcNow,
                CreatedByIp = ipAddress
            };

            user.RefreshTokens.Add(refreshToken);

            RemoveOldRefreshTokens(user);

            _repository.Edit(user);
            return new AuthenticateResponse(user.Id, user.UserName, jwtToken, refreshToken.Token);
        }

        public async Task<AuthenticateResponse> RefreshToken(long id, string ipAddress)
        {
            var user = await _finder.GetById(id);
            if (user == null) throw new KeyNotFoundException("User not found");

            var refreshToken = user.RefreshTokens.FirstOrDefault(x => x.Id == id && x.IsActive);
            if (refreshToken == null)
            {
                throw new KeyNotFoundException("Invalid Token");
            }

            user = await GetUserByRefreshToken(refreshToken.Token);
            refreshToken = user.RefreshTokens.Single(x => x.Token == refreshToken.Token);

            if (refreshToken.IsRevoked)
            {
                RevokeDescendantRefreshTokens(refreshToken, user, ipAddress, $"Attempted reuse of revoked ancestor token: {refreshToken.Token}");
                _repository.Edit(user);
            }

            if (!refreshToken.IsActive)
                throw new UnauthorizedAccessException("Invalid token");

            var newRefreshToken = RotateRefreshToken(refreshToken, ipAddress);
            user.RefreshTokens.Add(newRefreshToken);

            RemoveOldRefreshTokens(user);

            _repository.Edit(user);

            var jwtToken = _jwtUtils.GenerateJwtToken(user.Id.ToString());
            _userAccessor.SetToken(jwtToken);
            return new AuthenticateResponse(user.Id, user.UserName, jwtToken, newRefreshToken.Token);
        }

        public async Task<bool> RevokeToken(string token, string ipAddress)
        {
            var user = await GetUserByRefreshToken(token);
            var refreshToken = user.RefreshTokens.Single(x => x.Token == token);

            if (!refreshToken.IsActive)
                throw new UnauthorizedAccessException("Invalid token");

            RevokeRefreshToken(refreshToken, ipAddress, "Revoked without replacement");
            _repository.Edit(user);
            return true;
        }

        private async Task<UserMongo> GetUserByRefreshToken(string token)
        {
            var user = await _finder.GetByRefreshToken(token);

            if (user is null)
                throw new UnauthorizedAccessException("Invalid token");

            return user;
        }

        private RefreshToken RotateRefreshToken(RefreshToken refreshToken, string ipAddress)
        {
            var newRefreshTokenString = _jwtUtils.GenerateRefreshToken();

            var newRefreshToken = new RefreshToken
            {
                Token = newRefreshTokenString,
                Expires = DateTime.UtcNow.AddDays(_appSettings.RefreshTokenTTL),
                Created = DateTime.UtcNow,
                CreatedByIp = ipAddress
            };
            RevokeRefreshToken(refreshToken, ipAddress, "Replaced by new token", newRefreshToken.Token);
            return newRefreshToken;
        }

        private void RemoveOldRefreshTokens(UserMongo user)
        {
            user.RefreshTokens.RemoveAll(x =>
                !x.IsActive &&
                x.Created.AddDays(_appSettings.RefreshTokenTTL) <= DateTime.UtcNow);
        }

        private void RevokeDescendantRefreshTokens(RefreshToken refreshToken, UserMongo user, string ipAddress, string reason)
        {
            if (!string.IsNullOrEmpty(refreshToken.ReplacedByToken))
            {
                var childToken = user.RefreshTokens.SingleOrDefault(x => x.Token == refreshToken.ReplacedByToken);
                if (childToken.IsActive)
                    RevokeRefreshToken(childToken, ipAddress, reason);
                else
                    RevokeDescendantRefreshTokens(childToken, user, ipAddress, reason);
            }
        }
        private void RevokeRefreshToken(RefreshToken token, string ipAddress, string reason = null, string replacedByToken = null)
        {
            token.Revoked = DateTime.UtcNow;
            token.RevokedByIp = ipAddress;
            token.ReasonRevoked = reason;
            token.ReplacedByToken = replacedByToken;
        }
    }
}
