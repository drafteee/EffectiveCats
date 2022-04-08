using Domain.Exceptions;
using Domain.Interfaces;
using Domain.Models.Account;
using Domain.Repositories;
using Infrastructure.Interfaces;
using Infrastructure.Models;
using Microsoft.Extensions.Options;
using BCrypt.Net;
using Microsoft.AspNetCore.Identity;

namespace Domain.Services
{
    public class UserService : IUserService
    {
        private IJWTUtils _jwtUtils;
        private readonly JWTSettings _appSettings;
        private readonly UserManager<User> _userManager;

        private UserRepository _repository;
        public UserRepository Repository;

        public UserService(
            MainContext context,
            IJWTUtils jwtUtils,
            IOptions<JWTSettings> appSettings,
            UserManager<User> userManager)
        {
            Repository = new UserRepository(context);
            _jwtUtils = jwtUtils;
            _appSettings = appSettings.Value;
            _userManager = userManager;
        }

        public async Task<AuthenticateResponse> Authenticate(string userName, string password, string ipAddress)
        {
            var user = await Repository.FindByFirstOrDefaultAsync(x => x.UserName == userName);
            
            if (user == null || !(await _userManager.CheckPasswordAsync(user, password)))
                throw new AppException("Username or password is incorrect");

            var jwtToken = _jwtUtils.GenerateJwtToken(user.Id.ToString());

            var tokenIsUnique = false;

            while(!tokenIsUnique)
                tokenIsUnique = !Repository.Any(u => u.RefreshTokens.Any(t => t.Token == jwtToken));

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

            await Repository.EditAndSaveAsync(user);

            return new AuthenticateResponse(user, jwtToken, refreshToken.Token);
        }

        public async Task<AuthenticateResponse> RefreshToken(string token, string ipAddress)
        {
            var user = await GetUserByRefreshToken(token);
            var refreshToken = user.RefreshTokens.Single(x => x.Token == token);

            if (refreshToken.IsRevoked)
            {
                RevokeDescendantRefreshTokens(refreshToken, user, ipAddress, $"Attempted reuse of revoked ancestor token: {token}");
                await Repository.EditAndSaveAsync(user);
            }

            if (!refreshToken.IsActive)
                throw new AppException("Invalid token");

            var newRefreshToken = RotateRefreshToken(refreshToken, ipAddress);
            user.RefreshTokens.Add(newRefreshToken);

            RemoveOldRefreshTokens(user);

            await Repository.EditAndSaveAsync(user);

            var jwtToken = _jwtUtils.GenerateJwtToken(user.Id.ToString());

            return new AuthenticateResponse(user, jwtToken, newRefreshToken.Token);
        }

        public async Task<bool> RevokeToken(string token, string ipAddress)
        {
            var user = await GetUserByRefreshToken(token);
            var refreshToken = user.RefreshTokens.Single(x => x.Token == token);

            if (!refreshToken.IsActive)
                throw new AppException("Invalid token");

            RevokeRefreshToken(refreshToken, ipAddress, "Revoked without replacement");
            await Repository.EditAndSaveAsync(user);
            return true;
        }

        public async Task<User> GetById(long id)
        {
            var user = await Repository.FindByAsync(x=> x.Id == id);
            if (user == null) throw new KeyNotFoundException("User not found");
            return user;
        }
        private async Task<User> GetUserByRefreshToken(string token)
        {
            var user = await Repository.FindByFirstOrDefaultAsync(u => u.RefreshTokens.Any(t => t.Token == token));

            if (user is null)
                throw new AppException("Invalid token");

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

        private void RemoveOldRefreshTokens(User user)
        {
            user.RefreshTokens.RemoveAll(x =>
                !x.IsActive &&
                x.Created.AddDays(_appSettings.RefreshTokenTTL) <= DateTime.UtcNow);
        }

        private void RevokeDescendantRefreshTokens(RefreshToken refreshToken, User user, string ipAddress, string reason)
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
