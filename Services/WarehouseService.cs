using InventoryStockControlSystem.Models;
using InventoryStockControlSystem.Repositories;

namespace InventoryStockControlSystem.Services
{
    public class WarehouseService : IWarehouseService
    {
        private readonly IRepository<Warehouse> _warehouseRepository;

        public WarehouseService(IRepository<Warehouse> warehouseRepository)
        {
            _warehouseRepository = warehouseRepository;
        }

        public async Task<Warehouse?> GetWarehouseByIdAsync(int id)
        {
            return await _warehouseRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Warehouse>> GetAllWarehousesAsync()
        {
            return await _warehouseRepository.FindAsync(w => w.IsActive);
        }

        public async Task<Warehouse> CreateWarehouseAsync(Warehouse warehouse)
        {
            warehouse.CreatedDate = DateTime.UtcNow;
            return await _warehouseRepository.AddAsync(warehouse);
        }

        public async Task UpdateWarehouseAsync(Warehouse warehouse)
        {
            warehouse.UpdatedDate = DateTime.UtcNow;
            await _warehouseRepository.UpdateAsync(warehouse);
        }

        public async Task DeleteWarehouseAsync(int id)
        {
            var warehouse = await GetWarehouseByIdAsync(id);
            if (warehouse != null)
            {
                await _warehouseRepository.DeleteAsync(warehouse);
            }
        }
    }
}
