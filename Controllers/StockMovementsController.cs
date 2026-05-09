using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using InventoryStockControlSystem.Services;
using InventoryStockControlSystem.ViewModels;

namespace InventoryStockControlSystem.Controllers
{
    [Authorize]
    public class StockMovementsController : Controller
    {
        private readonly IStockMovementService _stockMovementService;
        private readonly IProductService _productService;
        private readonly IWarehouseService _warehouseService;
        private readonly IMapper _mapper;

        public StockMovementsController(
            IStockMovementService stockMovementService,
            IProductService productService,
            IWarehouseService warehouseService,
            IMapper mapper)
        {
            _stockMovementService = stockMovementService;
            _productService = productService;
            _warehouseService = warehouseService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var movements = await _stockMovementService.GetAllStockMovementsAsync();
            var viewModels = _mapper.Map<List<StockMovementViewModel>>(movements);
            return View(viewModels);
        }

        public async Task<IActionResult> Create()
        {
            var products = await _productService.GetAllProductsAsync();
            var warehouses = await _warehouseService.GetAllWarehousesAsync();
            ViewBag.Products = products;
            ViewBag.Warehouses = warehouses;
            ViewBag.MovementTypes = new[] { "In", "Out", "Adjustment" };
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(string movementType, int productId, int warehouseId, int quantity, string? reference, string? notes)
        {
            try
            {
                var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

                if (movementType == "In")
                {
                    await _stockMovementService.RecordStockInAsync(productId, warehouseId, quantity, reference, notes, userId);
                }
                else if (movementType == "Out")
                {
                    await _stockMovementService.RecordStockOutAsync(productId, warehouseId, quantity, reference, notes, userId);
                }
                else if (movementType == "Adjustment")
                {
                    // For adjustment, treat as In/Out based on sign
                    if (quantity > 0)
                        await _stockMovementService.RecordStockInAsync(productId, warehouseId, quantity, reference, notes, userId);
                    else
                        await _stockMovementService.RecordStockOutAsync(productId, warehouseId, Math.Abs(quantity), reference, notes, userId);
                }

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Error recording stock movement: {ex.Message}");
                var products = await _productService.GetAllProductsAsync();
                var warehouses = await _warehouseService.GetAllWarehousesAsync();
                ViewBag.Products = products;
                ViewBag.Warehouses = warehouses;
                ViewBag.MovementTypes = new[] { "In", "Out", "Adjustment" };
                return View();
            }
        }

        public async Task<IActionResult> ByProduct(int productId)
        {
            var movements = await _stockMovementService.GetStockMovementsByProductAsync(productId);
            var viewModels = _mapper.Map<List<StockMovementViewModel>>(movements);
            var product = await _productService.GetProductByIdAsync(productId);
            ViewBag.ProductName = product?.Name;
            return View("Index", viewModels);
        }

        public async Task<IActionResult> ByWarehouse(int warehouseId)
        {
            var movements = await _stockMovementService.GetStockMovementsByWarehouseAsync(warehouseId);
            var viewModels = _mapper.Map<List<StockMovementViewModel>>(movements);
            var warehouse = await _warehouseService.GetWarehouseByIdAsync(warehouseId);
            ViewBag.WarehouseName = warehouse?.Name;
            return View("Index", viewModels);
        }
    }
}
