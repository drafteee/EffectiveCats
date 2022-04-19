﻿using Infrastructure.Interfaces;
using Infrastructure.Models;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Identity;
using Infrastructure;
using BL.Interfaces;
using DAL.Interfaces.Finder;
using DAL.Interfaces.Repositories;
using DAL.Interfaces;
using DAL.Models.Account;
using BL.Exceptions;

namespace Domain.Services
{
    public class UserService : IUserService
    {
        private IUserFinder _finder;
        private IUserRepository _repository;
        private IUnitOfWork _unitOfWork;
        private IJWTUtils _jwtUtils;
        private readonly JWTSettings _appSettings;
        private readonly UserManager<User> _userManager;
        private readonly UserAccessor _userAccessor;

        public UserService(
            IUserFinder finder, IUserRepository repository, IUnitOfWork unitofWork,
            IJWTUtils jwtUtils,
            IOptions<JWTSettings> appSettings,
            UserManager<User> userManager,
            UserAccessor userAccessor)
        {
            _finder = finder;
            _repository = repository;
            _unitOfWork = unitofWork;

            _jwtUtils = jwtUtils;
            _appSettings = appSettings.Value;
            _userManager = userManager;
            _userAccessor = userAccessor;
        }

        public async Task<AuthenticateResponse> Authenticate(string userName, string password, string ipAddress)
        {
            var user = await _finder.GetAsync(x => x.UserName == userName);
            
            if (user == null || !(await _userManager.CheckPasswordAsync(user, password)))
                throw new AppException("Username or password is incorrect");

            var jwtToken = _jwtUtils.GenerateJwtToken(user.Id.ToString());
            _userAccessor.SetToken(jwtToken);

            var tokenIsUnique = false;

            while(!tokenIsUnique)
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
            _unitOfWork.Complete();
            return new AuthenticateResponse(user, jwtToken, refreshToken.Token);
        }

        public async Task<AuthenticateResponse> RefreshToken(long id, string ipAddress)
        {
            var user = await _finder.GetAsync(x => x.Id == id);
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
                _unitOfWork.Complete();
            }

            if (!refreshToken.IsActive)
                throw new AppException("Invalid token");

            var newRefreshToken = RotateRefreshToken(refreshToken, ipAddress);
            user.RefreshTokens.Add(newRefreshToken);

            RemoveOldRefreshTokens(user);

            _repository.Edit(user);
            await _unitOfWork.Complete();

            var jwtToken = _jwtUtils.GenerateJwtToken(user.Id.ToString());
            _userAccessor.SetToken(jwtToken);
            return new AuthenticateResponse(user, jwtToken, newRefreshToken.Token);
        }

        public async Task<bool> RevokeToken(string token, string ipAddress)
        {
            var user = await GetUserByRefreshToken(token);
            var refreshToken = user.RefreshTokens.Single(x => x.Token == token);

            if (!refreshToken.IsActive)
                throw new AppException("Invalid token");

            RevokeRefreshToken(refreshToken, ipAddress, "Revoked without replacement");
            _repository.Edit(user);
            await _unitOfWork.Complete();
            return true;
        }


        private async Task<User> GetUserByRefreshToken(string token)
        {
            var user = await _finder.GetAsync(u => u.RefreshTokens.Any(t => t.Token == token));

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