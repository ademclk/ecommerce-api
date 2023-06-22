using ECommerceAPI.Application.Repositories.Customer;
using ECommerceAPI.Persistence.Contexts;

namespace ECommerceAPI.Persistence.Repositories.Customer;

public class CustomerReadRepository : ReadRepository<Domain.Entities.Customer>, ICustomerReadRepository 
{
    public CustomerReadRepository(ECommerceApiDbContext context) : base(context)
    {
    }
}