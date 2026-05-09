using InventoryStockControlSystem.Models;

namespace InventoryStockControlSystem.Services
{
    public interface IWarehouseService
    {
        Task<Warehouse?> GetWarehouseByIdAsync(int id);
        Task<IEnumerable<Warehouse>> GetAllWarehousesAsync();
        Task<Warehouse> CreateWarehouseAsync(Warehouse warehouse);
        Task UpdateWarehouseAsync(Warehouse warehouse);
        Task DeleteWarehouseAsync(int id);
    }
}
