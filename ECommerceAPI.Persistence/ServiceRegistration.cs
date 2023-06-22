using ECommerceAPI.Application.Repositories.Customer;
using ECommerceAPI.Application.Repositories.Order;
using ECommerceAPI.Application.Repositories.Product;
using ECommerceAPI.Persistence.Contexts;
using ECommerceAPI.Persistence.Repositories.Customer;
using ECommerceAPI.Persistence.Repositories.Order;
using ECommerceAPI.Persistence.Repositories.Product;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerceAPI.Persistence;

public static class ServiceRegistration
{
    public static void AddPersistence(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddDbContext<ECommerceApiDbContext>(options =>
            options.UseNpgsql(Configuration.ConnectionString));
        serviceCollection.AddScoped<ICustomerReadRepository, CustomerReadRepository>();
        serviceCollection.AddScoped<ICustomerWriteRepository, CustomerWriteRepository>();
        serviceCollection.AddScoped<IOrderReadRepository, OrderReadRepository>();
        serviceCollection.AddScoped<IOrderWriteRepository, OrderWriteRepository>();
        serviceCollection.AddScoped<IProductReadRepository, ProductReadRepository>();
        serviceCollection.AddScoped<IProductWriteRepository, ProductWriteRepository>();
    }
}