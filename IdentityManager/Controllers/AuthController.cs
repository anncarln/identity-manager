using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityManager.Data.Dtos;
using IdentityManager.Services;
using Microsoft.AspNetCore.Mvc;

namespace IdentityManager.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class AuthController(AuthService authService) : ControllerBase
    {
        private readonly AuthService _authService = authService;
        
        [HttpPost]
        public async Task<IActionResult> LoginAsync(LoginUserDto dto)
        {
            var token = await _authService.GenerateTokenAsync(dto);
            return Ok(token);
        }
    }
}