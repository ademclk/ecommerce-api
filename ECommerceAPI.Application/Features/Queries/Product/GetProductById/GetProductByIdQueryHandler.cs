using System;
using ECommerceAPI.Application.Repositories.Product;
using MediatR;
using E = ECommerceAPI.Domain.Entities;

namespace ECommerceAPI.Application.Features.Queries.Product.GetProductById;

public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQueryRequest, GetProductByIdQueryResponse>
{
    private readonly IProductReadRepository _productReadRepository;

    public GetProductByIdQueryHandler(IProductReadRepository productReadRepository)
    {
        _productReadRepository = productReadRepository;
    }

    public async Task<GetProductByIdQueryResponse> Handle(GetProductByIdQueryRequest request, CancellationToken cancellationToken)
    {
        E.Product product =  await _productReadRepository.GetByIdAsync(request.Id, false);
        return new()
        {
            Name = product.Name,
            Description = product.Description,
            Stock = product.Stock,
            Price = product.Price
        };
    }
}

