using ECommerceAPI.Infrastructure.Utilities;

namespace ECommerceAPI.Infrastructure.Services;

public class FileService
{

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
}


