using BlogManagement.Application;
using BlogManagement.Application.Services;
using BlogManagement.Data.Configuration.MapperConfigs;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using BlogManagement.Data;
using BlogManagement.Data.Entities;
using Microsoft.AspNetCore.Identity;

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
            // Register email sender service
            services.AddTransient<IEmailSender>(o =>
                new EmailSender(
                    "localhost",
                    25,
                    "no-reply@andreamooreblogspace.com")
            );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        public static void ConfigureIdentity(this IServiceCollection services)
        {
            services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole<long>>()
                .AddEntityFrameworkStores<BlogManagementContext>();

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
