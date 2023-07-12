using System;
using ECommerceAPI.Application.Repositories.InvoiceFile;
using ECommerceAPI.Persistence.Contexts;
using E = ECommerceAPI.Domain.Entities;

namespace ECommerceAPI.Persistence.Repositories.InvoiceFile;

public class InvoiceFileReadRepository : ReadRepository<E::InvoiceFile>, IInvoiceFileReadRepository
{
    public InvoiceFileReadRepository(ECommerceApiDbContext context) : base(context)
    {
    }
}


