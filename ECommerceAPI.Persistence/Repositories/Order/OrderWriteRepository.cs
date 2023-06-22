using ECommerceAPI.Application.Repositories.Order;
using ECommerceAPI.Persistence.Contexts;

namespace ECommerceAPI.Persistence.Repositories.Order;

public class OrderWriteRepository : WriteRepository<Domain.Entities.Order>, IOrderWriteRepository
{
    public OrderWriteRepository(ECommerceApiDbContext context) : base(context)
    {
    }
}