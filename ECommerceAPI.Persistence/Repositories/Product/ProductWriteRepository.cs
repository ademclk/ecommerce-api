using ECommerceAPI.Application.Repositories.Product;
using ECommerceAPI.Persistence.Contexts;
using E = ECommerceAPI.Domain.Entities;

namespace ECommerceAPI.Persistence.Repositories.Product;

public class ProductWriteRepository : WriteRepository<E::Product>, IProductWriteRepository
{
    public ProductWriteRepository(ECommerceApiDbContext context) : base(context)
    {
    }
}