using System;

namespace Product.Domain.Entities
{
    public class ProductImage : BaseEntity
    {
        public string Url { get; set; } = string.Empty;
        public bool IsMain { get; set; }
        
        public Guid ProductId { get; set; }
        public Product Product { get; set; } = null!;
        
        public Guid? ProductVariantId { get; set; }
        public ProductVariant? ProductVariant { get; set; }
    }
}
