using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using kaizenplus.Services.Users.Models;
using kaizenplus.Services.UserService;
using kaizenplus.Attributes;
using kaizenplus.Models;
using Microsoft.AspNetCore.Mvc;

namespace kaizenplus.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IUserService service;

        public AccountController(IUserService service)
        {
            this.service = service;
        }
        [AppAuthorize]
        [ValidateModel]
        [HttpPost("register")]
        [Produces(typeof(BaseResponse<Guid>))]
        public async Task<ActionResult<BaseResponse<Guid>>> Register([FromForm] UserRegisterInput input)
        {
            return Ok(await service.Register(input));
        }



        [AppAuthorize]
        [ValidateModel]
        [HttpPost("newPassword")]
        [Produces(typeof(BaseResponse))]
        public async Task<ActionResult<BaseResponse>> NewPassword([FromForm] NewPasswordInput input)
        {
            return Ok(await service.NewPassword(input));
        }

        [AppAuthorize]
        [ValidateModel]
        [HttpDelete("Delete/{Id}")]
        [Produces(typeof(BaseResponse))]
        public async Task<ActionResult<BaseResponse>> DeleteUser(Guid Id)
        {
            return Ok(await service.DeleteUser(Id));
        }
        [AppAuthorize]
        [ValidateModel]
        [HttpGet("GetUsers")]
        [Produces(typeof(BaseResponse<List<UserOutput>>))]
        public ActionResult<BaseResponse<List<UserOutput>>> GetUsers()
        {
            return Ok(service.GetUsers());
        }

        [ValidateModel]
        [HttpPost("authenticate")]
        [Produces(typeof(BaseResponse<UserAuthenticateOutput>))]
        public async Task<ActionResult<BaseResponse<UserAuthenticateOutput>>> Authenticate([FromForm] UserAuthenticateInput input)
        {
            return Ok(await service.Authenticate(input));
        }

        [ValidateModel]
        [HttpPost("refreshToken")]
        [Produces(typeof(BaseResponse<UserAuthenticateOutput>))]
        public async Task<ActionResult<BaseResponse<UserAuthenticateOutput>>> RefreshToken([FromForm] UserRefreshTokenInput input)
        {
            return Ok(await service.RefreshToken(input));
        }

    }
}