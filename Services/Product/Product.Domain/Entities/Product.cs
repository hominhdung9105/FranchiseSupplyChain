using System;
using System.Collections.Generic;

namespace Product.Domain.Entities
{
    public class Product : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty; // Material Number / Style Code
        public string Technology { get; set; } = string.Empty; // e.g., Air Max, React
        public string Gender { get; set; } = string.Empty; // Men, Women, Kids
        public decimal BasePrice { get; set; }
        
        public Guid CategoryId { get; set; }
        public Category Category { get; set; } = null!;
        
        public ICollection<ProductVariant> Variants { get; set; } = new List<ProductVariant>();
        public ICollection<ProductImage> Images { get; set; } = new List<ProductImage>();
    }
}
