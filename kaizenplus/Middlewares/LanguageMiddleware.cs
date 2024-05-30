using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using kaizenplus.Localizations;

namespace kaizenplus.Middlewares
{
    public class LanguageMiddleware
    {
        private readonly RequestDelegate next;

        public LanguageMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            //var languageHelper = context.Request.HttpContext.RequestServices.GetService<ILanguageHelper>();
           // languageHelper.SetCurrentUICulture(context.Request.Headers["Accept-Language"]);

            await next.Invoke(context);
        }
    }
}