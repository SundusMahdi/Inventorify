using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inventorify.Models
{
    public class ApplicationIdentityDbContext : Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityDbContext
    {
            public ApplicationIdentityDbContext(DbContextOptions<ApplicationIdentityDbContext> options)
                : base(options)
            {
            }

        public DbSet<InventoryItem> InventoryItems { get; set; }
    }
}
