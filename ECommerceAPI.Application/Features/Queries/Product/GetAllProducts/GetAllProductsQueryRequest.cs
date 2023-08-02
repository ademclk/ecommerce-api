using ECommerceAPI.Application.RequestParameters;
using MediatR;

namespace ECommerceAPI.Application.Features.Queries.Product.GetAllProducts;

public class GetAllProductsQueryRequest : IRequest<GetAllProductsQueryResponse>
{
    // public Pagination Pagination { get; set; }
    public int Page { get; init; } = 0;
    public int Size { get; init; } = 5;
}

