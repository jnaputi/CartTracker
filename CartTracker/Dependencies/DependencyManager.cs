using CartTracker.Database;
using CartTracker.Repositories;
using CartTracker.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CartTracker.Dependencies
{
    public class DependencyManager
    {
        public static void RegisterDependencies(IServiceCollection services)
        {
            services.AddScoped<CartTrackerContext>();
            services.AddScoped<CategoryRepository>();
            services.AddScoped<CategoryService>();
        }
    }
}
