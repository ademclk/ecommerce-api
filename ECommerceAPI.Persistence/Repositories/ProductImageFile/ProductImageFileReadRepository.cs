using ECommerceAPI.Application.Repositories.ProductImageFile;
using ECommerceAPI.Persistence.Contexts;
using E = ECommerceAPI.Domain.Entities;

namespace ECommerceAPI.Persistence.Repositories.ProductImageFile;

public class ProductImageFileReadRepository : ReadRepository<E::ProductImageFile>, IProductImageFileReadRepository
{
    public ProductImageFileReadRepository(ECommerceApiDbContext context) : base(context)
    {
    }
}