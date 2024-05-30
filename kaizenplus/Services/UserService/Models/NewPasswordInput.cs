using System;
using System.ComponentModel.DataAnnotations;

namespace kaizenplus.Services.Users.Models
{
    public class NewPasswordInput
    {
        [Required]
        public string Password { get; set; }
        [Required]
        public Guid UserId { get; set; }
    }
}