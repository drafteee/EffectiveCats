using Microsoft.Extensions.DependencyInjection;

namespace Domain.Services
{
    public static class Extensions
    {
        public static IServiceCollection AddDomainServices(this IServiceCollection services)
            => services.AddScoped<CatService>();
    }
}
