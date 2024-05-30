using kaizenplus.Domain.UserRoles;
using System;
using System.Collections.Generic;
using System.Linq;


namespace kaizenplus.Domain.Users
{
    public class User : BaseEntity<Guid>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public DateTime? LastLoginDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string RefreshToken { get; set; }
        public string Picture { get; set; }
        public DateTime? RefreshTokenValidUntil { get; set; }
        
        public bool Active { get; set; }
        public bool IsVerified { get; set; }
       

  
        public List<UserRole> UserRoles { get; set; }


        public void AddRole(Enums.Roles role)
        {
            var roleId = (int)role;

            if (UserRoles == null)
            {
                UserRoles = new List<UserRole>();
            }

            if (!UserRoles.Any(ur => ur.RoleId == roleId))
            {
                UserRoles.Add(new UserRole
                {
                    RoleId = roleId
                });
            }
        }

        public string GetFullName()
        {
            return $"{FirstName} {LastName}";
        }
    }
}