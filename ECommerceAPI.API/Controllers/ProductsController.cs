using System.Net;
using ECommerceAPI.Application.Repositories.Product;
using ECommerceAPI.Application.RequestParameters;
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
    private readonly IWebHostEnvironment _webHostEnvironment;

    public ProductsController(IProductWriteRepository productWriteRepository, IProductReadRepository productReadRepository, IWebHostEnvironment webHostEnvironment)
    {
        _productWriteRepository = productWriteRepository;
        _productReadRepository = productReadRepository;
        _webHostEnvironment = webHostEnvironment;
    }

    [HttpGet]
    public IActionResult Get([FromQuery] Pagination pagination)
    {
        var totalCount = _productReadRepository.GetAll(false).Count();
        var products = _productReadRepository.GetAll(false)
            .OrderBy(product => product.CreatedAt)
            .Skip(pagination.Page * pagination.Size)
            .Take(pagination.Size)
            .Select(p => new
            {
                p.Id,
                p.Name,
                p.Price,
                p.Stock,
                p.UpdatedAt,
                p.CreatedAt
            }).ToList();

        return Ok(new
        {
            totalCount,
            products
        });
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
            Stock = model.Quantity
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
        product.Stock = model.Quantity;
        
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

    [HttpPost("[action]")]
    public async Task<IActionResult> Upload()
    {
        string uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, "resource/product-images");

        string guid = Guid.NewGuid().ToString().Replace("-", "");

        if (!Directory.Exists(uploadPath))
        {
            Directory.CreateDirectory(uploadPath);
        }

        foreach (var file in Request.Form.Files)
        {
            string fullPath = Path.Combine(uploadPath, $"{guid}{Path.GetExtension(file.FileName)}");

            using FileStream fileStream = new(fullPath, FileMode.Create, FileAccess.Write, FileShare.None, 1024 * 1024, useAsync: false);

            await file.CopyToAsync(fileStream);
            await fileStream.FlushAsync();
        }

        return Ok();
    }
}