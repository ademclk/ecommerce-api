using System;
using ECommerceAPI.Application.Services;
using ECommerceAPI.Infrastructure.Utilities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace ECommerceAPI.Infrastructure.Services;

public class FileService : IFileService
{
    private readonly IWebHostEnvironment _webHostEnvironment;

    public FileService(IWebHostEnvironment webHostEnvironment)
    {
        _webHostEnvironment = webHostEnvironment;
    }

    public async Task<bool> CopyFileAsync(string path, IFormFile file)
    {
        try
        {
            using FileStream fileStream = new(path, FileMode.Create);
            await file.CopyToAsync(fileStream);
            return true;
        }
        catch (Exception)
        {
            //TODO Log system
            throw;
        }
    }

    private async Task<string> RenameFileAsync(string path, string fileName)
    {

        var renamedFileName = await Task.Run(() =>
        {
            var fileExtension = Path.GetExtension(fileName);
            var name = Path.GetFileNameWithoutExtension(fileName);
            var alphaOnlyName = NameUtility.RemoveNonAlphabeticCharacters(name);
            var dateTime = DateUtility.GetCurrentDateTime();

            var updatedFileName = alphaOnlyName + dateTime;

            int count = 1;

            while (File.Exists($"{path}{Path.DirectorySeparatorChar}{updatedFileName}{fileExtension}"))
            {
                updatedFileName = $"{updatedFileName}-{count}";
                count++;
            }

            updatedFileName += fileExtension;

            return updatedFileName;
        });

        return renamedFileName;

    }

    public async Task<List<(string fileName, string path)>> UploadAsync(string path, IFormFileCollection files)
    {
        var uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, path);

        if (!Directory.Exists(uploadPath))
            Directory.CreateDirectory(uploadPath);

        List<(string fileName, string path)> values = new();
        List<bool> results = new();

        foreach (IFormFile file in files)
        {
            var changedFileName = await RenameFileAsync(uploadPath, file.FileName);

            string filePath = Path.Combine(uploadPath, changedFileName);
            var result = await CopyFileAsync(filePath, file);
            values.Add((changedFileName, path));
            results.Add(result);
        }

        if (results.TrueForAll(r => r.Equals(true)))
            return values;

        return null;
    }
}


