using System.Collections.Generic;

namespace ConfectioneryListImplement.Models
{
    public class Pastry
    {
        public int Id { get; set; }
        public string PastryName { get; set; }
        public decimal Price { get; set; }
        public Dictionary<int, int> PastryComponents { get; set; }
    }

}
