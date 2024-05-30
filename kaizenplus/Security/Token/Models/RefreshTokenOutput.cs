using System;

namespace kaizenplus.Security.Token.Models
{
    public class RefreshTokenOutput
    {
        public string RefreshToken { get; set; }
        public DateTime ValidUntil { get; set; }
    }
}