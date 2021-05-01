using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using ProductsCatalogManagement.Api.Controllers;
using ProductsCatalogManagement.Application.Dtos;
using ProductsCatalogManagement.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace MachineMonitoring.Api.UnitTests
{
    public class MachineControllerTests
    {
        private readonly Mock<IProductService> _productAppService;
        private readonly Mock<ILogger<ProductController>> _logger;

        public MachineControllerTests()
        {
            _productAppService = new Mock<IProductService>();
            _logger = new Mock<ILogger<ProductController>>();
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
            _productAppService.Setup(ms => ms.GetProductsAsync()).Returns(Task.FromResult(expectedProducts));
            var controller = new ProductController(_productAppService.Object, _logger.Object);

            // Act
            var result = await controller.GetProducts();
            var resultValue = ((OkObjectResult)result).Value;

            // Assert
            result.Should().BeAssignableTo<OkObjectResult>();
            resultValue.Should().BeEquivalentTo(expectedProducts);
        }

        #endregion GetProductsAsync

        #region Create

        [Fact]
        public async Task WhenCreateProduct_CreateProduct_ShouldReturn_OkResult()
        {
            // Arrange
            ProductDto productToCreate = new ProductDto { Code = "VM1" };
            ProductDto expectedResult = new ProductDto { Id = 1, Code = "VM1" };
            _productAppService.Setup(ms => ms.CreateAsync(productToCreate)).Returns(Task.FromResult(expectedResult));
            var controller = new ProductController(_productAppService.Object, _logger.Object);

            // Act
            var result = await controller.CreateProduct(productToCreate);

            // Assert
            result.Should().BeAssignableTo<CreatedResult>();
            ((CreatedResult)result).Value.Should().BeEquivalentTo(expectedResult);
        }

        [Fact]
        public async Task WhenArgumentError_CreateProduct_ShouldReturn_BadRequestObjectResult()
        {
            // Arrange
            ProductDto productToCreate = new ProductDto { Code = "VM1" };
            ArgumentNullException argException = new ArgumentNullException();
            _productAppService.Setup(ms => ms.CreateAsync(productToCreate)).ThrowsAsync(argException);
            var controller = new ProductController(_productAppService.Object, _logger.Object);

            // Act
            var result = await controller.CreateProduct(productToCreate);

            // Assert
            result.Should().BeAssignableTo<BadRequestObjectResult>();
        }

        [Fact]
        public async Task WhenArgumentError_CreateProduct_ShouldReturn_Null()
        {
            // Arrange
            ProductDto productToCreate = new ProductDto { Code = "VM1" };
            Exception exception = new Exception();
            _productAppService.Setup(ms => ms.CreateAsync(productToCreate)).ThrowsAsync(exception);
            var controller = new ProductController(_productAppService.Object, _logger.Object);

            // Act
            var result = await controller.CreateProduct(productToCreate);

            // Assert
            result.Should().BeNull();
        }

        #endregion Create
    }
}