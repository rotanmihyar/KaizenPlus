using System.Net;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using kaizenplus.Models;
using Serilog;

namespace kaizenplus.Middlewares
{
    public static class ExceptionMiddleware
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();

                    if (contextFeature != null)
                    {
                        Log.Error(contextFeature.Error, contextFeature.Error.Message);

                        if (contextFeature.Error.GetType() == typeof(AppException))
                        {
                            context.Response.StatusCode = (int)HttpStatusCode.OK;
                            context.Response.ContentType = "application/json";

                            var exception = contextFeature.Error as AppException;

                            await context.Response.WriteAsync(new BaseResponse(exception.ErrorCode).ToString());
                        }
                        else
                        {
                            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                            await context.Response.WriteAsync(new BaseResponse(ErrorCode.ServerError, contextFeature.Error.Message).ToString());
                        }
                    }
                });
            });
        }
    }
}