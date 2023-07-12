using ECommerceAPI.Application.Repositories.Customer;
using ECommerceAPI.Application.Repositories.File;
using ECommerceAPI.Application.Repositories.InvoiceFile;
using ECommerceAPI.Application.Repositories.Order;
using ECommerceAPI.Application.Repositories.Product;
using ECommerceAPI.Application.Repositories.ProductImageFile;
using ECommerceAPI.Persistence.Contexts;
using ECommerceAPI.Persistence.Repositories.Customer;
using ECommerceAPI.Persistence.Repositories.File;
using ECommerceAPI.Persistence.Repositories.InvoiceFile;
using ECommerceAPI.Persistence.Repositories.Order;
using ECommerceAPI.Persistence.Repositories.Product;
using ECommerceAPI.Persistence.Repositories.ProductImageFile;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerceAPI.Persistence;

public static class ServiceRegistration
{
    public static void AddPersistence(this IServiceCollection service)
    {
        service.AddDbContext<ECommerceApiDbContext>(options =>
            options.UseNpgsql(Configuration.ConnectionString));
        service.AddScoped<ICustomerReadRepository, CustomerReadRepository>();
        service.AddScoped<ICustomerWriteRepository, CustomerWriteRepository>();
        service.AddScoped<IOrderReadRepository, OrderReadRepository>();
        service.AddScoped<IOrderWriteRepository, OrderWriteRepository>();
        service.AddScoped<IProductReadRepository, ProductReadRepository>();
        service.AddScoped<IProductWriteRepository, ProductWriteRepository>();
        service.AddScoped<IFileReadRepository, FileReadRepository>();
        service.AddScoped<IFileWriteRepository, FileWriteRepository>();
        service.AddScoped<IProductImageFileReadRepository, ProductImageFileReadRepository>();
        service.AddScoped<IProductImageFileWriteRepository, ProductImageFileWriteRepository>();
        service.AddScoped<IInvoiceFileReadRepository, InvoiceFileReadRepository>();
        service.AddScoped<IInvoiceFileWriteRepository, InvoiceFileWriteRepository>();
    }
}