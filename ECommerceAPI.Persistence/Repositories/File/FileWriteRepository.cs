using System;
using ECommerceAPI.Application.Repositories.File;
using ECommerceAPI.Persistence.Contexts;
using E = ECommerceAPI.Domain.Entities;

namespace ECommerceAPI.Persistence.Repositories.File;

public class FileWriteRepository : WriteRepository<E::File>, IFileWriteRepository
{
    public FileWriteRepository(ECommerceApiDbContext context) : base(context)
    {
    }
}


