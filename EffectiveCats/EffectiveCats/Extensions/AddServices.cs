using Domain.Interfaces;
using Domain.Models;
using Domain.Services;
using Infrastructure;

namespace EffectiveCats.Extensions
{
    public static class AddServicesExtension
    {
        public static IServiceCollection AddServices(this IServiceCollection services) =>
            services.AddScoped<IUserService, UserService>()
                    .AddScoped<UserAccessor>()
                    .AddScoped<ICRUD<Cat>, CRUDService<Cat>>()
                    .AddScoped<ICRUD<CatType>, CRUDService<CatType>>();
        
    }
}
