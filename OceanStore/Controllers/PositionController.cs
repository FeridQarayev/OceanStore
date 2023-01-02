using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OceanStore.BusinessLayer.Managers;
using OceanStore.DataAccesLayer.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OceanStore.Controllers
{
    [Authorize(Roles = "Admin,SuperAdmin")]
    public class PositionController : Controller
    {
        #region ctor
        private readonly PositionManager _positionManager;
        public PositionController(PositionManager positionManager)
        {
            _positionManager = positionManager;
        }
        #endregion
        public async Task<IActionResult> Index()
        {
            List<Position> positions = await _positionManager.GetAllPositions();
            return View(positions);
        }
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Position position)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            await _positionManager.CreatePosition(position);
            return RedirectToAction("Index");
        }
    }
}
