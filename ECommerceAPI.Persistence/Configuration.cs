using Microsoft.Extensions.Configuration;

namespace ECommerceAPI.Persistence;

static class Configuration
{
    public static string ConnectionString
    {
        get
        {
            ConfigurationManager configurationManager = new();
            configurationManager.SetBasePath(Path.Combine(Directory.GetCurrentDirectory()));
            configurationManager.AddJsonFile("persistencesettings.json");
            
            return configurationManager.GetConnectionString("PostgresConnection")!;
        }
    }
}