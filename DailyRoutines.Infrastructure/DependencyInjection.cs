
using DailyRoutines.Application.Convertors;
using DailyRoutines.Application.Interfaces;
using DailyRoutines.Application.Senders;
using DailyRoutines.Application.Services;
using DailyRoutines.Domain.Interfaces;
using DailyRoutines.Infrastructure.Context;
using DailyRoutines.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace DailyRoutines.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        #region Db Context

        services.AddDbContext<DailyRoutinesDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DailyRoutinesDbConnection")));

        #endregion

        #region IoC

        //services 
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IAccessService, AccessService>();
        services.AddScoped<IRoutineService, RoutineService>();
        services.AddScoped<IMessageSender, MessageSender>();
        services.AddScoped<IViewRenderService, ViewRenderService>();

        //repository
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IAccessRepository, AccessRepository>();
        services.AddScoped<IRoutineRepository, RoutinesRepository>();

        #endregion

        #region Authentication

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["Jwt:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]))
                };
            });

        #endregion

        #region CORS

        services.AddCors(options =>
        {
            options.AddPolicy("EnableCors", builder =>
            {
                builder.AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .Build();
            });
        });

        #endregion



        #region Configure

        // should ssl site
        services.Configure<MvcOptions>(options => { options.Filters.Add(new RequireHttpsAttribute()); });

        #endregion

        return services;
    }
}