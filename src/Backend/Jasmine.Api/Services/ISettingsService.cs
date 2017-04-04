using System;

namespace Jasmine.Api.Services
{
    public interface ISettingsService
    {
        Guid AuthCode { get; }

        TimeSpan GetSensorActivityExpiry();

        string GetConnectionString();

        string GetMovementsDbName();

        TimeSpan GetWatcherDelay();
    }
}