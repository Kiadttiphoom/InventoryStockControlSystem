using AutoMapper;
using InventoryStockControlSystem.Models;
using InventoryStockControlSystem.ViewModels;

namespace InventoryStockControlSystem.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductViewModel>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category!.Name));
            CreateMap<ProductViewModel, Product>();

            CreateMap<Category, CategoryViewModel>();
            CreateMap<CategoryViewModel, Category>();

            CreateMap<Warehouse, WarehouseViewModel>();
            CreateMap<WarehouseViewModel, Warehouse>();

            CreateMap<StockMovement, StockMovementViewModel>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product!.Name))
                .ForMember(dest => dest.WarehouseName, opt => opt.MapFrom(src => src.Warehouse!.Name));
            CreateMap<StockMovementViewModel, StockMovement>();

            CreateMap<AuditLog, AuditLogViewModel>();
        }
    }

    public class AuditLogViewModel
    {
        public int Id { get; set; }
        public string EntityName { get; set; } = null!;
        public int? EntityId { get; set; }
        public string Action { get; set; } = null!;
        public string? Changes { get; set; }
        public string? UserId { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
