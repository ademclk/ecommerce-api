using ECommerceAPI.Application.Repositories.File;
using ECommerceAPI.Persistence.Contexts;
using E = ECommerceAPI.Domain.Entities;

namespace ECommerceAPI.Persistence.Repositories.File;

public class FileReadRepository : ReadRepository<E::File>, IFileReadRepository
{
    public FileReadRepository(ECommerceApiDbContext context) : base(context)
    {
    }
}
