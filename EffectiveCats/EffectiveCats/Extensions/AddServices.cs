using Domain.Finders;
using Domain.Interfaces;
using Domain.Interfaces.Finders;
using Domain.Interfaces.Repositories;
using Domain.Models;
using Domain.Models.Account;
using Domain.Repositories;
using Domain.Services;
using Infrastructure;

namespace EffectiveCats.Extensions
{
    public static class AddServicesExtension
    {
        public static IServiceCollection AddServices(this IServiceCollection services) =>
            services
                    .AddScoped<IUnitOfWork, UnitOfWork>()

                    .AddScoped<IFinder<Cat, long>, BaseFinder<Cat, long>>()
                    .AddScoped<IFinder<CatType, long>, BaseFinder<CatType, long>>()
                    .AddScoped<IFinder<User, long>, BaseFinder<User, long>>()

                    .AddScoped<ICatFinder, CatFinder>()
                    .AddScoped<ICatTypeFinder, CatTypeFinder>()

                    .AddScoped<IRepository<Cat, long>, BaseRepository<Cat, long>>()
                    .AddScoped<IRepository<CatType, long>, BaseRepository<CatType, long>>()
                    .AddScoped<IRepository<User, long>, BaseRepository<User, long>>()

                    .AddScoped<ICatRepository, CatRepository>()
                    .AddScoped<ICatTypeRepository, CatTypeRepository>()
                    .AddScoped<IUserRepository, UserRepository>()

                    .AddScoped<IUserService, UserService>()
                    .AddScoped<UserAccessor>()
                    .AddScoped<ICRUD<Cat, long>, CRUDService<Cat, long>>()
                    .AddScoped<ICRUD<CatType, long>, CRUDService<CatType, long>>();
        
    }
}
