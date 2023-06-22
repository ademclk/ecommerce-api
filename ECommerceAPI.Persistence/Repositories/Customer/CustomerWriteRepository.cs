using ECommerceAPI.Application.Repositories.Customer;
using ECommerceAPI.Persistence.Contexts;

namespace ECommerceAPI.Persistence.Repositories.Customer;

public class CustomerWriteRepository : WriteRepository<Domain.Entities.Customer>, ICustomerWriteRepository
{
    public CustomerWriteRepository(ECommerceApiDbContext context) : base(context)
    {
    }
}