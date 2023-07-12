using ECommerceAPI.Application.Repositories.ProductImageFile;
using ECommerceAPI.Persistence.Contexts;
using E = ECommerceAPI.Domain.Entities;

namespace ECommerceAPI.Persistence.Repositories.ProductImageFile;

public class ProductImageFileWriteRepository : WriteRepository<E::ProductImageFile>, IProductImageFileWriteRepository
{
    public ProductImageFileWriteRepository(ECommerceApiDbContext context) : base(context)
    {
    }
}


