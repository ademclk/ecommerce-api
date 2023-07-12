using System;
using ECommerceAPI.Application.Repositories.InvoiceFile;
using ECommerceAPI.Persistence.Contexts;
using E = ECommerceAPI.Domain.Entities;

namespace ECommerceAPI.Persistence.Repositories.InvoiceFile;

public class InvoiceFileWriteRepository : WriteRepository<E::InvoiceFile>, IInvoiceFileWriteRepository
{
    public InvoiceFileWriteRepository(ECommerceApiDbContext context) : base(context)
    {
    }
}


