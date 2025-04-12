using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using IdentityManager.Data.Dtos;
using IdentityManager.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace IdentityManager.Services
{
    public class AuthService(SignInManager<User> signInManager, 
        ILogger<AuthService> logger,
        IConfiguration config)
    {
        private readonly SignInManager<User> _signInManager = signInManager;
        private readonly ILogger<AuthService> _logger = logger;
        private readonly string _secretKey = config["JwtSettings:SecretKey"]
        ?? throw new ArgumentNullException("SecretKey not found in configuration");

        public async Task<string> GenerateTokenAsync(LoginUserDto loginUserDto)
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

            var user = _signInManager
                .UserManager
                .Users
                .FirstOrDefault(user => user.NormalizedUserName == loginUserDto.Username.ToUpper());

            if (user == null)
            {
                _logger.LogWarning("User {Username} not found.", loginUserDto.Username);
                throw new ApplicationException("User not found.");
            }

            var token = GenerateJwt(user);

            return token;
        }

        private string GenerateJwt(User user)
        {
            Claim[] claims =
            [
                new Claim("username", user.UserName ?? throw new ArgumentNullException(nameof(user.UserName))),
                new Claim("id", user.Id ?? throw new ArgumentNullException(nameof(user.Id))),
                new Claim(ClaimTypes.DateOfBirth, user.DateOfBirth.ToString()),
            ];

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));

            var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: signingCredentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}