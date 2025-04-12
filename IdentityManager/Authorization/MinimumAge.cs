using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace IdentityManager.Authorization
{
    public class MinimumAge(int age) : IAuthorizationRequirement
    {
        public int Age { get; set; } = age;
    }
}