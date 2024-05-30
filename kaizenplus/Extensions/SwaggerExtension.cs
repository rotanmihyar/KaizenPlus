using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace kaizenplus.Extensions
{
    public static class SwaggerExtension
    {
        /// <summary>
        /// Adds Swagger UI to the application
        /// </summary>
        /// <param name="title">Swagger UI Title</param>
        /// <param name="version">Swagger UI Version</param>
        /// <returns></returns>
        public static IServiceCollection AddSwagger(this IServiceCollection services, string title, string version)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = title, Version = version });

                var security = new OpenApiSecurityRequirement
                {
                    { new OpenApiSecurityScheme { Name = "Bearer" }, new string[] { } }
                };

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });
                c.AddSecurityRequirement(security);
            });

            return services;
        }
    }
}