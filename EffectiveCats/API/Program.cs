using Microsoft.EntityFrameworkCore;
using MediatR;
using FluentValidation.AspNetCore;
using EffectiveCats.Middlewares;
using EffectiveCats.Extensions;
using API.Behaviors;
using SQLiteDAL.Repositories;
using DomainMongo;
using BL.Repository;
using DomainMongo.Repositories;
using StackExchange.Redis;

var assembly = AppDomain.CurrentDomain.Load("MediatRL");
var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<DogDatabaseSettings>(
    builder.Configuration.GetSection("DogsDatabase"));
builder.Services.AddDbContext<MainContext>(options => options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")))
                .AddSwagger()
                .AddMvc(setup =>
                {
                    setup.EnableEndpointRouting = false;
                    setup.Filters.Add<ValidationMiddleware>();
                })
                .AddFluentValidation(cfg => cfg.RegisterValidatorsFromAssembly(assembly));

builder.Services.AddControllers();

var configurationOptions = new ConfigurationOptions
{
    EndPoints = { "127.0.0.1:12000" },
     Ssl = true,
     //AbortOnConnectFail = false,
 };
builder.Services.AddScoped<IDatabase>(x => ConnectionMultiplexer.Connect("127.0.0.1:12000").GetDatabase());
//builder.Services.AddStackExchangeRedisCache(options =>
//{
//    options.Configuration = "127.0.0.1:12000";
//});
builder.Services.AddMediatR(assembly)
                .AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>))
                .AddAutoMapper(assembly)
                .AddServices()
                .AddAuthenticationJWT(builder.Configuration);
builder.Services.AddSwaggerGen(options =>
{
    options.CustomSchemaIds(type => type.FullName.Replace("+", "_"));
});
var app = builder.Build().InitDatabase();



if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection()
   .UseAuthentication()
   .UseAuthorization()
   .UseMiddleware<ErrorHandlerMiddleware>();

app.MapControllers();

app.Run();
