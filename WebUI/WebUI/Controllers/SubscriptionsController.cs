using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
    [Route("[controller]")]
    public class SubscriptionsController : Controller
    {
        [HttpGet("[action]/{id:guid}")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost("[action]/{id:guid}")]
        public IActionResult Create()
        {
            return View();
        }

    }
}
