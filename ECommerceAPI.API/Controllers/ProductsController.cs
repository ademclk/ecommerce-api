using System.Net;
using ECommerceAPI.Application.Repositories.Product;
using ECommerceAPI.Domain.Entities;
using ECommerceAPI.Persistence.ViewModels.Products;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceAPI.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductWriteRepository _productWriteRepository;
    private readonly IProductReadRepository _productReadRepository;

    public ProductsController(IProductWriteRepository productWriteRepository, IProductReadRepository productReadRepository)
    {
        _productWriteRepository = productWriteRepository;
        _productReadRepository = productReadRepository;
    }

    [HttpGet]
    public IActionResult Get()
    {
        return Ok(_productReadRepository.GetAll(false));
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(string id)
    {
        return Ok(await _productReadRepository.GetByIdAsync(id, false));
    }

    [HttpPost]
    public async Task<IActionResult> Post(VmCreateProduct model)
    {
        await _productWriteRepository.AddAsync(new Product
        {
            Name = model.Name,
            Description = model.Description,
            Price = model.Price,
            Quantity = model.Quantity
        });

        await _productWriteRepository.SaveChangesAsync();

        return StatusCode((int)HttpStatusCode.Created);
    }

    [HttpPut]
    public async Task<IActionResult> Put(VmUpdateProduct model)
    {
        Product product = await _productReadRepository.GetByIdAsync(model.Id);

        product.Name = model.Name;
        product.Description = model.Description;
        product.Price = model.Price;
        product.Quantity = model.Quantity;
        
        await _productWriteRepository.SaveChangesAsync();
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        await _productWriteRepository.RemoveAsync(id);
        await _productWriteRepository.SaveChangesAsync();
        return Ok();
    }
}