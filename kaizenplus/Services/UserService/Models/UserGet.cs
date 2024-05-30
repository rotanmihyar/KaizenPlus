using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

using kaizenplus.Enums;
using Coachyou.Services.Users.Models;
using kaizenplus.Domain.Users;
using kaizenplus.Models;
using kaizenplus.Security.Token.Models;


namespace Coachyou.Services.Users.Models
{
    public class UserOutput
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Picture { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime CreatedDate { get; set; }
        
        
    
        public List<LookupOutput<int>> Roles { get; set; }

        public UserOutput(User user)
        {
            if (user != null)
            {
                Id = user.Id;
                Username = user.Username;
                FirstName = user.FirstName;
                LastName = user.LastName;
                PhoneNumber = user.PhoneNumber;
                Email = user.Email;
            
                CreatedDate = user.CreatedDate;
                
                Picture = user.Picture;
               
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