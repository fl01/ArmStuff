using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Jasmine.Api.Models;

namespace Jasmine.Api.Services
{
    public class StatusWatcherService : IStatusWatcherService
    {
        private readonly INotificationsService _notificationService;
        private readonly IDeviceService _deviceService;
        private readonly ISettingsService _settingsService;

        public StatusWatcherService
            (IDeviceService deviceService,
            INotificationsService notificationService,
            ISettingsService settingsService)
        {
            _notificationService = notificationService;
            _deviceService = deviceService;
            _settingsService = settingsService;
        }

        public async void Watch(Guid deviceId)
        {
            DeviceStatus lastDeviceStatus = null;
            while (true)
            {
                IEnumerable<DeviceStatus> statuses = await _deviceService.GetStatusAsync(deviceId);
                DeviceStatus deviceStatus = statuses.FirstOrDefault();
                if (!Equals(lastDeviceStatus, deviceStatus))
                {
                    _notificationService.OnDeviceStatusChanged(deviceStatus);
                    lastDeviceStatus = deviceStatus;
                }

                Thread.Sleep(_settingsService.GetWatcherDelay());
            }
        }
    }
}
