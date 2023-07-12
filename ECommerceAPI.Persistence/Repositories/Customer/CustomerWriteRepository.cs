using ECommerceAPI.Application.Repositories.Customer;
using ECommerceAPI.Persistence.Contexts;
using E = ECommerceAPI.Domain.Entities;

namespace ECommerceAPI.Persistence.Repositories.Customer;

public class CustomerWriteRepository : WriteRepository<E::Customer>, ICustomerWriteRepository
{
    public CustomerWriteRepository(ECommerceApiDbContext context) : base(context)
    {
    }
}