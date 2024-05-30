using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace kaizenplus.Extensions
{
    public static class AppCorsExtension
    {
        public static IServiceCollection AddAppCors(this IServiceCollection services, IConfiguration configuration)
        {
            var configSection = configuration.GetSection("Cors");

            var origin = configSection["Origin"];

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder => builder
                .WithOrigins("http://localhost:5268", "https://localhost:7243")
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials());
            });

            return services;
        }
    }
}