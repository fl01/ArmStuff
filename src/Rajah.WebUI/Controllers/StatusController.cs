using Microsoft.AspNetCore.Mvc;

namespace Rajah.WebUI.Controllers
{
    public class StatusController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
