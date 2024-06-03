using System;
using System.ComponentModel.DataAnnotations;

using kaizenplus.Enums;

namespace kaizenplus.Services.Users.Models
{
    public class UserCreateInput
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public Roles UserRole { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }

   
        [Required]
        public string Password { get; set; }

        public bool IsAdmin { get; set; }
    }
}