using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Inventorify.Models;
using Microsoft.EntityFrameworkCore.Internal;

namespace Inventorify.Controllers
{
    public class ReSeedDb
    {
        const bool ReSeed = false;

        private readonly ApplicationIdentityDbContext _db;

        public ReSeedDb(ApplicationIdentityDbContext db)
        {
            _db = db;
        }

        public void SeedDb()
        {
            if (ReSeed || !_db.InventoryItems.ToList().Any())
            {
                _db.Database.EnsureDeleted();
                _db.Database.EnsureCreated();
                _db.InventoryItems.Add(new InventoryItem("Bottled water",
                                                        "Food & Beverages",
                                                        745,
                                                        (float)1.99));
                _db.InventoryItems.Add(new InventoryItem("Socks",
                                                        "Apparel",
                                                        413,
                                                        (float)4.5));
                _db.InventoryItems.Add(new InventoryItem("Coffee machine",
                                                        "Appliances",
                                                        18,
                                                        (float)28.49));
                _db.InventoryItems.Add(new InventoryItem("Hand sanitizer",
                                                        "Health and Beauty",
                                                        100,
                                                        (float)2.45));
                _db.InventoryItems.Add(new InventoryItem("A4 paper",
                                                        "Office Supplies",
                                                        320,
                                                        (float)4.98));
                _db.InventoryItems.Add(new InventoryItem("Table lamp",
                                                        "Home",
                                                        245,
                                                        (float)23.95));
                _db.InventoryItems.Add(new InventoryItem("Tape",
                                                        "Office Supplies",
                                                        99,
                                                        (float)4.24));
                _db.InventoryItems.Add(new InventoryItem("Scarf",
                                                        "Apparel",
                                                        240,
                                                        (float)8.95));
                _db.InventoryItems.Add(new InventoryItem("Shirt",
                                                        "Apparel",
                                                        125,
                                                        (float)9.95));

                _db.SaveChanges();
            }
        }
    }
}
