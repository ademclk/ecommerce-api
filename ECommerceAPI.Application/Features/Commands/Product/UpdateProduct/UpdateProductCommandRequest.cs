﻿using System;
using ECommerceAPI.Application.Features.Queries.Product.GetProductById;
using MediatR;

namespace ECommerceAPI.Application.Features.Commands.Product.UpdateProduct;

public class UpdateProductCommandRequest : IRequest<UpdateProductCommandResponse>
{
    public string? Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public int Stock { get; set; }
    public float Price { get; set; }
}
