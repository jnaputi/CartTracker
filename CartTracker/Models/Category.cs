using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CartTracker.Models
{
    [Table("Categories")]
    public class Category
    {
        public int CategoryId { get; set; }
        public int? ItemId { get; set; }
        public string Name { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime LastUpdated { get; set; }

        public Items Item { get; set; }
    }
}
