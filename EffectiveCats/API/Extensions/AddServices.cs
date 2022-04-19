using BL.Interfaces;
using BL.Services;
using DAL.Interfaces;
using DAL.Interfaces.Finder;
using DAL.Interfaces.Finders;
using DAL.Interfaces.Repositories;
using DAL.Models;
using DAL.Models.Account;
using DAL.Repositories;
using Domain.Finders;
using Domain.Services;
using Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace EffectiveCats.Extensions
{
    public static class AddServicesExtension
    {
        public static IServiceCollection AddServices(this IServiceCollection services) =>
            services
                    .AddScoped<IUnitOfWork, UnitOfWork>()

                    .AddScoped(x=> x.GetRequiredService<MainContext>().Cats)
                    .AddScoped(x=> x.GetRequiredService<MainContext>().Cats)



                    .AddScoped<ICatFinder, CatFinder>()
                    .AddScoped<ICatTypeFinder, CatTypeFinder>()
                    .AddScoped<IUserFinder, UserFinder>()

                    .AddScoped<IRepository<Cat>, Repository<Cat>>()
                    .AddScoped<IRepository<CatType>, Repository<CatType>>()
                    .AddScoped<IRepository<User>, Repository<User>>()

                    .AddScoped<ICatService, CatService>()
                    .AddScoped<ICatTypeService, CatTypeService>()
                    .AddScoped<IUserService, UserService>()
                    .AddScoped<UserAccessor>();
        
    }
}
