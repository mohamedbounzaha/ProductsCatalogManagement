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

        public virtual DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(e => e.Code)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.StartValidityDate)
                  .IsRequired();

                entity.Property(e => e.EndValidityDate)
              .IsRequired();

                // add unique constraint
                entity.HasIndex(e => e.Code).IsUnique();
            });
        }
    }
}
