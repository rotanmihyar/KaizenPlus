using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using kaizenplus.Enums;
using kaizenplus.Models;

namespace kaizenplus.Attributes
{
    public class AppAuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        public Roles[] Roles { get; set; }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!context.HttpContext.User.Identity.IsAuthenticated)
            {
                context.Result = new UnauthorizedObjectResult(new BaseResponse(ErrorCode.Unauthorized));
            }

            if (Roles != null && Roles.Any())
            {
                if (!Roles.Any(role => context.HttpContext.User.IsInRole(role.ToString())))
                {
                    context.Result = new ForbiddenObejctResult(new BaseResponse(ErrorCode.Forbidden), 403);
                }
            }
        }
    }

    public class ForbiddenObejctResult : ObjectResult
    {
        public ForbiddenObejctResult(object value, int statusCode) : base(value)
        {
            StatusCode = statusCode;
        }
    }
}