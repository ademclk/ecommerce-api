using ECommerceAPI.Application.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceAPI.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }
    
    [HttpGet]
    public IActionResult GetProducts()
    {
        var products = _productService.GetAll();
        return Ok(products);
    }
}