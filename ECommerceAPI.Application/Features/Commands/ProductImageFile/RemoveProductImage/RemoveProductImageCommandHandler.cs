using System;
using ECommerceAPI.Application.Repositories.Product;
using ECommerceAPI.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;

namespace ECommerceAPI.Application.Features.Commands.ProductImageFile.RemoveProductImage;

public class RemoveProductImageCommandHandler : IRequestHandler<RemoveProductImageCommandRequest, RemoveProductImageCommandResponse>
{
    private readonly IProductReadRepository _productReadRepository;
    private readonly IProductWriteRepository _productWriteRepository;

    public RemoveProductImageCommandHandler(IProductReadRepository productReadRepository, IProductWriteRepository productWriteRepository)
    {
        _productReadRepository = productReadRepository;
        _productWriteRepository = productWriteRepository;
    }

    public async Task<RemoveProductImageCommandResponse> Handle(RemoveProductImageCommandRequest request, CancellationToken cancellationToken)
    {
        var product = await _productReadRepository.Table.Include(p => p.ProductImageFiles)
        .FirstOrDefaultAsync(p => p.Id == Guid.Parse(request.ProductId));

        var productImageFile = product.ProductImageFiles.FirstOrDefault(i => i.Id == Guid.Parse(request.ImageId));

        product.ProductImageFiles.Remove(productImageFile);

        await _productWriteRepository.SaveChangesAsync();
        return new();
    }
}
