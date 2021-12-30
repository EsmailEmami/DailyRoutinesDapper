using DailyRoutines.Infrastructure.Context;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using DailyRoutines.Application.Interfaces;
using DailyRoutines.Application.Services;
using DailyRoutines.Domain.Interfaces;
using DailyRoutines.Infrastructure.Repositories;


namespace DailyRoutines.Infrastructure
{
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
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IActionService, ActionService>();

            //repository
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IActionRepository, ActionRepository>();

            #endregion

            #region Authentication

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            }).AddCookie(options =>
            {
                options.LoginPath = "/Login";
                options.LogoutPath = "/Logout";
                options.ExpireTimeSpan = TimeSpan.FromDays(30);
                options.Cookie.HttpOnly = true;
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                options.AccessDeniedPath = "/";
            });

            #endregion

            #region Cryptography

            services.AddDataProtection()
                .DisableAutomaticKeyGeneration()
                .SetDefaultKeyLifetime(new TimeSpan(15, 0, 0, 0));

            #endregion

            #region Configure

            // should ssl site
            services.Configure<MvcOptions>(options => { options.Filters.Add(new RequireHttpsAttribute()); });

            #endregion

            return services;
        }
    }
}
