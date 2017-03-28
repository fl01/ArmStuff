using System;

namespace Jasmine.Api.Services
{
    public interface ISettingsService
    {
        TimeSpan GetSensorActivityExpiry();

        string GetConnectionString();

        string GetMovementsDbName();
    }
}