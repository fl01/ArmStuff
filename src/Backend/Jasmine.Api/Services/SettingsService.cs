using Microsoft.Extensions.Configuration;

namespace Jasmine.Api.Services
{
    public class SettingsService : ISettingsService
    {
        private readonly IConfigurationRoot _configuration;

        public SettingsService(IConfigurationRoot configuration)
        {
            _configuration = configuration;
        }

        public string GetConnectionString()
        {
            return _configuration.GetConnectionString("MongoDb");
        }

        public string GetMovementsDbName()
        {
            return _configuration.GetConnectionString("MongoDbName");
        }
    }
}
