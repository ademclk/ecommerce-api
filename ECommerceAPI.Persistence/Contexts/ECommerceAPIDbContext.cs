using ECommerceAPI.Domain.Entities;
using ECommerceAPI.Domain.Entities.Common;
using ECommerceAPI.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ECommerceAPI.Persistence.Contexts;

public class ECommerceApiDbContext : IdentityDbContext<AppUser, AppRole, string>
{
    public ECommerceApiDbContext (DbContextOptions<ECommerceApiDbContext> options) : base(options)
    {
    }
    
    public DbSet<Product> Products { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Domain.Entities.File> Files { get; set; }
    public DbSet<ProductImageFile> ProductImageFiles { get; set; }
    public DbSet<InvoiceFile> InvoiceFiles { get; set; }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var entries = ChangeTracker.Entries<BaseEntity>();

        foreach (var data in entries)
        {
            _ = data.State switch
            {
                EntityState.Added => data.Entity.CreatedAt = DateTime.UtcNow,
                EntityState.Modified => data.Entity.UpdatedAt = DateTime.UtcNow,
                _ => DateTime.UtcNow
            };
        }

        return await base.SaveChangesAsync(cancellationToken);
    }
}  