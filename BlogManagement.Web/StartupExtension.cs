using System;
using BlogManagement.Application;
using BlogManagement.Application.Contracts;
using BlogManagement.Application.Repositories;
using BlogManagement.Application.Services;
using BlogManagement.Data.Configuration.MapperConfigs;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.DependencyInjection;

namespace BlogManagement.Web
{
    /// <summary>
    /// 
    /// </summary>
    public static class StartupExtension
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddTransient<IUnitOfWork, UnitOfWork>();

            services.AddAutoMapper(typeof(MapperConfig));

            // Register email sender service
            services.AddTransient<IEmailSender>(o =>
                new EmailSender(
                    "localhost",
                    25,
                    "no-reply@andreamooreblogspace.com")
            );

            services.ConfigureApplicationCookie(options =>
            {
                // Cookie settings
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(10);

                options.LoginPath = "/Identity/Account/Login";
                options.AccessDeniedPath = "/Identity/Account/AccessDenied";
                options.SlidingExpiration = true;
            });
        }
    }
}
