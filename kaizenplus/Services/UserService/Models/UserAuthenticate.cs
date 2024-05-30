using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

using kaizenplus.Enums;
using Coachyou.Services.Users.Models;
using kaizenplus.Domain.Users;
using kaizenplus.Security.Token.Models;

namespace kaizenplus.Services.Users.Models
{
    public class UserAuthenticateInput
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

       

    }

    public class UserAuthenticateOutput : UserOutput
    {
        public string Token { get; set; }
        public RefreshTokenOutput RefreshToken { get; set; }

        public UserAuthenticateOutput(User user)
            :base(user)
        {
        }
    }
}