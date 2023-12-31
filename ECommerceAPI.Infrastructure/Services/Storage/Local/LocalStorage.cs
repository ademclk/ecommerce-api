﻿using System.IO;
using ECommerceAPI.Application.Abstractions.Storage.Local;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace ECommerceAPI.Infrastructure.Services.Storage.Local;

public class LocalStorage : Storage, ILocalStorage
{
    private readonly IWebHostEnvironment _webHostEnvironment;

    public LocalStorage(IWebHostEnvironment webHostEnvironment)
    {
        _webHostEnvironment = webHostEnvironment;
    }

    public bool ContainsFile(string path, string fileName)
        => File.Exists(Path.Combine(path, fileName));

    public async Task DeleteAsync(string path, string fileName)
       => File.Delete(Path.Combine(path, fileName));


    public List<string> GetFiles(string path)
    {
        DirectoryInfo directoryInfo = new(path);

        return directoryInfo.GetFiles().Select(file => file.Name).ToList();
    }

    public async Task<List<(string fileName, string pathOrContainerName)>> UploadAsync(string path, IFormFileCollection files)
    {
        var uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, path);

        if (!Directory.Exists(uploadPath))
            Directory.CreateDirectory(uploadPath);

        List<(string fileName, string path)> fileInfoList = new();

        foreach (IFormFile file in files)
        {
            var changedFileName = await RenameFileAsync(uploadPath, file.Name, ContainsFile);

            string filePath = Path.Combine(uploadPath, changedFileName);
            await CopyFileAsync(filePath, file);
            fileInfoList.Add((changedFileName, path));
        }

        return fileInfoList;
    }

    private static async Task<bool> CopyFileAsync(string path, IFormFile file)
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
}
