using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using MediatR;
using FluentValidation.AspNetCore;
using EffectiveCats.Middlewares;
using EffectiveCats.Extensions;

var assembly = AppDomain.CurrentDomain.Load("Common");
var builder = WebApplication.CreateBuilder(args);
//add initialize db

builder.Services.AddDbContext<MainContext>(options => options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")))
                .AddSwagger()
                .AddMvc(setup =>
                {
                    setup.EnableEndpointRouting = false;
                    setup.Filters.Add<ValidationMiddleware>();
                })
                .AddFluentValidation(cfg => cfg.RegisterValidatorsFromAssembly(assembly));

builder.Services.AddControllers();

builder.Services.AddMediatR(assembly)
                .AddAutoMapper(assembly)
                .AddServices()
                .AddAuthenticationJWT(builder.Configuration);

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
