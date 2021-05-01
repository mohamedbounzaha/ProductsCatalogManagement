using MachineMonitoring.Infrastructure.Repository.Base;
using ProductsCatalogManagement.Core.Entities;
using ProductsCatalogManagement.Core.Repositories;
using ProductsCatalogManagement.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductsCatalogManagement.Infrastructure.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(ProductsCatalogManagementContext dbContext) : base(dbContext)
        {
        }
    }
}
