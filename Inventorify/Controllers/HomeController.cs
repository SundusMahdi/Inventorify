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
        private readonly ApplicationIdentityDbContext _db;

        [BindProperty]
        public InventoryItem InventoryItem { get; set; }

        public HomeController(ApplicationIdentityDbContext db)
        {
            _db = db;
        }

        // Reseed if necessary then render index page
        public IActionResult Index()
        {
            // Reseed the database
            ReSeedDb rDb = new ReSeedDb(_db);
            rDb.SeedDb();

            return View();
        }

        // render create/edit page
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

        #region API Calls
        // Post edited or new InventoryItem
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert()
        {
            if (ModelState.IsValid)
            {
                if (InventoryItem.Id == 0)
                {
                    // Create
                    InventoryItem.TotalPrice = Math.Round(InventoryItem.UnitPrice * InventoryItem.Count, 2);
                    _db.InventoryItems.Add(InventoryItem);
                }
                else
                {
                    // Edit
                    InventoryItem.TotalPrice = Math.Round(InventoryItem.UnitPrice * InventoryItem.Count, 2);
                    _db.InventoryItems.Update(InventoryItem);
                }
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(InventoryItem);
        }

        // Get all items in database and return json
        [HttpGet]
        public async Task<IActionResult> GetAll() 
        {
            return Json(new { data = await _db.InventoryItems.ToListAsync() });
        }

        // delete data by id
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
