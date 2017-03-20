using System;
using System.Threading.Tasks;
using Jasmine.Api.Models;
using Jasmine.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Jasmine.Api.Controllers
{
    [Route("api/[controller]")]
    public class MovementController : Controller
    {
        private readonly IMovementService _movementsService;

        public MovementController(IMovementService movementsService)
        {
            _movementsService = movementsService;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] MovementChangedData movementData)
        {
            if (movementData == null || !ModelState.IsValid)
            {
                return new BadRequestObjectResult(ModelState);
            }

            Console.WriteLine($"{DateTime.UtcNow} Recevied movement change from {movementData.DeviceId}");

            await _movementsService.AddAsync(movementData);
            return Accepted();
        }
    }
}
