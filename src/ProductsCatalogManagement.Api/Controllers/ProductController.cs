using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        private readonly IValidator<ProductDto> _productValidator;

        public ProductController(IProductService productAppService, IValidator<ProductDto> productValidator)
        {
            _productAppService = productAppService ?? throw new ArgumentNullException(nameof(productAppService));
            _productValidator = productValidator ?? throw new ArgumentNullException(nameof(productAppService));
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
                // TODO ADD Logger
                return new BadRequestObjectResult(apiErrorException);
            }
            catch (Exception exception)
            {
                // TODO ADD Logger
                return null;
            }
        }
    }
}