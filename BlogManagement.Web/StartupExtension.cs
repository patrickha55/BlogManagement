using BlogManagement.Application;
using BlogManagement.Application.Contracts;
using BlogManagement.Application.Contracts.Repositories;
using BlogManagement.Application.Contracts.Services;
using BlogManagement.Application.Repositories;
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
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ITagService, TagService>();
            services.AddScoped<IPostService, PostService>();
            services.AddScoped<IPostRatingService, PostRatingService>();
            services.AddScoped<IUserService, UserService>();

            services.AddAutoMapper(typeof(MapperConfig));

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
