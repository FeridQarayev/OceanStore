using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace OceanStore.Controllers
{
    public class IncomesController : Controller
    {
        public async Task<IActionResult> Index()
        {
            return View();
        }
    }
}
