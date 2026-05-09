using Microsoft.AspNetCore.Identity;

namespace InventoryStockControlSystem.Models
{
    public class ApplicationUser : IdentityUser<int>
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public bool IsActive { get; set; } = true;

        // Navigation properties
        public ICollection<AuditLog>? AuditLogs { get; set; }
    }
}
