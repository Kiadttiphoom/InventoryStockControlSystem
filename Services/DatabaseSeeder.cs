using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using InventoryStockControlSystem.Data;
using InventoryStockControlSystem.Models;

namespace InventoryStockControlSystem.Services
{
    public class DatabaseSeeder
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole<int>> _roleManager;

        public DatabaseSeeder(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole<int>> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task SeedAsync()
        {
            await _context.Database.MigrateAsync();

            // Seed Roles
            var roles = new[] { "Admin", "Manager", "Staff" };
            foreach (var role in roles)
            {
                if (!await _roleManager.RoleExistsAsync(role))
                    await _roleManager.CreateAsync(new IdentityRole<int> { Name = role });
            }

            // Seed Admin User
            const string adminEmail = "admin@inventory.com";
            const string adminPassword = "Admin@123456";

            var adminUser = await _userManager.FindByEmailAsync(adminEmail);
            if (adminUser == null)
            {
                adminUser = new ApplicationUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true
                };

                var result = await _userManager.CreateAsync(adminUser, adminPassword);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(adminUser, "Admin");
                }
            }

            // Seed Categories
            if (!_context.Categories.Any())
            {
                var categories = new[]
                {
                    new Category { Name = "Electronics", Description = "Electronic products and devices" },
                    new Category { Name = "Clothing", Description = "Apparel and fashion items" },
                    new Category { Name = "Books", Description = "Books and publications" },
                    new Category { Name = "Home & Garden", Description = "Home and garden supplies" },
                    new Category { Name = "Sports & Outdoors", Description = "Sports and outdoor equipment" }
                };

                _context.Categories.AddRange(categories);
                await _context.SaveChangesAsync();
            }

            // Seed Warehouses
            if (!_context.Warehouses.Any())
            {
                var warehouses = new[]
                {
                    new Warehouse { Name = "Main Warehouse", Location = "Downtown", Contact = "555-0001" },
                    new Warehouse { Name = "Secondary Warehouse", Location = "Uptown", Contact = "555-0002" },
                    new Warehouse { Name = "Distribution Center", Location = "Industrial Area", Contact = "555-0003" }
                };

                _context.Warehouses.AddRange(warehouses);
                await _context.SaveChangesAsync();
            }
        }
    }
}
