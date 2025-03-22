using System;
using System.Collections.Generic;
using AutoMapper;
using System.Linq;
using System.Threading.Tasks;
using IdentityManager.Data.Dtos;
using IdentityManager.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using IdentityManager.Services;

namespace IdentityManager.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class UserController(RegisterService registerService) : Controller
    {
        private readonly RegisterService _registerService = registerService;

        [HttpPost]
        public async Task<IActionResult> RegisterUserAsync(CreateUserDto dto)
        {
            await _registerService.RegisterAsync(dto);
            return Ok("User successfully registered!");
        }
    }
}