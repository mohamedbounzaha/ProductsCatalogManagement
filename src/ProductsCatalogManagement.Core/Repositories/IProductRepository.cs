using ProductsCatalogManagement.Core.Entities;
using ProductsCatalogManagement.Core.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductsCatalogManagement.Core.Repositories
{
    public interface IProductRepository : IRepository<Product>
    {
    }
}
