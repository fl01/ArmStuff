using System;
using Jasmine.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Jasmine.Api.Controllers
{
    [Route("api/[controller]")]
    public class MovementController : Controller
    {
        [HttpPost]
        public void Post([FromBody]MovementChangedData movementData)
        {
            if (movementData == null)
            {
                Console.WriteLine("Invalid post data");
                return;
            }

            Console.WriteLine($"{DateTime.UtcNow} Recevied movement change from {movementData.DeviceId}");
        }
    }
}
