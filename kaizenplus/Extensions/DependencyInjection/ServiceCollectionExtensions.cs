using System;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace kaizenplus.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtentions
    {
        public static AutoRegisterData RegisterAssemblyPublicNonGenericClasses<T>(this IServiceCollection services, Assembly assembly)
        {
            var allPublicTypes = assembly
                .GetExportedTypes()
                .Where(y =>
                    y.IsClass &&
                    !y.IsAbstract &&
                    !y.IsGenericType &&
                    !y.IsNested &&
                    y.GetInterfaces().Any(i => Attribute.GetCustomAttribute(i, typeof(T)) != null));
            return new AutoRegisterData(services, allPublicTypes);
        }

        public static IServiceCollection AsPublicImplementedInterfaces(this AutoRegisterData autoRegData,
            ServiceLifetime lifetime = ServiceLifetime.Transient)
        {
            if (autoRegData == null) throw new ArgumentNullException(nameof(autoRegData));
            foreach (var classType in (autoRegData.TypeFilter == null
                ? autoRegData.TypesToConsider
                : autoRegData.TypesToConsider.Where(autoRegData.TypeFilter)))
            {
                var interfaces = classType.GetTypeInfo().ImplementedInterfaces
                    .Where(i => i != typeof(IDisposable) && (i.IsPublic));
                foreach (var infc in interfaces)
                {
                    autoRegData.Services.Add(new ServiceDescriptor(infc, classType, lifetime));
                }
            }

            return autoRegData.Services;
        }
    }
}