using BL.Finders;
using BL.Repository;
using BL.Services;
using Domain.Models;
using Domain.Models.Account;
using Infrastructure;
using MediatR.Interfaces;
using MediatR.Services;
using SQLiteDAL.Finders;
using SQLiteDAL.Repositories;

namespace EffectiveCats.Extensions
{
    public static class AddServicesExtension
    {
        public static IServiceCollection AddServices(this IServiceCollection services) =>
            services
                    .AddScoped<IUnitOfWork, UnitOfWork>()

                    .AddScoped(x=> x.GetRequiredService<MainContext>().Cats)
                    .AddScoped(x=> x.GetRequiredService<MainContext>().CatTypes)



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
