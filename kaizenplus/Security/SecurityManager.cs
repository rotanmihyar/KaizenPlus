using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using kaizenplus.Enums;

namespace kaizenplus.Security
{
    public class SecurityManager : ISecurityManager
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public SecurityManager(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        public Guid GetUserId()
        {
            var userIdClaim = httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "userId");

            return new Guid(userIdClaim.Value);
        }
        public List<string> GetUserRoles()
        {
            var roles = httpContextAccessor.HttpContext.User.Claims.Where(c => c.Type == ClaimTypes.Role);

            return roles.Select(r => r.Value).ToList();
        }

        public bool HasRole(params Roles[] roles)
        {
            var userRoles = GetUserRoles();

            if (roles == null || roles.Length < 1)
            {
                return true;
            }

            foreach (var role in roles)
            {
                if (userRoles.Any(r => r == role.ToString()))
                {
                    return true;
                }
            }

            return false;
        }
    }
}