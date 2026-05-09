using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using InventoryStockControlSystem.Services;
using InventoryStockControlSystem.ViewModels;

namespace InventoryStockControlSystem.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        private readonly IProductService _productService;
        private readonly IStockMovementService _stockMovementService;
        private readonly ICategoryService _categoryService;
        private readonly IWarehouseService _warehouseService;

        public DashboardController(
            IProductService productService,
            IStockMovementService stockMovementService,
            ICategoryService categoryService,
            IWarehouseService warehouseService)
        {
            _productService = productService;
            _stockMovementService = stockMovementService;
            _categoryService = categoryService;
            _warehouseService = warehouseService;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var products = await _productService.GetAllProductsAsync();
                var categories = await _categoryService.GetAllCategoriesAsync();
                var warehouses = await _warehouseService.GetAllWarehousesAsync();
                var movements = await _stockMovementService.GetAllStockMovementsAsync();
                var lowStockProducts = await _productService.GetLowStockProductsAsync();

                var viewModel = new DashboardViewModel
                {
                    TotalProducts = products.Count(),
                    TotalCategories = categories.Count(),
                    TotalWarehouses = warehouses.Count(),
                    LowStockProducts = lowStockProducts.Count(),
                    RecentMovements = movements.Take(10)
                        .Select(m => new StockMovementViewModel
                        {
                            Id = m.Id,
                            ProductId = m.ProductId,
                            ProductName = m.Product?.Name,
                            WarehouseId = m.WarehouseId,
                            WarehouseName = m.Warehouse?.Name,
                            MovementType = m.MovementType,
                            Quantity = m.Quantity,
                            Reference = m.Reference,
                            CreatedDate = m.CreatedDate
                        }).ToList()
                };

                return View(viewModel);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Error loading dashboard: {ex.Message}");
                return View(new DashboardViewModel());
            }
        }
    }
}
