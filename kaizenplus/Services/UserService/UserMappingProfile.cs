using AutoMapper;
using kaizenplus.Services.Users.Models;
using Coachyou.Services.Users.Models;
using kaizenplus.Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kaizenplus.Services.UserService
{
  
        public class UserMappingProfile : Profile
        {
            public UserMappingProfile()
            {
                CreateMap<User, UserOutput>();

               

                CreateMap<User, UserAuthenticateOutput>()
                    .ForMember(dest => dest.RefreshToken, act => act.Ignore());

                CreateMap<UserCreateInput, User>();
            }
        }
}
