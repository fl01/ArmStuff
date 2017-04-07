using System;

namespace TalkingHead.Api.Services
{
    public interface ISettingsService
    {
        Guid AuthCode { get; }

        string GetVoiceRssApiKey();

        string GetVoiceRssApiUrl();
    }
}