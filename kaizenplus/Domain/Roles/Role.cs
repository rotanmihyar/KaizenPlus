using System.Collections.Generic;
using System.Linq;
using kaizenplus.Domain.UserRoles;
using kaizenplus.Domain.Users;

namespace kaizenplus.Domain.Roles
{
    public class Role : BaseEntity<int>
    {
        public string Name { get; set; }

        public List<UserRole> UserRoles { get; set; }

        public void AddUser(User user)
        {
            if (UserRoles == null)
            {
                UserRoles = new List<UserRole>();
            }

            if (!UserRoles.Any(userRole => userRole.UserId == user.Id))
            {
                UserRoles.Add(new UserRole
                {
                    RoleId = Id,
                    UserId = user.Id
                });
            }
        }
    }
}