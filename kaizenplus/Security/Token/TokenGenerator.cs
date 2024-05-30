using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using kaizenplus.Extensions;
using kaizenplus.Security.Token.Models;

namespace kaizenplus.Security.Token
{
    public class TokenGenerator : ITokenGenerator
    {
        private readonly TokenConfigurations configurations;

        public TokenGenerator(IOptions<TokenConfigurations> configurations)
        {
            this.configurations = configurations.Value;
        }

        public string Generate(GenerateTokenInput user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(configurations.Secret);

            var claims = user.Roles.Select(role => new Claim(ClaimTypes.Role, role)).ToList();
            claims.Add(new Claim("userId", user.Id.ToString()));
            claims.Add(new Claim(ClaimTypes.Name, string.IsNullOrEmpty(user.Name) ? "" : user.Name));
            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));

            if (user.CompanyId.HasValue)
            {
                claims.Add(new Claim("companyId", user.CompanyId.ToString()));
            }
            else
            {
                claims.Add(new Claim("companyId", ""));
            }

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                IssuedAt = DateTime.UtcNow,
                Expires = DateTime.UtcNow.AddMinutes(configurations.Validity),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public RefreshTokenOutput GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var randomNumberGenerator = RandomNumberGenerator.Create())
            {
                randomNumberGenerator.GetBytes(randomNumber);
                return new RefreshTokenOutput
                {
                    RefreshToken = Convert.ToBase64String(randomNumber),
                    ValidUntil = DateTimeExtension.SystemNow().AddMinutes(configurations.RefreshTokenValidity)
                };
            }
        }
    }
}