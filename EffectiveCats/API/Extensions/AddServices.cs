using BL.Interfaces;
using DAL.Interfaces;
using DAL.Interfaces.Finders;
using DAL.Interfaces.Repositories;
using DAL.Models;
using DAL.Repositories;
using Domain.Finders;
using Domain.Services;
using Infrastructure;

namespace EffectiveCats.Extensions
{
    public static class AddServicesExtension
    {
        public static IServiceCollection AddServices(this IServiceCollection services) =>
            services
                    .AddScoped<IUnitOfWork, UnitOfWork>()

                    .AddScoped<ICatFinder, CatFinder>()
                    .AddScoped<ICatTypeFinder, CatTypeFinder>()

                    .AddScoped<ICatRepository, CatRepository>()
                    .AddScoped<ICatTypeRepository, CatTypeRepository>()
                    .AddScoped<IUserRepository, UserRepository>()

                    .AddScoped<IUserService, UserService>()
                    .AddScoped<UserAccessor>();
        
    }
}
