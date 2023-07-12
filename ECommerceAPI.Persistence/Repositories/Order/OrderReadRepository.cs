using ECommerceAPI.Application.Repositories.Order;
using ECommerceAPI.Persistence.Contexts;
using E = ECommerceAPI.Domain.Entities;

namespace ECommerceAPI.Persistence.Repositories.Order;

public class OrderReadRepository : ReadRepository<E::Order>, IOrderReadRepository
{
    public OrderReadRepository(ECommerceApiDbContext context) : base(context)
    {
    }
}