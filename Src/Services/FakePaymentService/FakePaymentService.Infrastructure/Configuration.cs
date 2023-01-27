

using Microsoft.Extensions.Configuration;

namespace HotelReservationService.Infrastracture;

public static class Configuration
{
    public static string ConnectionString
        {
            get
            {
                ConfigurationManager configurationManager = new();

                configurationManager.SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../FakePaymentService.Api"));
                configurationManager.AddJsonFile("appsettings.json");

                return configurationManager.GetConnectionString("PostgreSql");
            }
        }
}