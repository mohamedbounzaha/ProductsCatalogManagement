using Microsoft.Extensions.DependencyInjection;
using ProductsCatalogManagement.Application.Interfaces;
using ProductsCatalogManagement.Application.Services;

namespace ProductsCatalogManagement.Application
{
    public static class ApplicationDependencyInjectionExtension
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IProductService, ProductService>();
            return services;
        }
    }
}