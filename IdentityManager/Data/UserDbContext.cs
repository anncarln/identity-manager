using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using IdentityManager.Models;
using Microsoft.EntityFrameworkCore;

namespace IdentityManager.Data
{
    public class UserDbContext(DbContextOptions<UserDbContext> options) : IdentityDbContext<User>(options)
    {
        
    }
}