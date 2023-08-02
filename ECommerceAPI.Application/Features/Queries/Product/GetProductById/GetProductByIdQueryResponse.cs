using System;
namespace ECommerceAPI.Application.Features.Queries.Product.GetProductById;

public class GetProductByIdQueryResponse
{
    public string Name { get; set; }
    public string Description { get; set; }
    public int Stock { get; set; }
    public float Price { get; set; }
}

