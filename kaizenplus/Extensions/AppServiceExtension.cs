using System.Reflection;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using kaizenplus.Attributes;
using kaizenplus.Cache;
using kaizenplus.DataAccess.Repositories;
using kaizenplus.DataAccess.UnitOfWorks;
using kaizenplus.Extensions.DependencyInjection;
using kaizenplus.Services.Users;
using kaizenplus.Services.UserService;
using kaizenplus.Services.WarehouseServices;
using kaizenplus.Services.WarehouseItemServices;

namespace kaizenplus.Extensions
{
    public static class AppServiceExtension
    {
        public static IServiceCollection AddAppServices(this IServiceCollection services)
        {
            services.RegisterAssemblyPublicNonGenericClasses<TransientInjectableAttribute>(Assembly.GetExecutingAssembly())
            .AsPublicImplementedInterfaces(ServiceLifetime.Transient);

            services.RegisterAssemblyPublicNonGenericClasses<ScopedInjectableAttribute>(Assembly.GetExecutingAssembly())
            .AsPublicImplementedInterfaces(ServiceLifetime.Scoped);

            services.RegisterAssemblyPublicNonGenericClasses<SingeltonInjectableAttribute>(Assembly.GetExecutingAssembly())
            .AsPublicImplementedInterfaces(ServiceLifetime.Singleton);

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            //services.AddScoped(typeof(ILookupService<>), typeof(LookupService<>));
         
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IWarehouseService, WarehouseService>();
            services.AddScoped<IWarehouseItemService, WarehouseItemService>();
            services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
       
            services.AddJsonLocalization();

            services.AddSingleton(typeof(IMemoryCacheManager<>), typeof(MemoryCacheManager<>));

            return services;
        }
    }
}