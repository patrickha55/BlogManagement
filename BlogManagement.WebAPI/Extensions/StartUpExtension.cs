using BlogManagement.Data;
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
    }
}
