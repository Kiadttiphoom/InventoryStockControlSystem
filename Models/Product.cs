namespace InventoryStockControlSystem.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Code { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public int CategoryId { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal ReorderLevel { get; set; }
        public int ReorderQuantity { get; set; }
        public string? Unit { get; set; } = "Piece";
        public bool IsActive { get; set; } = true;
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedDate { get; set; }

        // Foreign keys
        public Category? Category { get; set; }

        // Navigation properties
        public ICollection<StockMovement>? StockMovements { get; set; }
    }
}
