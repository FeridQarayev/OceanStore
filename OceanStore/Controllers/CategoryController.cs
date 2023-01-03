using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace OceanStore.Controllers
{
    [Authorize(Roles = "Admin,SuperAdmin")]
    public class CategoryController : Controller
    {
        #region ctor
        public CategoryController()
        {

        }
        #endregion

        #region Index
        public async Task<IActionResult> Index()
        {
            return View();
        }
        #endregion
    }
}
