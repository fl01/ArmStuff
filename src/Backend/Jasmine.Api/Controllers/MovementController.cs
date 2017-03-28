﻿using System;
using System.Threading.Tasks;
using Jasmine.Api.Models;
using Jasmine.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Jasmine.Api.Controllers
{
    public class MovementController : Controller
    {
        private readonly IMovementService _movementsService;

        public MovementController(IMovementService movementsService)
        {
            _movementsService = movementsService;
        }

        [HttpGet("api/movement/{deviceId:guid}/{page:int}/{limit:int}")]
        public async Task<IActionResult> GetHistory([FromRoute]Guid deviceId, [FromRoute]int page, [FromRoute]int limit)
        {
            if (Guid.Empty.Equals(deviceId))
            {
                return BadRequest();
            }

            MovementHistory history = await _movementsService.GetHistoryForDeviceAsync(deviceId, page, limit);
            return Ok(history);
        }

        [HttpPost("api/movement")]
        public async Task<IActionResult> AddMovementRecord([FromBody] MovementChangedData movementData)
        {
            if (movementData == null || !ModelState.IsValid)
            {
                return new BadRequestObjectResult(ModelState);
            }

            Console.WriteLine($"{DateTime.UtcNow} Received movement change from {movementData.DeviceId}");

            await _movementsService.AddMovementAsync(movementData);
            return Accepted();
        }
    }
}
