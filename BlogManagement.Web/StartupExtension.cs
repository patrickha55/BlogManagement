using BlogManagement.Application.ApiClient;
using BlogManagement.Application.Services;
using BlogManagement.Common.Common;
using BlogManagement.Contracts.ApiClient;
using BlogManagement.Contracts.Services.ClientServices;
using BlogManagement.Data;
using BlogManagement.Data.Configuration.MapperConfigs;
using BlogManagement.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using BlogManagement.Application.Services.ClientServices;
using CategoryService = BlogManagement.Application.Services.ClientServices.CategoryService;
using PostService = BlogManagement.Application.Services.ClientServices.PostService;
using TagService = BlogManagement.Application.Services.ClientServices.TagService;
using UserService = BlogManagement.Application.Services.ClientServices.UserService;

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

            services.AddHttpClient(Constants.HttpClientName, c => { c.BaseAddress = new Uri("https://localhost:44392/api/"); });

            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ITagService, TagService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IPostService, PostService>();

            services.AddAutoMapper(typeof(MapperConfig));
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
