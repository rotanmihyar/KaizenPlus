using System;
using System.ComponentModel.DataAnnotations;
using kaizenplus.Enums;
using Microsoft.AspNetCore.Http;

namespace kaizenplus.Services.Users.Models
{
    public class UserRegisterInput
    {
     
        public string PhoneNumber { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }

        [Required]

       
        public Roles UserRole { get; set; }
        [Required]
        public DateTime BirthDate { get; set; }

    
        [Required]
        public string Email { get; set; }
      
        public IFormFile Picture { get; set; }
    }
}