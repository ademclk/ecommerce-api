using ECommerceAPI.Application.Repositories.Customer;
using ECommerceAPI.Persistence.Contexts;
using E = ECommerceAPI.Domain.Entities;

namespace ECommerceAPI.Persistence.Repositories.Customer;

public class CustomerReadRepository : ReadRepository<E::Customer>, ICustomerReadRepository 
{
    public CustomerReadRepository(ECommerceApiDbContext context) : base(context)
    {
    }
}