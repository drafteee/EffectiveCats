using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Domain.Services
{
    public static class Extensions
    {
        public static IServiceCollection AddDomainServices(this IServiceCollection services)
        {
            var assemblyServices = Assembly.GetAssembly(typeof(CatService)).GetTypes().Where(x => x.Name.Contains("Service"));
            foreach (var service in assemblyServices)
            {
                if (!service.IsGenericType)
                {
                    services.Add(new ServiceDescriptor(service.GetInterface("ICRUD") , service, ServiceLifetime.Scoped));
                    //services.Add(new ServiceDescriptor(service, ServiceLifetime.Scoped));
                }
            }

            return services;
        }
    }
}
