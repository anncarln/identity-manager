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
    public class RegisterService(IMapper mapper, UserManager<User> userManager)
    {
        private IMapper _mapper = mapper;
        private UserManager<User> _userManager = userManager;
        
        public async Task RegisterAsync(CreateUserDto dto)
        {
            User user = _mapper.Map<User>(dto);

            IdentityResult result = await _userManager.CreateAsync(user, dto.Password);

            if (!result.Succeeded) 
            {
                throw new ApplicationException("Error registering user!");
            }
        }
    }
}