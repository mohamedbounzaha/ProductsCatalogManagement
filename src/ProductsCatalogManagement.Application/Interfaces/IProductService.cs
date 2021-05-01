using ProductsCatalogManagement.Application.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductsCatalogManagement.Application.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetProductsAsync();

        Task<ProductDto> CreateAsync(ProductDto productDto);
    }
}