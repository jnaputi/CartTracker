using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CartTracker.Models
{
    [Table("Carts")]
    public class Cart
    {
        public Cart()
        {
            CartItems = new HashSet<CartItem>();
        }

        public int CartId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime LastUpdated { get; set; }

        public ICollection<CartItem> CartItems { get; set; }
    }
}
