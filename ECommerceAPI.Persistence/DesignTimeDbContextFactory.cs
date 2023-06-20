using ECommerceAPI.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace ECommerceAPI.Persistence;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ECommerceApiDbContext>
{
    public ECommerceApiDbContext CreateDbContext(string[] args)
    {
        DbContextOptionsBuilder<ECommerceApiDbContext> dbContextOptionsBuilder = new();
        dbContextOptionsBuilder.UseNpgsql(Configuration.ConnectionString);
        return new ECommerceApiDbContext(dbContextOptionsBuilder.Options);
    }
}