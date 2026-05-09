namespace InventoryStockControlSystem.Models
{
    public class StockMovement
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int WarehouseId { get; set; }
        public string MovementType { get; set; } = null!; // "In", "Out", "Adjustment"
        public int Quantity { get; set; }
        public string? Reference { get; set; } // PO Number, SO Number, etc.
        public string? Notes { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        // Foreign keys
        public Product? Product { get; set; }
        public Warehouse? Warehouse { get; set; }
    }
}
