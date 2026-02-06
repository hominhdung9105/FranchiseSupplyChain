using Microsoft.EntityFrameworkCore;
using Product.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Product.Infrastructure.Persistence
{
    public class ProductDbContext : DbContext
    {
        public ProductDbContext(DbContextOptions<ProductDbContext> options)
            : base(options) { }
        public DbSet<Product.Domain.Entities.Product> Products => Set<Product.Domain.Entities.Product>();
        public DbSet<ProductVariant> ProductVariants => Set<ProductVariant>();
        public DbSet<ProductImage> ProductImages => Set<ProductImage>();
        public DbSet<Category> Categories => Set<Category>();
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProductDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
