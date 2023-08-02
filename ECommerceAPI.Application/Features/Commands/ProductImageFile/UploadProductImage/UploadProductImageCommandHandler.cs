using System;
using ECommerceAPI.Application.Abstractions.Storage;
using ECommerceAPI.Application.Repositories.Product;
using ECommerceAPI.Application.Repositories.ProductImageFile;
using E = ECommerceAPI.Domain.Entities;
using MediatR;

namespace ECommerceAPI.Application.Features.Commands.ProductImageFile.UploadProductImage;

public class UploadProductImageCommandHandler : IRequestHandler<UploadProductImageCommandRequest, UploadProductImageCommandResponse>
{
    private readonly IStorageService _storageService;
    private readonly IProductReadRepository _productReadRepository;
    private readonly IProductImageFileWriteRepository _productImageFileWriteRepository;

    public async Task<UploadProductImageCommandResponse> Handle(UploadProductImageCommandRequest request, CancellationToken cancellationToken)
    {
        //var fileInfoList = await _storageService.UploadAsync("images", request.Files);
        //var product = await _productReadRepository.GetByIdAsync(request.Id);

        //await _productImageFileWriteRepository.AddRangeAsync(fileInfoList.Select(f => new E.ProductImageFile()
        //{
        //    FileName = f.fileName,
        //    Path = f.pathOrContainerName,
        //    Storage = _storageService.Storage,
        //    Products = new List<E.Product> { product }
        //}).ToList());

        //await _productImageFileWriteRepository.SaveChangesAsync();

        return new();
    }
}
