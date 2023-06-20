using ECommerceAPI.Application.Abstractions;
using ECommerceAPI.Domain.Entities;

namespace ECommerceAPI.Persistence.Concretes;

public class ProductService : IProductService
{
    public List<Product> GetAll()
        => new()
        {
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Product 1",
                Description = "Description 1",
                Price = 10,
                Quantity = 50,
                CreatedAt = DateTime.UtcNow
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Product 2",
                Description = "Description 2",
                Price = 20,
                Quantity = 50,
                CreatedAt = DateTime.UtcNow
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Product 3",
                Description = "Description 3",
                Price = 30,
                Quantity = 50,
                CreatedAt = DateTime.UtcNow
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Product 4",
                Description = "Description 4",
                Price = 40,
                Quantity = 50,
                CreatedAt = DateTime.UtcNow
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Product 5",
                Description = "Description 5",
                Price = 50,
                Quantity = 50,
                CreatedAt = DateTime.UtcNow
            }
        };
}