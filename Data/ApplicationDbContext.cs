using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using InventoryStockControlSystem.Models;
using System.Text.Json;

namespace InventoryStockControlSystem.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser, IdentityRole<int>, int>(options)
{
    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Warehouse> Warehouses { get; set; }
    public DbSet<StockMovement> StockMovements { get; set; }
    public DbSet<AuditLog> AuditLogs { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Category configuration
        builder.Entity<Category>()
            .HasKey(c => c.Id);
        builder.Entity<Category>()
            .Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(100);
        builder.Entity<Category>()
            .HasIndex(c => c.Name)
            .IsUnique();
        builder.Entity<Category>()
            .HasMany(c => c.Products)
            .WithOne(p => p.Category)
            .HasForeignKey(p => p.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);

        // Product configuration
        builder.Entity<Product>()
            .HasKey(p => p.Id);
        builder.Entity<Product>()
            .Property(p => p.Code)
            .IsRequired()
            .HasMaxLength(50);
        builder.Entity<Product>()
            .Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(200);
        builder.Entity<Product>()
            .Property(p => p.UnitPrice)
            .HasPrecision(18, 2);
        builder.Entity<Product>()
            .Property(p => p.ReorderLevel)
            .HasPrecision(18, 2);
        builder.Entity<Product>()
            .HasIndex(p => p.Code)
            .IsUnique();
        builder.Entity<Product>()
            .HasMany(p => p.StockMovements)
            .WithOne(sm => sm.Product)
            .HasForeignKey(sm => sm.ProductId)
            .OnDelete(DeleteBehavior.Cascade);

        // Warehouse configuration
        builder.Entity<Warehouse>()
            .HasKey(w => w.Id);
        builder.Entity<Warehouse>()
            .Property(w => w.Name)
            .IsRequired()
            .HasMaxLength(100);
        builder.Entity<Warehouse>()
            .HasIndex(w => w.Name)
            .IsUnique();
        builder.Entity<Warehouse>()
            .HasMany(w => w.StockMovements)
            .WithOne(sm => sm.Warehouse)
            .HasForeignKey(sm => sm.WarehouseId)
            .OnDelete(DeleteBehavior.Restrict);

        // StockMovement configuration
        builder.Entity<StockMovement>()
            .HasKey(sm => sm.Id);
        builder.Entity<StockMovement>()
            .Property(sm => sm.MovementType)
            .IsRequired()
            .HasMaxLength(20);
        builder.Entity<StockMovement>()
            .HasIndex(sm => new { sm.ProductId, sm.WarehouseId, sm.CreatedDate });

        // AuditLog configuration
        builder.Entity<AuditLog>()
            .HasKey(al => al.Id);
        builder.Entity<AuditLog>()
            .Property(al => al.EntityName)
            .IsRequired()
            .HasMaxLength(100);
        builder.Entity<AuditLog>()
            .Property(al => al.Action)
            .IsRequired()
            .HasMaxLength(20);
        builder.Entity<AuditLog>()
            .HasOne(al => al.User)
            .WithMany(u => u.AuditLogs)
            .HasForeignKey(al => al.UserId)
            .OnDelete(DeleteBehavior.SetNull);
        builder.Entity<AuditLog>()
            .HasIndex(al => new { al.EntityName, al.CreatedDate })
            .IsDescending(false, true);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var entries = ChangeTracker.Entries()
            .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified || e.State == EntityState.Deleted)
            .ToList();

        foreach (var entry in entries)
        {
            if (entry.Entity is AuditLog or ApplicationUser)
                continue;

            var auditLog = new AuditLog
            {
                EntityName = entry.Entity.GetType().Name,
                Action = entry.State switch
                {
                    EntityState.Added => "Create",
                    EntityState.Modified => "Update",
                    EntityState.Deleted => "Delete",
                    _ => "Unknown"
                },
                CreatedDate = DateTime.UtcNow,
                Changes = JsonSerializer.Serialize(entry.CurrentValues.Properties.ToDictionary(
                    p => p.Name,
                    p => entry.CurrentValues[p] ?? "null"))
            };

            AuditLogs.Add(auditLog);
        }

        return await base.SaveChangesAsync(cancellationToken);
    }
}
