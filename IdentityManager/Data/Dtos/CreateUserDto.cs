using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityManager.Data.Dtos
{
    public class CreateUserDto
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}