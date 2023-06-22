using ECommerceAPI.Application.Repositories.Product;
using ECommerceAPI.Persistence.Contexts;

namespace ECommerceAPI.Persistence.Repositories.Product;

public class ProductReadRepository : ReadRepository<Domain.Entities.Product>, IProductReadRepository
{
    public ProductReadRepository(ECommerceApiDbContext context) : base(context)
    {
    }
}