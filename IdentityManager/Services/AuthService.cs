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
    public class AuthService(IMapper mapper, SignInManager<User> signInManager)
    {
        private IMapper _mapper = mapper;
        private SignInManager<User> _signInManager = signInManager;

        public async Task LoginAsync(LoginUserDto loginUserDto)
        {
            var result = await _signInManager.PasswordSignInAsync(loginUserDto.Username, loginUserDto.Password, false, false);

            if (!result.Succeeded)
            {
                throw new ApplicationException("Invalid login attempt!");
            }
        }
    }
}