using CoreFinalDemo.BL.Interface;
using CoreFinalDemo.BL.Services;
using CoreFinalDemo.Models;
using Microsoft.Extensions.DependencyInjection;

namespace CoreFinalDemo.Extensions
{
    /// <summary>
    /// Extension methods for adding BL services to the service collection.
    /// </summary>
    public static class ServiceExtensions
    {
        /// <summary>
        /// Adds BL services to the service collection.
        /// </summary>
        /// <param name="services">The service collection.</param>
        public static void AddBLServices(this IServiceCollection services)
        {
            services.AddTransient<Response>();
            services.AddScoped<IBKS01, BLBKS01>();
            services.AddScoped<IUSR01, BLUSR01>();
            services.AddScoped<ILogin, BLLogin>();
        }
    }
}