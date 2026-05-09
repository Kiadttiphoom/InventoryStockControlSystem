namespace InventoryStockControlSystem.ViewModels
{
    public class StockMovementViewModel
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public int WarehouseId { get; set; }
        public string? WarehouseName { get; set; }
        public string MovementType { get; set; } = null!;
        public int Quantity { get; set; }
        public string? Reference { get; set; }
        public string? Notes { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
