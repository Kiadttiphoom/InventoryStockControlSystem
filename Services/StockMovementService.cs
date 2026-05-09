using InventoryStockControlSystem.Data;
using InventoryStockControlSystem.Models;
using InventoryStockControlSystem.Repositories;
using Microsoft.EntityFrameworkCore;

namespace InventoryStockControlSystem.Services
{
    public class StockMovementService : IStockMovementService
    {
        private readonly IRepository<StockMovement> _stockMovementRepository;
        private readonly ApplicationDbContext _context;

        public StockMovementService(IRepository<StockMovement> stockMovementRepository, ApplicationDbContext context)
        {
            _stockMovementRepository = stockMovementRepository;
            _context = context;
        }

        public async Task<StockMovement?> GetStockMovementByIdAsync(int id)
        {
            return await _context.StockMovements
                .Include(sm => sm.Product)
                .Include(sm => sm.Warehouse)
                .FirstOrDefaultAsync(sm => sm.Id == id);
        }

        public async Task<IEnumerable<StockMovement>> GetAllStockMovementsAsync()
        {
            return await _context.StockMovements
                .Include(sm => sm.Product)
                .Include(sm => sm.Warehouse)
                .OrderByDescending(sm => sm.CreatedDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<StockMovement>> GetStockMovementsByProductAsync(int productId)
        {
            return await _context.StockMovements
                .Where(sm => sm.ProductId == productId)
                .Include(sm => sm.Warehouse)
                .OrderByDescending(sm => sm.CreatedDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<StockMovement>> GetStockMovementsByWarehouseAsync(int warehouseId)
        {
            return await _context.StockMovements
                .Where(sm => sm.WarehouseId == warehouseId)
                .Include(sm => sm.Product)
                .OrderByDescending(sm => sm.CreatedDate)
                .ToListAsync();
        }

        public async Task<StockMovement> RecordStockInAsync(int productId, int warehouseId, int quantity, string? reference, string? notes, string? createdBy)
        {
            var stockMovement = new StockMovement
            {
                ProductId = productId,
                WarehouseId = warehouseId,
                MovementType = "In",
                Quantity = quantity,
                Reference = reference,
                Notes = notes,
                CreatedBy = createdBy,
                CreatedDate = DateTime.UtcNow
            };

            return await _stockMovementRepository.AddAsync(stockMovement);
        }

        public async Task<StockMovement> RecordStockOutAsync(int productId, int warehouseId, int quantity, string? reference, string? notes, string? createdBy)
        {
            var currentStock = await GetCurrentStockAsync(productId, warehouseId);
            if (currentStock < quantity)
            {
                throw new InvalidOperationException($"Insufficient stock. Current stock: {currentStock}, Requested: {quantity}");
            }

            var stockMovement = new StockMovement
            {
                ProductId = productId,
                WarehouseId = warehouseId,
                MovementType = "Out",
                Quantity = -quantity,
                Reference = reference,
                Notes = notes,
                CreatedBy = createdBy,
                CreatedDate = DateTime.UtcNow
            };

            return await _stockMovementRepository.AddAsync(stockMovement);
        }

        public async Task<int> GetCurrentStockAsync(int productId, int warehouseId)
        {
            var stock = await _context.StockMovements
                .Where(sm => sm.ProductId == productId && sm.WarehouseId == warehouseId)
                .SumAsync(sm => sm.Quantity);

            return stock;
        }
    }
}
