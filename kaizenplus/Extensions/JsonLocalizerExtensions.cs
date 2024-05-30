using System;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using kaizenplus.Localizations;

namespace kaizenplus.Extensions
{
    public static class JsonLocalizerExtensions
    {
        public static IServiceCollection AddJsonLocalization(this IServiceCollection serviceCollection)
        {
            if (serviceCollection == null)
            {
                throw new ArgumentNullException(nameof(serviceCollection));
            }

            _ = serviceCollection.AddOptions();

            var serviceProvider = serviceCollection.BuildServiceProvider();
            var httpContextAccessor = serviceProvider.GetService<IHttpContextAccessor>();
            AppHttpContext.Configure(httpContextAccessor);

            AddJsonLocalizationServices(serviceCollection);

            return serviceCollection;
        }

        public static IServiceCollection AddJsonLocalization(
            this IServiceCollection serviceCollection,
            Action<JsonLocalizationOptions> setupAction)
        {
            if (serviceCollection == null)
            {
                throw new ArgumentNullException(nameof(serviceCollection));
            }

            if (setupAction == null)
            {
                Console.Error.WriteLine("Setup Action seems to be null, The localization options will not be override. For any helps create an issue at ");
                AddJsonLocalizationServices(serviceCollection);
            }

            AddJsonLocalizationServices(serviceCollection, setupAction);

            return serviceCollection;
        }

        internal static void AddJsonLocalizationServices(IServiceCollection serviceCollection)
        {
            _ = serviceCollection.AddMemoryCache();
            _ = serviceCollection.AddSingleton<IStringLocalizerFactory, JsonStringLocalizerFactory>();
            _ = serviceCollection.AddScoped<IJsonStringLocalizer, JsonStringLocalizer>();
            _ = serviceCollection.AddScoped(typeof(IJsonStringLocalizer<>), typeof(JsonStringLocalizerOfT<>));
            _ = serviceCollection.AddScoped<IStringLocalizer, JsonStringLocalizer>();
            _ = serviceCollection.AddScoped(typeof(IStringLocalizer<>), typeof(JsonStringLocalizerOfT<>));

        }

        internal static void AddJsonLocalizationServices(
            IServiceCollection serviceCollection,
            Action<JsonLocalizationOptions> setupAction)
        {
            AddJsonLocalizationServices(serviceCollection);
            _ = serviceCollection.Configure(setupAction);
        }
    }
}