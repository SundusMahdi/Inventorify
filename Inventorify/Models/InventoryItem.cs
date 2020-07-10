using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Inventorify.Models
{
    public class InventoryItem
    {
        public InventoryItem(){}
        public InventoryItem(string name, string group, int count, float unitPrice)
        {
            this.Name = name;
            this.Group = group;
            this.Count = count;
            this.UnitPrice = unitPrice;
            this.TotalPrice = Math.Round(this.Count * this.UnitPrice, 2);
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Group { get; set; }

        [Required]
        public int Count { get; set; }

        [Required]
        public float UnitPrice { get; set; }

        public double TotalPrice { get; set; }
    }
}
