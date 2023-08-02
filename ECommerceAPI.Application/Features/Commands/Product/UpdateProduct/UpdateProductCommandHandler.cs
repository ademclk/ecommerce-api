using System;
using ECommerceAPI.Application.Features.Queries.Product.GetProductById;
using ECommerceAPI.Application.Repositories.Product;
using MediatR;
using E = ECommerceAPI.Domain.Entities;

namespace ECommerceAPI.Application.Features.Commands.Product.UpdateProduct;

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommandRequest, UpdateProductCommandResponse>
{
    private readonly IProductReadRepository _productReadRepository;
    private readonly IProductWriteRepository _productWriteRepository;

    public UpdateProductCommandHandler(IProductReadRepository productReadRepository, IProductWriteRepository productWriteRepository)
    {
        _productReadRepository = productReadRepository;
        _productWriteRepository = productWriteRepository;
    }

    public async Task<UpdateProductCommandResponse> Handle(UpdateProductCommandRequest request, CancellationToken cancellationToken)
    {
        E.Product product = await _productReadRepository.GetByIdAsync(request.Id);

        product.Name = request.Name;
        product.Description = request.Description;
        product.Price = request.Price;
        product.Stock = request.Stock;

        await _productWriteRepository.SaveChangesAsync();
        return new();
    }
}

