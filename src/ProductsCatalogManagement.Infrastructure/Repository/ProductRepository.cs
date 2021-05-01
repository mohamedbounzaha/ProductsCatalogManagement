using MachineMonitoring.Infrastructure.Repository.Base;
using ProductsCatalogManagement.Core.Entities;
using ProductsCatalogManagement.Core.Repositories;
using ProductsCatalogManagement.Infrastructure.Data;

namespace ProductsCatalogManagement.Infrastructure.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(ProductsCatalogManagementContext dbContext) : base(dbContext)
        {
        }
    }
}