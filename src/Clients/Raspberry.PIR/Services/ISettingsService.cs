using System;

namespace Raspberry.PIR.Services
{
    public interface ISettingsService
    {
        Guid DeviceId { get; }

        int PirHeaderNum { get; }

        string MovementEndpointUrl { get; }

        string PingEndpointUrl { get; }
    }
}