using FluentAssertions;
using Moq;
using ProductsCatalogManagement.Application.Dtos;
using ProductsCatalogManagement.Application.Services;
using ProductsCatalogManagement.Core.Entities;
using ProductsCatalogManagement.Core.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace MachineMonitoring.Api.UnitTests
{
    public class ProductServiceTests
    {
        private readonly Mock<IProductRepository> _productRepository;

        public ProductServiceTests()
        {
            _productRepository = new Mock<IProductRepository>();
        }

        #region GetProductsAsync

        [Fact]
        public async Task WhenProductsExists_GetProductsAsync_ShouldReturnAllProducts()
        {
            // Arrange
            IEnumerable<ProductDto> expectedProducts = new List<ProductDto>()
            {
                new ProductDto(){Id = 1,Code = "VM1"},
                new ProductDto(){Id = 2,Code = "VM2"}
            };
            IReadOnlyList<Product> fakeProducts = new List<Product>()
            {
                new Product(){Id = 1,Code = "VM1"},
                new Product(){Id = 2,Code = "VM2"}
            };
            _productRepository.Setup(ms => ms.GetAllAsync()).Returns(Task.FromResult(fakeProducts));
            var service = new ProductService(_productRepository.Object);

            // Act
            var result = await service.GetProductsAsync();

            // Assert
            result.Should().BeEquivalentTo(expectedProducts);
        }

        #endregion GetProductsAsync

        #region Create

        [Fact]
        public async Task WhenCreateProduct_CreateProduct_ShouldReturn_OkResult()
        {
            // Arrange
            ProductDto productToCreate = new ProductDto { Code = "VM1" };
            ProductDto expectedResult = new ProductDto { Id = 1, Code = "VM1" };
            _productRepository.Setup(ms => ms.AddAsync(It.IsAny<Product>())).Returns(Task.FromResult(new Product { Id = 1, Code = "VM1" }));
            var service = new ProductService(_productRepository.Object);

            // Act
            var result = await service.CreateAsync(productToCreate);

            // Assert
            result.Should().BeEquivalentTo(expectedResult);
        }

        #endregion Create
    }
}