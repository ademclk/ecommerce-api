using ECommerceAPI.Application.Abstractions.Storage;
using ECommerceAPI.Infrastructure.Services;
using ECommerceAPI.Infrastructure.Services.Storage;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerceAPI.Infrastructure;

public static class ServiceRegistration
{
    public static void AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IStorageService, StorageService>();
    }

    public static void AddStorage<T>(this IServiceCollection services) where T : Storage, IStorage
    {
        services.AddScoped<IStorage, T>();
    }
}


