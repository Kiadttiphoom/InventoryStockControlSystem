using FluentValidation;
using InventoryStockControlSystem.ViewModels;

namespace InventoryStockControlSystem.Validators
{
    public class ProductValidator : AbstractValidator<ProductViewModel>
    {
        public ProductValidator()
        {
            RuleFor(x => x.Code)
                .NotEmpty().WithMessage("Product code is required")
                .Length(1, 50).WithMessage("Product code must be between 1 and 50 characters");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Product name is required")
                .Length(1, 200).WithMessage("Product name must be between 1 and 200 characters");

            RuleFor(x => x.CategoryId)
                .GreaterThan(0).WithMessage("A valid category must be selected");

            RuleFor(x => x.UnitPrice)
                .GreaterThanOrEqualTo(0).WithMessage("Unit price cannot be negative");

            RuleFor(x => x.ReorderLevel)
                .GreaterThanOrEqualTo(0).WithMessage("Reorder level cannot be negative");

            RuleFor(x => x.ReorderQuantity)
                .GreaterThan(0).WithMessage("Reorder quantity must be greater than 0");
        }
    }

    public class CategoryValidator : AbstractValidator<CategoryViewModel>
    {
        public CategoryValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Category name is required")
                .Length(1, 100).WithMessage("Category name must be between 1 and 100 characters");
        }
    }

    public class WarehouseValidator : AbstractValidator<WarehouseViewModel>
    {
        public WarehouseValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Warehouse name is required")
                .Length(1, 100).WithMessage("Warehouse name must be between 1 and 100 characters");
        }
    }
}
