using CartTracker.Database;
using CartTracker.Models;
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
            services.AddScoped<IRepository<Category>, CategoryRepository>();
            services.AddScoped<CategoryService>();
        }
    }
}
