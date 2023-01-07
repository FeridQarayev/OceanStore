﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OceanStore.BusinessLayer.Managers;
using System.Linq;
using System.Threading.Tasks;

namespace OceanStore.Controllers
{
    [Authorize(Roles = "Admin,SuperAdmin")]
    public class HomeController : Controller
    {
        private readonly AmmountManager _ammountManager;
        public HomeController(AmmountManager ammountManager)
        {
            _ammountManager = ammountManager;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.Ammount = await _ammountManager.GetTotalAmmount();
            ViewBag.ExpensesAmmount = await _ammountManager.GetTotalExpensesAmmount();
            ViewBag.IncomesAmmount = await _ammountManager.GetTotalIncomesAmmount();
            ViewBag.LatestIncomes = (await _ammountManager.GetIncomes()).FirstOrDefault();
            ViewBag.LatestExpenses = (await _ammountManager.GetExpenses()).FirstOrDefault();
            return View();
        }
    }
}
