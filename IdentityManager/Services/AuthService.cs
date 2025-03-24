using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using IdentityManager.Data.Dtos;
using IdentityManager.Models;
using Microsoft.AspNetCore.Identity;

namespace IdentityManager.Services
{
    public class AuthService(IMapper mapper, SignInManager<User> signInManager, ILogger<AuthService> logger)
    {
        private IMapper _mapper = mapper;
        private SignInManager<User> _signInManager = signInManager;
        private readonly ILogger<AuthService> _logger = logger;

        public async Task LoginAsync(LoginUserDto loginUserDto)
        {
            var result = await _signInManager.PasswordSignInAsync(loginUserDto.Username, loginUserDto.Password, false, false);

            if (!result.Succeeded)
            {
                if (result.IsLockedOut)
                {
                    _logger.LogWarning("User {Username} is locked out.", loginUserDto.Username);
                    throw new ApplicationException("Your account is locked. Please try again later.");
                }

                if (result.IsNotAllowed)
                {
                    _logger.LogWarning("Login not allowed for user {Username}.", loginUserDto.Username);
                    throw new ApplicationException("Login not allowed. Please make sure your email is confirmed or check for account restrictions.");
                }

                if (result.RequiresTwoFactor)
                {
                    _logger.LogWarning("Two-factor authentication required for {Username}.", loginUserDto.Username);
                    throw new ApplicationException("This login requires two-factor authentication.");
                }

                _logger.LogWarning("Invalid login attempt for {Username}.", loginUserDto.Username);
                throw new ApplicationException("Invalid username or password.");
            }
        }
    }
}