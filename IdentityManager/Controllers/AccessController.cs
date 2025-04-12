using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IdentityManager.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class AccessController : ControllerBase
    {
        [HttpGet]
        // [Authorize(Policy = "MinimumAge")]
        public IActionResult Get()
        {
            return Ok("Access granted");
        }
    }
}