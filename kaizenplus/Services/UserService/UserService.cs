using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using kaizenplus.Services.Users.Models;
using kaizenplus.Services.UserService;
using Coachyou.Services.Users.Models;
using kaizenplus.DataAccess.UnitOfWorks;
using kaizenplus.Domain.UserRoles;
using kaizenplus.Domain.Users;
using kaizenplus.Enums;
using kaizenplus.Files;
using kaizenplus.Models;
using kaizenplus.Security;
using kaizenplus.Security.Token;
using kaizenplus.Security.Token.Models;
using Microsoft.EntityFrameworkCore;

namespace kaizenplus.Services.Users
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork unitOfWork;
       
        private readonly ISecurityManager securityManager;
        private readonly ITokenGenerator tokenGenerator;
        private readonly IFileManager fileManager;

        public UserService(
            IUnitOfWork unitOfWork,
            
            ISecurityManager securityManager,
            ITokenGenerator tokenGenerator,
            IFileManager fileManager
        
         )
        {
            this.unitOfWork = unitOfWork;
            
            this.securityManager = securityManager;
            this.tokenGenerator = tokenGenerator;
            this.fileManager = fileManager;
        
        }

     

        

    

        public async Task<BaseResponse<Guid>> Register(UserRegisterInput input)
        {
            try
            {
                var user = unitOfWork.UserRepository.FirstOrDefault(u => u.Email == input.Email);

               

                if (user == null)
                {

                    var userCount = unitOfWork.UserRepository.Where().Count();

                    user = new User
                    {
                        Username = input.Email,

                        CreatedDate = DateTime.Now,
                        Email = input.Email,
                        DateOfBirth = input.BirthDate,
                        FirstName = input.FirstName,
                        LastName = input.LastName,
                       
                        Active = true,
                        IsVerified=true,
                       
                       
                        
                        UserRoles = new List<UserRole>
                    {
                        new UserRole
                        {
                            RoleId = (int)input.UserRole
                        }
                    }
                    };

                    unitOfWork.UserRepository.Create(user);
                    if (input.Picture != null && input.Picture.Length >= 1)
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            input.Picture.CopyTo(memoryStream);
                            var fileBytes = memoryStream.ToArray();

                            var extension = input.Picture.FileName.Split('.').Last();

                            var fileName = fileManager.Save(fileBytes, extension);

                            user.Picture = fileName;
                        }
                    }
                }
                else
                {
                    user.Username = input.Email;
                    user.PhoneNumber = input.PhoneNumber;
                    user.Email = input.Email;
                    user.DateOfBirth = input.BirthDate;
                    user.FirstName = input.FirstName;
                    user.LastName = input.LastName;
                   
                }
                if (input.PhoneNumber != null)
                {
                    user.PhoneNumber = input.PhoneNumber;
                }
                else
                {
                    user.PhoneNumber = " ";
                }
                CreatePasswordHash(input.Password, user);

               

                await unitOfWork.SaveAsync();
       
                return new BaseResponse<Guid>(user.Id);
            }
            catch (Exception ex) { return new BaseResponse<Guid>(Guid.NewGuid(), ErrorCode.BadRequest, ex.Message); }
        }
        /// <summary>
        /// Changes the password for the specified user.
        /// </summary>
        public async Task<BaseResponse> DeleteUser(Guid Id)
        {
            var userId = securityManager.GetUserId();

            var user = unitOfWork.UserRepository.FirstOrDefault(u => u.Id != userId && u.Id==Id);

            if (user == null)
            {
                return new BaseResponse<UserAuthenticateOutput>(null, ErrorCode.UserNotFound);
            }
            user.Active = false;
            unitOfWork.UserRepository.Update(user);
            await unitOfWork.SaveAsync();

            return new BaseResponse();
        }
        public BaseResponse<List<UserOutput>> GetUsers()
        {
            var userId = securityManager.GetUserId();

            var user = unitOfWork.UserRepository.Where(u => u.Id != userId && u.Active == true).Include(x => x.UserRoles).ThenInclude(x=>x.Role);

            return new BaseResponse<List<UserOutput>>(user.Select(x => new UserOutput(x)).ToList());

        }
        public async Task<BaseResponse> NewPassword(NewPasswordInput input)
        {
            var userId = input.UserId;

            var user = unitOfWork.UserRepository.FirstOrDefault(u => u.Id == userId);

            if (user == null)
            {
                return new BaseResponse<UserAuthenticateOutput>(null, ErrorCode.UserNotFound);
            }

            CreatePasswordHash(input.Password, user);
            
            await unitOfWork.SaveAsync();

            return new BaseResponse();
        }

        public async Task<BaseResponse<UserAuthenticateOutput>> Authenticate(UserAuthenticateInput input)
        {
            var user = unitOfWork.UserRepository.FirstOrDefault(u => u.Username == input.Username, (query) =>
            {
                return query.Include(u => u.UserRoles).ThenInclude(ur => ur.Role);
            });
            if (user == null)
                return new BaseResponse<UserAuthenticateOutput>(null, ErrorCode.InvalidPassword);
            if (!VerifyPasswordHash(input.Password, user.PasswordHash, user.PasswordSalt))
                return new BaseResponse<UserAuthenticateOutput>(null, ErrorCode.InvalidPassword);

            
            unitOfWork.UserRepository.Update(user);
            await unitOfWork.SaveAsync();
            return await AuthenticateUser(user);
        }
        private async Task<BaseResponse<UserAuthenticateOutput>> AuthenticateUser(User user)
        {
          

            if (!user.Active)
            {
                return new BaseResponse<UserAuthenticateOutput>(null, ErrorCode.UserInactive);
            }

           
            var output = new UserAuthenticateOutput(user)
            {
                Token = tokenGenerator.Generate(new GenerateTokenInput
                {
                    Id = user.Id,
                    Name = user.FirstName+user.LastName,
                  
                    Roles = user.UserRoles.Select(userRole => userRole.Role.Name).ToList()
                }),
                
                RefreshToken = tokenGenerator.GenerateRefreshToken()
            };


            user.RefreshToken = output.RefreshToken.RefreshToken;
            user.RefreshTokenValidUntil = output.RefreshToken.ValidUntil;

            await unitOfWork.SaveAsync();

            return new BaseResponse<UserAuthenticateOutput>(output);
        }


        public async Task<BaseResponse<UserAuthenticateOutput>> RefreshToken(UserRefreshTokenInput input)
        {
            var user = unitOfWork.UserRepository.FirstOrDefault(u => u.RefreshToken == input.RefreshToken, (query) =>
            {
                return query.Include(u => u.UserRoles).ThenInclude(ur => ur.Role);
            });

            if (user == null)
                return new BaseResponse<UserAuthenticateOutput>(null, ErrorCode.Unauthorized);

            if (!user.RefreshTokenValidUntil.HasValue || user.RefreshTokenValidUntil < DateTime.Now)
            {
                return new BaseResponse<UserAuthenticateOutput>(null, ErrorCode.Unauthorized);
            }

            return await AuthenticateUser(user);
        }

        public async Task<BaseResponse<Guid>> Create(UserCreateInput input)
        {
            var user = unitOfWork.UserRepository.FirstOrDefault(u => u.Email == input.Email);

            if (user != null)
            {
                return new BaseResponse<Guid>(Guid.Empty, ErrorCode.UsernameTaken);
            }

            user = new User
            {
                Username = input.Email,
                PhoneNumber = input.PhoneNumber,
                CreatedDate = DateTime.Now,
                Email = input.Email,
                DateOfBirth = input.DateOfBirth,
                FirstName = input.FirstName,
                LastName = input.LastName,
               
                Active = true,
               
                UserRoles = new List<UserRole>
                    {
                        new UserRole
                        {
                            RoleId = (int)input.UserRole
                        }
                    }
            };

            if (input.IsAdmin)
            {

                user.UserRoles.Add(new UserRole
                {
                    RoleId = (int)Roles.Admin
                });
            }

            CreatePasswordHash(input.Password, user);

            unitOfWork.UserRepository.Create(user);

            await unitOfWork.SaveAsync();

            return new BaseResponse<Guid>(user.Id);
        }

        public BaseResponse<UserOutput> Get(Guid id)
        {
            var user = GetUser(id);

            return new BaseResponse<UserOutput>(new UserOutput(user));
        }

        public BaseResponse<UserOutput> Get(string PhoneNumber)
        {
            var user = GetUser(PhoneNumber);
            if (user == null)
            {
                return new BaseResponse<UserOutput>(null, ErrorCode.UserNotFound);
            }
            return new BaseResponse<UserOutput>(new UserOutput(user));
        }

      

        
        private User GetUser(Guid id)
        {
            var user = unitOfWork.UserRepository.FirstOrDefault(c => c.Id == id);

            if (user == null)
            {
                throw new AppException(ErrorCode.UserNotFound);
            }

            return user;
        }

        private User GetUser(string PhoneNumber)
        {
            var user = unitOfWork.UserRepository.FirstOrDefault(u => u.PhoneNumber == PhoneNumber);

            //if (user == null)
            //{
            //    throw new AppException(ErrorCode.UserNotFound);
            //}

            return user;
        }



        private void CreatePasswordHash(string password, User user)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                user.PasswordSalt = hmac.Key;
                user.PasswordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (storedHash.Length != 64) throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
            if (storedSalt.Length != 128) throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordHash");

            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }

            return true;
        }

      

     

    }
}