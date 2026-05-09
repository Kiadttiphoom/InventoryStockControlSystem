using InventoryStockControlSystem.Data;
using InventoryStockControlSystem.Models;
using InventoryStockControlSystem.Repositories;
using Microsoft.EntityFrameworkCore;

namespace InventoryStockControlSystem.Services
{
    public class ProductService : IProductService
    {
        private readonly IRepository<Product> _productRepository;
        private readonly ApplicationDbContext _context;

        public ProductService(IRepository<Product> productRepository, ApplicationDbContext context)
        {
            _productRepository = productRepository;
            _context = context;
        }

        public async Task<Product?> GetProductByIdAsync(int id)
        {
            return await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _context.Products
                .Include(p => p.Category)
                .Where(p => p.IsActive)
                .ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsByCategoryAsync(int categoryId)
        {
            return await _context.Products
                .Where(p => p.CategoryId == categoryId && p.IsActive)
                .ToListAsync();
        }

        public async Task<Product> CreateProductAsync(Product product)
        {
            product.CreatedDate = DateTime.UtcNow;
            return await _productRepository.AddAsync(product);
        }

        public async Task UpdateProductAsync(Product product)
        {
            product.UpdatedDate = DateTime.UtcNow;
            await _productRepository.UpdateAsync(product);
        }

        public async Task DeleteProductAsync(int id)
        {
            var product = await GetProductByIdAsync(id);
            if (product != null)
            {
                await _productRepository.DeleteAsync(product);
            }
        }

        public async Task<IEnumerable<Product>> GetLowStockProductsAsync()
        {
            var movements = await _context.StockMovements
                .GroupBy(sm => sm.ProductId)
                .Select(g => new
                {
                    ProductId = g.Key,
                    TotalStock = g.Sum(sm => sm.Quantity)
                })
                .ToListAsync();

            var lowStockProductIds = movements
                .Where(m => m.TotalStock <= 0)
                .Select(m => m.ProductId)
                .ToList();

            return await _context.Products
                .Where(p => lowStockProductIds.Contains(p.Id) && p.IsActive)
                .ToListAsync();
        }
    }
}
