namespace InventoryStockControlSystem.Models
{
    public class AuditLog
    {
        public int Id { get; set; }
        public string EntityName { get; set; } = null!;
        public int? EntityId { get; set; }
        public string Action { get; set; } = null!; // "Create", "Update", "Delete"
        public string? Changes { get; set; } // JSON serialized changes
        public int? UserId { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        // Foreign keys
        public ApplicationUser? User { get; set; }
    }
}
