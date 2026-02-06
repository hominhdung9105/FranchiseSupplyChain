using System;

namespace Product.Domain.Entities
{
    public class ProductVariant : BaseEntity
    {
        public Guid ProductId { get; set; }
        public Product Product { get; set; } = null!;
        
        public string Color { get; set; } = string.Empty;
        public string Size { get; set; } = string.Empty;
        public string Sku { get; set; } = string.Empty;
        public int InventoryQuantity { get; set; }
        public decimal CurrentPrice { get; set; }
    }
}
