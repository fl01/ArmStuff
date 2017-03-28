using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Jasmine.Api.Models;

namespace Jasmine.Api.Services
{
    public interface IDeviceService
    {
        Task<IEnumerable<DeviceStatus>> GetStatusAsync(Guid deviceId);
    }
}
