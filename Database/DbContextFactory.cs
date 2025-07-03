using Application.Common.Interfaces.Repositories;
using Database;
using Database.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace EntityFramework
{
    public static class DbContextFactoryExtensions
    {
        public static IServiceCollection AddEntityFramework(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("PostgreSQLConnection")));

            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IOrderContentRepository, OrderContentRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IUserProductRepository, UserProductRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IFaqRepository, FaqRepository>();

            return services;
        }
    }
}
