using System;
using ECommerceAPI.Infrastructure.Utilities;

namespace ECommerceAPI.Infrastructure.Services.Storage;

public class Storage
{
    protected delegate bool ContainsFile(string pathOrContainerName, string fileName);

    protected static async Task<string> RenameFileAsync(string pathOrContainerName, string fileName, ContainsFile containsFile)
    {

        var renamedFileName = await Task.Run(() =>
        {
            var fileExtension = Path.GetExtension(fileName);
            var name = Path.GetFileNameWithoutExtension(fileName);
            var alphaOnlyName = NameUtility.RemoveNonAlphabeticCharacters(name);
            var dateTime = DateUtility.GetCurrentDateTime();

            var updatedFileName = alphaOnlyName + dateTime;

            int count = 1;

            while (containsFile(pathOrContainerName, fileName))
            {   
                updatedFileName = $"{updatedFileName}-{count}";
                count++;
            }

            updatedFileName += fileExtension;

            return updatedFileName;
        });
        return renamedFileName;
    }
}


