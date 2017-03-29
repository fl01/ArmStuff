using System;

namespace Raspberry.PIR.Services
{
    public interface ISettingsService
    {
        Guid DeviceId { get; }

        int PirHeaderNum { get; }

        int RangeEchoHeaderNum { get; }

        int RangeTriggerHeaderNum { get; }

        string MovementEndpointUrl { get; }

        string PingEndpointUrl { get; }

        bool PushFalseData { get; }
    }
}