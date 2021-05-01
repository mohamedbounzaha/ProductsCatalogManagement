using FluentValidation;
using ProductsCatalogManagement.Core.Repositories;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ProductsCatalogManagement.Application.Dtos
{
    public class ProductDto
    {
        public int Id { get; set; }

        /// <summary>
        /// The product code.
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// The product name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The product start validity date.
        /// </summary>
        public DateTime StartValidityDate { get; set; }

        /// <summary>
        /// The product end validity date.
        /// </summary>
        public DateTime EndValidityDate { get; set; }
    }

    public class ProductDtoValidator : AbstractValidator<ProductDto>
    {
        private readonly IProductRepository repository;

        public ProductDtoValidator(IProductRepository repository)
        {
            this.repository = repository;
            RuleFor(r => r).MustAsync(BeNotEmptyAndUnique).WithMessage("Product code must be unique and not empty or null.");
            RuleFor(p => p.Name).NotEmpty().WithMessage("Name must not be empty.");
            RuleFor(x => x.StartValidityDate).LessThan(p => p.EndValidityDate)
                .WithMessage("Start validity must be less than end validity.");
        }

        private async Task<bool> BeNotEmptyAndUnique(ProductDto productCode, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(productCode.Code))
            {
                return false;
            }
            var existingCodes = await this.repository.GetAsync(p => p.Code.ToLower() == productCode.Code.ToLower() && p.Id != productCode.Id);
            return existingCodes == null || existingCodes.Count == 0;
        }
    }
}