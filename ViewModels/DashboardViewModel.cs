namespace InventoryStockControlSystem.ViewModels
{
    public class DashboardViewModel
    {
        public int TotalProducts { get; set; }
        public int TotalCategories { get; set; }
        public int TotalWarehouses { get; set; }
        public int LowStockProducts { get; set; }
        public decimal TotalInventoryValue { get; set; }
        public List<StockMovementViewModel> RecentMovements { get; set; } = new();
        public List<ProductStockViewModel> TopMovingProducts { get; set; } = new();
        public List<WarehouseStockViewModel> WarehouseStockStatus { get; set; } = new();
    }

    public class ProductStockViewModel
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = null!;
        public int TotalStock { get; set; }
        public int MovementCount { get; set; }
    }

    public class WarehouseStockViewModel
    {
        public int WarehouseId { get; set; }
        public string WarehouseName { get; set; } = null!;
        public int TotalProductsStored { get; set; }
        public int TotalUnitsInStock { get; set; }
    }
}
