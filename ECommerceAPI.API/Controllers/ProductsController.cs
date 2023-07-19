using System.Net;
using ECommerceAPI.Application.Abstractions.Storage;
using ECommerceAPI.Application.Repositories.File;
using ECommerceAPI.Application.Repositories.InvoiceFile;
using ECommerceAPI.Application.Repositories.Product;
using ECommerceAPI.Application.Repositories.ProductImageFile;
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
    private readonly IStorageService _storageService;
    private readonly IFileReadRepository _fileReadRepository;
    private readonly IFileWriteRepository _fileWriteRepository;
    private readonly IProductImageFileReadRepository _productImageFileReadRepository;
    private readonly IProductImageFileWriteRepository _productImageFileWriteRepository;
    private readonly IInvoiceFileReadRepository _invoiceFileReadRepository;
    private readonly IInvoiceFileWriteRepository _invoiceFileWriteRepository;

    public ProductsController(
        IProductWriteRepository productWriteRepository,
        IProductReadRepository productReadRepository,
        IFileReadRepository fileReadRepository,
        IFileWriteRepository fileWriteRepository,
        IProductImageFileReadRepository productImageFileReadRepository,
        IProductImageFileWriteRepository productImageFileWriteRepository,
        IInvoiceFileReadRepository invoiceFileReadRepository,
        IInvoiceFileWriteRepository invoiceFileWriteRepository,
        IStorageService storageService)
    {
        _productWriteRepository = productWriteRepository;
        _productReadRepository = productReadRepository;
        _fileReadRepository = fileReadRepository;
        _fileWriteRepository = fileWriteRepository;
        _productImageFileReadRepository = productImageFileReadRepository;
        _productImageFileWriteRepository = productImageFileWriteRepository;
        _invoiceFileReadRepository = invoiceFileReadRepository;
        _invoiceFileWriteRepository = invoiceFileWriteRepository;
        _storageService = storageService;
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
            Stock = model.Stock
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
        product.Stock = model.Stock;

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

    [HttpPost("[action]/{id}")]
    public async Task<IActionResult> Upload(string id)
    {
        var fileInfoList = await _storageService.UploadAsync("images", Request.Form.Files);

        var product = await _productReadRepository.GetByIdAsync(id);

        await _productImageFileWriteRepository.AddRangeAsync(fileInfoList.Select(f => new ProductImageFile()
        {
            FileName = f.fileName,
            Path = f.pathOrContainerName,
            Storage = _storageService.Storage,
            Products = new List<Product> { product }
        }).ToList());

        await _productImageFileWriteRepository.SaveChangesAsync();

        return Ok();
    }
}