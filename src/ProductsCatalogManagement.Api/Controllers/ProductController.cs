using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProductsCatalogManagement.Api.Contracts;
using ProductsCatalogManagement.Api.Extensions;
using ProductsCatalogManagement.Application.Dtos;
using ProductsCatalogManagement.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductsCatalogManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiConventionType(typeof(DefaultApiConventions))]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productAppService;
        private readonly ILogger<ProductController> _logger;

        public ProductController(IProductService productAppService, ILogger<ProductController> logger)
        {
            _productAppService = productAppService ?? throw new ArgumentNullException(nameof(productAppService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        [Route("/api/products")]
        [Produces(ContentTypes.Json)]
        [ProducesResponseType(200, StatusCode = StatusCodes.Status200OK, Type = typeof(IEnumerable<ProductDto>))]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _productAppService.GetProductsAsync();
            return Ok(products);
        }

        [HttpPost("/api/createproduct")]
        [Produces(ContentTypes.Json)]
        [ProducesResponseType(201, StatusCode = StatusCodes.Status201Created, Type = typeof(ProductDto))]
        [ProducesResponseType(400, StatusCode = StatusCodes.Status400BadRequest, Type = typeof(ApiExceptionDto))]
        public async Task<IActionResult> CreateProduct([FromBody] ProductDto productDto)
        {
            try
            {
                var rowsAffected = await _productAppService.CreateAsync(productDto);
                return Created(string.Empty, rowsAffected);
            }
            catch (ArgumentNullException argumentNullException)
            {
                var apiErrorException = argumentNullException.CreateApiErrorException(StatusCodes.Status400BadRequest);
                return new BadRequestObjectResult(apiErrorException);
            }
            catch (Exception exception)
            {
                _logger.LogError($"{nameof(CreateProduct)}" + "@{ex}", exception);
                return null;
            }
        }
    }
}