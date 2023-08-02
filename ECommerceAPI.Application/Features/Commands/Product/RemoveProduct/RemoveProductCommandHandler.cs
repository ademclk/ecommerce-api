using System;
using ECommerceAPI.Application.Repositories.Product;
using MediatR;

namespace ECommerceAPI.Application.Features.Commands.Product.RemoveProduct;

public class RemoveProductCommandHandler : IRequestHandler<RemoveProductCommandRequest, RemoveProductCommandResponse>
{
    private readonly IProductWriteRepository _productWriteRepository;

    public RemoveProductCommandHandler(IProductWriteRepository productWriteRepository)
    {
        _productWriteRepository = productWriteRepository;
    }

    public async Task<RemoveProductCommandResponse> Handle(RemoveProductCommandRequest request, CancellationToken cancellationToken)
    {
        await _productWriteRepository.RemoveAsync(request.Id);
        await _productWriteRepository.SaveChangesAsync();
        return new();
    }
}