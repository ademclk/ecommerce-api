using ECommerceAPI.Application.Abstractions;
using ECommerceAPI.Persistence.Concretes;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerceAPI.Persistence;

public static class ServiceRegistration
{
    public static void AddPersistence(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<IProductService, ProductService>();
    }
}