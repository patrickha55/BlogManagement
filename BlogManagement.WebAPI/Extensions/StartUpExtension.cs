using BlogManagement.Application;
using BlogManagement.Application.Contracts;
using BlogManagement.Application.Contracts.Repositories;
using BlogManagement.Application.Contracts.Services;
using BlogManagement.Application.Repositories;
using BlogManagement.Application.Services;
using BlogManagement.Data;
using BlogManagement.Data.Configuration.MapperConfigs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BlogManagement.WebAPI.Extensions
{
    public static class StartUpExtension
    {
        public static void ConfigureApplicationContext(this IServiceCollection services, IConfiguration configuration)
            => services.AddDbContext<BlogManagementContext>(
                options => options.UseSqlServer(configuration.GetConnectionString("BlogManagementDB"))
                );

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
            services.AddScoped<IPostCommentService, PostCommentService>();

            services.AddAutoMapper(typeof(MapperConfig));
        }
    }
}
