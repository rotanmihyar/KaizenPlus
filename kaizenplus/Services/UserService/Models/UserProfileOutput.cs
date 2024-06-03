using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

using kaizenplus.Enums;
using kaizenplus.Services.Users.Models;
using kaizenplus.Domain.Users;
using kaizenplus.Models;
using kaizenplus.Security.Token.Models;


namespace kaizenplus.Services.Users.Models
{
    public class UserProfileOutput
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Picture { get; set; }
       
        public DateTime? DateOfBirth { get; set; }
        public DateTime? LastUpdateDate { get; set; }
        public DateTime CreatedDate { get; set; }
      
        public string InvitationCode { get; set; }
        public bool AllowNotifications { get; set; }
        public bool IsOnline { get; set; }
        public bool HasDefaultLocation { get; set; }
        public List<LookupOutput<int>> Roles { get; set; }

        public UserProfileOutput(User user, bool hasDefaultLocation = false)
        {
            if (user != null)
            {
                Id = user.Id;
                Username = user.Username;
                FirstName = user.FirstName;
                LastName = user.LastName;
                PhoneNumber = user.PhoneNumber;
                Email = user.Email;
                
                DateOfBirth = user.DateOfBirth;
                CreatedDate = user.CreatedDate;
              
                Picture = user.Picture;
             
                HasDefaultLocation = hasDefaultLocation;
              
                Roles = new List<LookupOutput<int>>();

                if (user.UserRoles != null && user.UserRoles.Any())
                {
                    Roles = user.UserRoles.Select(ur => new LookupOutput<int>
                    {
                        Id = ur.RoleId,
                        Name = ur.Role?.Name
                    }).ToList();
                }
            }
        }
    }
}