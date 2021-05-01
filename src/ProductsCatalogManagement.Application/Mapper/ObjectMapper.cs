using AutoMapper;
using ProductsCatalogManagement.Application.Dtos;
using ProductsCatalogManagement.Core.Entities;
using System;

namespace ProductsCatalogManagement.Application.Mapper
{
    public static class ObjectMapper
    {
        private static readonly Lazy<IMapper> Lazy = new Lazy<IMapper>(() =>
        {
            var config = new MapperConfiguration(cfg =>
            {
                // This line ensures that internal properties are also mapped over.
                cfg.ShouldMapProperty = p => p.GetMethod.IsPublic || p.GetMethod.IsAssembly;
                cfg.AddProfile<ProductsCatalogManagementDtoMapper>();
            });
            var mapper = config.CreateMapper();
            return mapper;
        });

        public static IMapper Mapper => Lazy.Value;
    }

    public class ProductsCatalogManagementDtoMapper : Profile
    {
        public ProductsCatalogManagementDtoMapper()
        {
            CreateMap<Product, ProductDto>()
                .ReverseMap();
        }
    }
}