using ProductsCatalogManagement.Application.Dtos;
using ProductsCatalogManagement.Application.Interfaces;
using ProductsCatalogManagement.Application.Mapper;
using ProductsCatalogManagement.Core.Entities;
using ProductsCatalogManagement.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductsCatalogManagement.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
        }

        public async Task<ProductDto> CreateAsync(ProductDto productDto)
        {
            var mapped = ObjectMapper.Mapper.Map<Product>(productDto);
            var createdProduct = await _productRepository.AddAsync(mapped);
            return ObjectMapper.Mapper.Map<ProductDto>(createdProduct);
        }

        public async Task<IEnumerable<ProductDto>> GetProductsAsync()
        {
            var products = await _productRepository.GetAllAsync();
            var mapped = ObjectMapper.Mapper.Map<IEnumerable<ProductDto>>(products);
            return mapped;
        }
    }
}