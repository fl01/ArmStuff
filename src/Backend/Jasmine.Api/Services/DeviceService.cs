using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Jasmine.Api.Definitions;
using Jasmine.Api.Extensions;
using Jasmine.Api.Models;

namespace Jasmine.Api.Services
{
    public class DeviceService : IDeviceService
    {
        private readonly IMovementService _movementService;
        private readonly ISettingsService _settingsService;

        public DeviceService(IMovementService movementService, ISettingsService settingsService)
        {
            _movementService = movementService;
            _settingsService = settingsService;
        }

        public async Task<IEnumerable<DeviceStatus>> GetStatusAsync(Guid deviceId)
        {
            if (!Guid.Empty.Equals(deviceId))
            {
                return new[] { await GetExactDeviceStatus(deviceId) };
            }

            return await GetAllDevicesStatus();
        }

        private async Task<DeviceStatus> GetExactDeviceStatus(Guid deviceId)
        {
            SensorActivity pirActivity = await _movementService.GetSensorStatusAsync(deviceId, SensorType.PIR);
            SensorActivity rangeActivity = await _movementService.GetSensorStatusAsync(deviceId, SensorType.Range);

            TimeSpan expiry = _settingsService.GetSensorActivityExpiry();

            OccupationStatus status = pirActivity.IsActive(expiry) || rangeActivity.IsActive(expiry)
                                      ? OccupationStatus.Occupied
                                      : OccupationStatus.Empty;

            return new DeviceStatus()
            {
                DeviceInfo = Devices.All.FirstOrDefault(f => f.ID.Equals(deviceId)),
                Status = status,
                LastActivity = GetMinDate(pirActivity?.EntryDate, rangeActivity?.EntryDate)
            };
        }

        private async Task<IEnumerable<DeviceStatus>> GetAllDevicesStatus()
        {
            var devices = new List<DeviceStatus>();

            foreach (Device device in Devices.All)
            {
                DeviceStatus status = await GetExactDeviceStatus(device.ID);
                devices.Add(status);
            }

            return devices;
        }

        private DateTime GetMinDate(DateTime? first, DateTime? second)
        {
            var firstValue = first.GetValueOrDefault();
            var secondValue = second.GetValueOrDefault();
            return firstValue > secondValue ? firstValue : secondValue;
        }
    }
}
