using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Inventorify.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Eventing.Reader;

namespace Inventorify.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationIdentityDbContext _db;

        [BindProperty]
        public InventoryItem InventoryItem { get; set; }

        public HomeController(ILogger<HomeController> logger, ApplicationIdentityDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Upsert(int? id)
        {
            InventoryItem = new InventoryItem();
            if (id == null)
            {
                //create
                return View(InventoryItem);
            }
            //update
            InventoryItem = _db.InventoryItems.FirstOrDefault(u => u.Id == id);
            if (InventoryItem == null)
            {
                return NotFound();
            }
            return View(InventoryItem);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert()
        {
            if (ModelState.IsValid)
            {
                if (InventoryItem.Id == 0)
                {
                    //create
                    _db.InventoryItems.Add(InventoryItem);
                }
                else
                {
                    _db.InventoryItems.Update(InventoryItem);
                }
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(InventoryItem);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        #region API Calls
        [HttpGet]
        public async Task<IActionResult> GetAll() 
        {
            return Json(new { data = await _db.InventoryItems.ToListAsync() });
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var itemFromDb = await _db.InventoryItems.FirstOrDefaultAsync(u => u.Id == id);
            if (itemFromDb == null) 
            {
                return Json(new { success = false, message = "Error while Deleting" });
            }
            _db.InventoryItems.Remove(itemFromDb);
            await _db.SaveChangesAsync();
            return Json(new { success = true, message = "Delete Successful" });
        }
        #endregion
    }
}
