using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CartTracker.Models
{
    [Table("Items")]
    public class Items
    {
        public Items()
        {
            CartItems = new HashSet<CartItem>();
            Categories = new HashSet<Category>();
        }

        public int ItemId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsAcquired { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime LastUpdated { get; set; }

        public ICollection<CartItem> CartItems { get; set; }
        public ICollection<Category> Categories { get; set; }
    }
}
