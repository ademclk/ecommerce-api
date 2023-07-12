using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using ECommerceAPI.Application.Abstractions.Storage.Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace ECommerceAPI.Infrastructure.Services.Storage.Azure;

public class AzureStorage : Storage, IAzureStorage
{
    private readonly BlobServiceClient _blobServiceClient;
    private BlobContainerClient _blobContainerClient;

    public AzureStorage(IConfiguration configuration)
    {
        _blobServiceClient = new(configuration["Storage:Azure"]);
    }

    public bool ContainsFile(string containerName, string fileName)
    {
        _blobContainerClient = _blobServiceClient.GetBlobContainerClient(containerName);

        return _blobContainerClient.GetBlobs().Any(b => b.Name != fileName);
    }

    public async Task DeleteAsync(string containerName, string fileName)
    {
        _blobContainerClient = _blobServiceClient.GetBlobContainerClient(containerName);
        BlobClient blobClient = _blobContainerClient.GetBlobClient(fileName);

        await blobClient.DeleteAsync();
    }

    public List<string> GetFiles(string containerName)
    {
        _blobContainerClient = _blobServiceClient.GetBlobContainerClient(containerName);

        return _blobContainerClient.GetBlobs().Select(b => b.Name).ToList();
    }

    public async Task<List<(string fileName, string pathOrContainerName)>> UploadAsync(string containerName, IFormFileCollection files)
    {
        _blobContainerClient = _blobServiceClient.GetBlobContainerClient(containerName);
        await _blobContainerClient.CreateIfNotExistsAsync();
        await _blobContainerClient.SetAccessPolicyAsync(PublicAccessType.BlobContainer);

        List<(string fileName, string pathOrContainerName)> fileInfoList = new();
        foreach (IFormFile file in files)
        {
            var changedFileName = await RenameFileAsync(containerName, file.Name, ContainsFile);

            BlobClient blobClient = _blobContainerClient.GetBlobClient(changedFileName);
            await blobClient.UploadAsync(file.OpenReadStream());
            fileInfoList.Add((file.Name, containerName));
        }
        return fileInfoList;
    }
}


