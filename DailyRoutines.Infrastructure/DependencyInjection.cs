using System.Security.Claims;
using DailyRoutines.Application.Convertors;
using DailyRoutines.Application.Interfaces;
using DailyRoutines.Application.Senders;
using DailyRoutines.Application.Services;
using DailyRoutines.Domain.Interfaces;
using DailyRoutines.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Threading.Tasks;
using DailyRoutines.Application.Security;
using Microsoft.AspNetCore.SignalR;

namespace DailyRoutines.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {


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
                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        var accessToken = context.Request.Query["access_token"];

                        // If the request is for our hub...
                        var path = context.HttpContext.Request.Path;
                        if (!string.IsNullOrEmpty(accessToken) &&
                            (path.StartsWithSegments("/ChatHub")))
                        {
                            // Read the token out of the query string
                            context.Token = accessToken;
                        }
                        return Task.CompletedTask;
                    }
                };
            });

        #endregion

        #region CORS

        services.AddCors(options =>
        {
            options.AddPolicy("EnableCors", builder =>
            {
                builder.AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials()
                    .WithOrigins("http://localhost:4200")
                    .Build();

            });
        });

        #endregion

        #region SignalR

        services.AddSignalR();

        #endregion

        #region IoC

        //services 
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IAccessService, AccessService>();
        services.AddScoped<IRoutineService, RoutineService>();
        services.AddScoped<IMessageSender, MessageSender>();
        services.AddScoped<IViewRenderService, ViewRenderService>();
        services.AddScoped<IChatRoomService, ChatRoomService>();

        //repository
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IAccessRepository, AccessRepository>();
        services.AddScoped<IRoutineRepository, RoutinesRepository>();
        services.AddScoped<IChatRoomRepository, ChatRoomRepository>();

        #endregion

        #region Configure

        // should ssl site
        services.Configure<MvcOptions>(options => { options.Filters.Add(new RequireHttpsAttribute()); });

        #endregion

        return services;
    }
}