using System;
using System.Collections.Generic;
using AutoMapper;
using System.Linq;
using System.Threading.Tasks;
using IdentityManager.Data.Dtos;
using IdentityManager.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IdentityManager.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class UserController(IMapper mapper, UserManager<User> userManager) : Controller
    {
        private IMapper _mapper = mapper;
        private UserManager<User> _userManager = userManager;

        [HttpPost]
        public async Task<IActionResult> RegisterUser(CreateUserDto dto)
        {
            User user = _mapper.Map<User>(dto);

            IdentityResult result = await _userManager.CreateAsync(user, dto.Password);

            if (result.Succeeded) 
            {
                return Ok("User successfully registered!");
            }

            throw new ApplicationException("Error registering user!");
        }
    }
}