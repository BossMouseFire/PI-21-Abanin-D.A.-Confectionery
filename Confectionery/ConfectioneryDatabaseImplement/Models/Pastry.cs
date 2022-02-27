using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConfectioneryDatabaseImplement.Models
{
    public class Pastry
    {
        public int Id { get; set; }
        [Required]
        public string PastryName { get; set; }
        [Required]
        public decimal Price { get; set; }
        [ForeignKey("PastryId")]
        public virtual List<PastryComponent> PastryComponents { get; set; }
        public virtual List<Order> Orders { get; set; }

    }
}
