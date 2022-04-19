using DAL.Models;
using DAL.Models.Account;
using Microsoft.AspNetCore.Identity;

namespace DAL.Repositories
{
    public static class DbInitializer
    {
        public async static void Initialize(MainContext context, UserManager<User> userManager)
        {
            context.Database.EnsureCreated();

            if (context.CatTypes.Any())
            {
                return;
            }

            var catTypes = new CatType[]
            {
                new CatType { Name = "Шотландец"},
                new CatType { Name = "Бродяга"}
            };
            foreach(var type in catTypes)
            {
                context.CatTypes.Add(type);
            }
            context.SaveChanges();

            var cats = new Cat[]
            {
                new Cat{Type=catTypes[0],Name="Котик", Description = "test", Image = new byte[0]},
                new Cat{Type=catTypes[1],Name="Китик", Description = "test", Image = new byte[0]}
            };
            foreach (var cat in cats)
            {
                context.Cats.Add(cat);
            }
            context.SaveChanges();

            var user = new User
            {
                UserName = "test",
                SecurityStamp = Guid.NewGuid().ToString()
            };

            await userManager.CreateAsync(user, "test");

            context.SaveChanges();
        }
    }
}
