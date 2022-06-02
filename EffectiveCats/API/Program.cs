using Microsoft.EntityFrameworkCore;
using MediatR;
using FluentValidation.AspNetCore;
using EffectiveCats.Middlewares;
using EffectiveCats.Extensions;
using API.Behaviors;
using SQLiteDAL.Repositories;
using DomainMongo;
using StackExchange.Redis;
using BLL;
using Domain.Entities;

var assembly = AppDomain.CurrentDomain.Load("MediatRL");
var builder = WebApplication.CreateBuilder(args);

builder.Host.ConfigureAppConfiguration(app => {
    var prefix = "LESSON_";
    app.AddEnvironmentVariables(prefix);
});

//builder.Services.Configure<DogDatabaseSettings>(builder.Configuration.GetSection("DogsDatabase"));

builder.Services.AddDbContext<MainContext>(options => options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")))
                .AddSwagger()
                .AddMvc(setup =>
                {
                    setup.EnableEndpointRouting = false;
                    setup.Filters.Add<ValidationMiddleware>();
                })
                .AddFluentValidation(cfg => cfg.RegisterValidatorsFromAssembly(assembly));

builder.Services.AddControllers();

//builder.Services.AddScoped<IDatabase>(x => ConnectionMultiplexer.Connect("127.0.0.1:12000").GetDatabase());
builder.Services.AddSingleton<IConnectionMultiplexer>(x => ConnectionMultiplexer.Connect("127.0.0.1:12000"));

//extension for cache injections
builder.Services.AddSingleton<DictionaryCache<Cat>>();

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

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection()
   .UseAuthentication()
   .UseAuthorization()
   .UseMiddleware<ErrorHandlerMiddleware>();

app.MapControllers();

app.Run();
