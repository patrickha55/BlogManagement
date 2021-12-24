using BlogManagement.Application;
using BlogManagement.Application.Contracts;
using BlogManagement.Data.Configuration.MapperConfigs;
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
            services.AddTransient<IUnitOfWork, UnitOfWork>();

            services.AddAutoMapper(typeof(MapperConfig));
        }
    }
}
