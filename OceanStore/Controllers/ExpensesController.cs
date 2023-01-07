using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OceanStore.BusinessLayer.Managers;
using OceanStore.DataAccesLayer.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OceanStore.Controllers
{
    [Authorize(Roles = "Admin,SuperAdmin")]
    public class ExpensesController : Controller
    {
        #region ctor
        private readonly AmmountManager _ammountManager;
        public ExpensesController(AmmountManager ammountManager)
        {
            _ammountManager = ammountManager;
        }
        #endregion

        #region Index
        public async Task<IActionResult> Index()
        {
            List<Ammount> ammounts = await _ammountManager.ListAllExpenses();
            return View(ammounts);
        }
        #endregion
    }
}
