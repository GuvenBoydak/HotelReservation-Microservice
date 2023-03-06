using Microsoft.Extensions.Configuration;

namespace IdentityService.Infrastructure;

public static class Configuration
{
    public static string ConnectionString
    {
        get
        {
            ConfigurationManager configurationManager = new();

            configurationManager.SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../IdentityService.Api"));
            configurationManager.AddJsonFile("appsettings.json");

            return configurationManager.GetConnectionString("PostgreSql");
        }
    }
}