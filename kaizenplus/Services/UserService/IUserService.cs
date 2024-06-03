using kaizenplus.Services.Users.Models;
using kaizenplus.Services.Users.Models;
using kaizenplus.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kaizenplus.Services.UserService
{
    public interface IUserService
    {
        Task<BaseResponse<Guid>> Register(UserRegisterInput input);
        Task<BaseResponse> NewPassword(NewPasswordInput input);
        Task<BaseResponse<UserAuthenticateOutput>> RefreshToken(UserRefreshTokenInput input);
        Task<BaseResponse<Guid>> Create(UserCreateInput input);
        BaseResponse<UserOutput> Get(Guid id);
        Task<BaseResponse<UserAuthenticateOutput>> Authenticate(UserAuthenticateInput input);
        BaseResponse<List<UserOutput>> GetUsers();
        Task<BaseResponse> DeleteUser(Guid Id);
    }
}
