using System.Net;
using ECommerceAPI.Application.Abstractions.Storage;
using ECommerceAPI.Application.Features.Commands.Product.CreateProduct;
using ECommerceAPI.Application.Features.Commands.Product.RemoveProduct;
using ECommerceAPI.Application.Features.Commands.Product.UpdateProduct;
using ECommerceAPI.Application.Features.Commands.ProductImageFile.RemoveProductImage;
using ECommerceAPI.Application.Features.Queries.Product.GetAllProducts;
using ECommerceAPI.Application.Features.Queries.Product.GetProductById;
using ECommerceAPI.Application.Features.Queries.ProductImageFile.GetProductImages;
using ECommerceAPI.Application.Repositories.File;
using ECommerceAPI.Application.Repositories.InvoiceFile;
using ECommerceAPI.Application.Repositories.Product;
using ECommerceAPI.Application.Repositories.ProductImageFile;
using ECommerceAPI.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace ECommerceAPI.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly IProductReadRepository _productReadRepository;
    private readonly IStorageService _storageService;
    private readonly IProductImageFileWriteRepository _productImageFileWriteRepository;
    private readonly IMediator _mediator;

    public ProductsController(
        IProductReadRepository productReadRepository,
        IProductImageFileWriteRepository productImageFileWriteRepository,
        IStorageService storageService,
        IMediator mediator)
    {
        _productReadRepository = productReadRepository;
        _productImageFileWriteRepository = productImageFileWriteRepository;
        _storageService = storageService;
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] GetAllProductsQueryRequest getAllProductsQueryRequest)
    {
        GetAllProductsQueryResponse response = await _mediator.Send(getAllProductsQueryRequest);
        return Ok(response);
    }

    [HttpGet("{Id}")]
    public async Task<IActionResult> Get([FromRoute] GetProductByIdQueryRequest getProductByIdQueryRequest)
    {
        GetProductByIdQueryResponse response = await _mediator.Send(getProductByIdQueryRequest);
        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> Post(CreateProductCommandRequest createProductCommandRequest)
    {
        await _mediator.Send(createProductCommandRequest);
        return StatusCode((int)HttpStatusCode.Created);
    }

    [HttpPut]
    public async Task<IActionResult> Put([FromBody] UpdateProductCommandRequest updateProductCommandRequest)
    {
        UpdateProductCommandResponse response = await _mediator.Send(updateProductCommandRequest);
        return Ok();
    }

    [HttpDelete("{Id}")]
    public async Task<IActionResult> Delete([FromRoute] RemoveProductCommandRequest removeProductCommandRequest)
    {
        RemoveProductCommandResponse response = await _mediator.Send(removeProductCommandRequest);
        return Ok();
    }

    [HttpDelete("[action]/{ProductId}/{ImageId}")]
    public async Task<IActionResult> DeleteImage([FromRoute] RemoveProductImageCommandRequest removeProductImageCommandRequest)
    {
        RemoveProductImageCommandResponse response = await _mediator.Send(removeProductImageCommandRequest);
        return Ok();
    }

    // FIXME IDK WHY THE FOLLOWING ENPOINTS DOES NOT WORK WHEN MEDIATR IMPLEMENTED.

    [HttpPost("[action]")]
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

    [HttpGet("[action]/{Id}")]
    public async Task<IActionResult> GetImages([FromRoute] GetProductImagesQueryRequest getProductImagesQueryRequest)
    {
        Product? product = await _productReadRepository.Table.Include(p => p.ProductImageFiles)
            .FirstOrDefaultAsync(p => p.Id == Guid.Parse(getProductImagesQueryRequest.Id));

        return Ok(product?.ProductImageFiles.Select(p => new
        {
            p.Id,
            p.Path,
            p.FileName
        }));
    }
}