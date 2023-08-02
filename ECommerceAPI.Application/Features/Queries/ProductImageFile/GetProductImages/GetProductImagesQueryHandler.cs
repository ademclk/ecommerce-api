using ECommerceAPI.Application.Repositories.Product;
using MediatR;
using Microsoft.EntityFrameworkCore;
using E = ECommerceAPI.Domain.Entities;

namespace ECommerceAPI.Application.Features.Queries.ProductImageFile.GetProductImages;

public class GetProductImagesQueryHandler : IRequestHandler<GetProductImagesQueryRequest, List<GetProductImagesQueryResponse>>
{
    private readonly IProductReadRepository _productReadRepository;

    public async Task<List<GetProductImagesQueryResponse>> Handle(GetProductImagesQueryRequest request, CancellationToken cancellationToken)
    {
        var product = await _productReadRepository.Table.Include(p => p.ProductImageFiles)
             .FirstOrDefaultAsync(p => p.Id == Guid.Parse(request.Id));

        return product?.ProductImageFiles.Select(p => new GetProductImagesQueryResponse
        {
            Id = p.Id,
            FileName = p.FileName,
            Path = p.Path
        }).ToList();
    }
}