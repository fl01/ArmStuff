using System;
using Jasmine.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Jasmine.Api.Controllers
{
    [Route("api/[controller]")]
    public class PingController : Controller
    {
        [HttpPost]
        public void Post([FromBody]Ping ping)
        {
            Console.WriteLine($"{DateTime.Now} Recevied ping from {ping?.DeviceId}");
        }
    }
}
