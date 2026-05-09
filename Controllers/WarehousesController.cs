using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using InventoryStockControlSystem.Services;
using InventoryStockControlSystem.ViewModels;
using InventoryStockControlSystem.Models;
using FluentValidation;

namespace InventoryStockControlSystem.Controllers
{
    [Authorize]
    public class WarehousesController : Controller
    {
        private readonly IWarehouseService _warehouseService;
        private readonly IMapper _mapper;
        private readonly IValidator<WarehouseViewModel> _validator;

        public WarehousesController(
            IWarehouseService warehouseService,
            IMapper mapper,
            IValidator<WarehouseViewModel> validator)
        {
            _warehouseService = warehouseService;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<IActionResult> Index()
        {
            var warehouses = await _warehouseService.GetAllWarehousesAsync();
            var viewModels = _mapper.Map<List<WarehouseViewModel>>(warehouses);
            return View(viewModels);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(WarehouseViewModel viewModel)
        {
            var validationResult = await _validator.ValidateAsync(viewModel);
            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var warehouse = _mapper.Map<Warehouse>(viewModel);
                    await _warehouseService.CreateWarehouseAsync(warehouse);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"Error creating warehouse: {ex.Message}");
                }
            }

            return View(viewModel);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var warehouse = await _warehouseService.GetWarehouseByIdAsync(id);
            if (warehouse == null)
                return NotFound();

            var viewModel = _mapper.Map<WarehouseViewModel>(warehouse);
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, WarehouseViewModel viewModel)
        {
            if (id != viewModel.Id)
                return BadRequest();

            var validationResult = await _validator.ValidateAsync(viewModel);
            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var warehouse = _mapper.Map<Warehouse>(viewModel);
                    await _warehouseService.UpdateWarehouseAsync(warehouse);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"Error updating warehouse: {ex.Message}");
                }
            }

            return View(viewModel);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var warehouse = await _warehouseService.GetWarehouseByIdAsync(id);
            if (warehouse == null)
                return NotFound();

            var viewModel = _mapper.Map<WarehouseViewModel>(warehouse);
            return View(viewModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _warehouseService.DeleteWarehouseAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Error deleting warehouse: {ex.Message}");
                return View();
            }
        }
    }
}
