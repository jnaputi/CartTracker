using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CartTracker.Models
{
    [Table("CartItems")]
    public partial class CartItem
    {
        public int CartItemId { get; set; }
        public int CartId { get; set; }
        public int? ItemId { get; set; }
        public DateTime LastUpdated { get; set; }

        public Cart Cart { get; set; }
        public Items Item { get; set; }
    }
}
