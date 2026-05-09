using InventoryStockControlSystem.Models;

namespace InventoryStockControlSystem.Services
{
    public interface IStockMovementService
    {
        Task<StockMovement?> GetStockMovementByIdAsync(int id);
        Task<IEnumerable<StockMovement>> GetAllStockMovementsAsync();
        Task<IEnumerable<StockMovement>> GetStockMovementsByProductAsync(int productId);
        Task<IEnumerable<StockMovement>> GetStockMovementsByWarehouseAsync(int warehouseId);
        Task<StockMovement> RecordStockInAsync(int productId, int warehouseId, int quantity, string? reference, string? notes, string? createdBy);
        Task<StockMovement> RecordStockOutAsync(int productId, int warehouseId, int quantity, string? reference, string? notes, string? createdBy);
        Task<int> GetCurrentStockAsync(int productId, int warehouseId);
    }
}
