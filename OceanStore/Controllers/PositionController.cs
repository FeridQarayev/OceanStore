using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace OceanStore.Controllers
{
    public class PositionController : Controller
    {
        [Authorize(Roles = "Admin,SuperAdmin")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
