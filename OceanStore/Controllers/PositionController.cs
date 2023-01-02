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

        #region Index
        public async Task<IActionResult> Index()
        {
            List<Position> positions = await _positionManager.GetAllPositions();
            return View(positions);
        }
        #endregion
        #region Create
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
            bool IsExist = await _positionManager.IsExistPositionName(position);
            if (IsExist)
            {
                ModelState.AddModelError("Name", "This Position is already exist");
                return View();
            }
            await _positionManager.CreatePosition(position);
            return RedirectToAction("Index");
        }
        #endregion

        #region Update
        public async Task<IActionResult> Update(int id)
        {
            if (id == null)
                return NotFound();
            Position position = await _positionManager.GetPositionById(id);
            if (position == null)
                return BadRequest();
            return View(position);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(Position position,int id)
        {
            #region FromGet
            if (id == null)
                return NotFound();
            Position dbPosition = await _positionManager.GetPositionById(id);
            if (dbPosition == null)
                return BadRequest();
            #endregion
            if (!ModelState.IsValid)
                return View(position);
            bool isExist = await _positionManager.IsExistPositionNameVariousId(position);
            if (isExist)
            {
                ModelState.AddModelError("Name", "This Position is already exist");
                return View(position);
            }
            await _positionManager.UpdatePosition(dbPosition,position);
            return RedirectToAction("Index");
        }
        #endregion

        #region Activity
        public async Task<IActionResult> Activity(int? id)
        {
            if (id == null)
                return NotFound();
            Position position = await _positionManager.GetPositionById((int)id);
            if (position == null)
                return BadRequest();
            await _positionManager.ActivityPosition(position);
            return RedirectToAction("Index");
        }
        #endregion
    }
}
