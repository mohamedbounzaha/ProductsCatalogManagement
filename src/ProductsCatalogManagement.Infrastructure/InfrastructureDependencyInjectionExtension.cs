using MachineMonitoring.Infrastructure.Repository.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProductsCatalogManagement.Core.Repositories;
using ProductsCatalogManagement.Core.Repositories.Base;
using ProductsCatalogManagement.Infrastructure.Data;
using ProductsCatalogManagement.Infrastructure.Repository;

namespace ProductsCatalogManagement.Infrastructure
{
    public static class InfrastructureDependencyInjectionExtension
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ProductsCatalogManagementContext>(c =>
                c.UseSqlServer(configuration.GetConnectionString("ProductsCatalogManagementConnection")));
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IProductRepository, ProductRepository>();
            return services;
        }
    }
}