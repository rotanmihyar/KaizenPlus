using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using kaizenplus.Files.Models;
using kaizenplus.Models;
using kaizenplus.Security.Token.Models;


namespace kaizenplus.Extensions
{
    public static class ConfigurationExtensions
    {
        public static void AddConfigurationServices(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            serviceCollection.AddSingleton<IConfiguration>(configuration);
            serviceCollection.Configure<FileManagerConfigurations>(configuration.GetSection("Storage"));
            serviceCollection.Configure<TokenConfigurations>(configuration.GetSection("Token"));
        
       
            serviceCollection.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });
        }
    }

  
}