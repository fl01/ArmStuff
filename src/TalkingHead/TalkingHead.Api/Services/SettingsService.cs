using System;
using Microsoft.Extensions.Configuration;

namespace TalkingHead.Api.Services
{
    public class SettingsService : ISettingsService
    {
        private readonly IConfigurationRoot _configuration;

        public SettingsService(IConfigurationRoot configuration)
        {
            _configuration = configuration;
        }

        public Guid AuthCode
        {
            get
            {
                return _configuration.GetValue<Guid>("Auth:AuthCode");
            }
        }

        public string GetVoiceRssApiKey()
        {
            return _configuration.GetValue<string>("TTSProviders:VoiceRss:ApiKey");
        }

        public string GetVoiceRssApiUrl()
        {
            return _configuration.GetValue<string>("TTSProviders:VoiceRss:ApiUrl");
        }
    }
}
