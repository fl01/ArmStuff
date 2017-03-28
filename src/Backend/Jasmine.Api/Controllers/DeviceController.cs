using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Jasmine.Api.Models;
using Jasmine.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Jasmine.Api.Controllers
{
    public class DeviceController : Controller
    {
        private readonly IDeviceService _deviceService;

        public DeviceController(IDeviceService deviceService)
        {
            _deviceService = deviceService;
        }

        [HttpGet("api/device/{deviceId:guid?}")]
        public async Task<IActionResult> GetStatus([FromRoute]Guid? deviceId)
        {
            IEnumerable<DeviceStatus> statuses = await _deviceService.GetStatusAsync(deviceId.GetValueOrDefault());
            return Ok(statuses);
        }
    }
}
