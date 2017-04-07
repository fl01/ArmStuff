using Microsoft.AspNetCore.Mvc;
using TalkingHead.Api.Auth;

namespace TalkingHead.Api.Controllers
{
    [Route("api/[controller]")]
    public class SpeechController : Controller
    {
        public SpeechController()
        {
        }

        [HttpPost]
        [CodeRequirement]
        public void Post([FromBody]string value)
        {
        }
    }
}
