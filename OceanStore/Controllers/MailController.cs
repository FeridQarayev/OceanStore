using Microsoft.AspNetCore.Mvc;

namespace OceanStore.Controllers
{
    public class MailController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
