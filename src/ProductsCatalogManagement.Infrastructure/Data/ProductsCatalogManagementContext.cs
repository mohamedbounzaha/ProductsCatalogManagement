using ProductsCatalogManagement.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductsCatalogManagement.Infrastructure.Data
{
    public class ProductsCatalogManagementContext : DbContext
    {
        public ProductsCatalogManagementContext()
        {
        }

        public ProductsCatalogManagementContext(DbContextOptions<ProductsCatalogManagementContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           
        }
    }
}
