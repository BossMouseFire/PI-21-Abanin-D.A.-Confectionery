using System.Collections.Generic;
using System.ComponentModel;

namespace ConfectioneryContracts.ViewModels
{
    public class PastryViewModel
    {
        public int Id { get; set; }
        [DisplayName("Название изделия")]
        public string PastryName { get; set; }
        [DisplayName("Цена")]
        public decimal Price { get; set; }
        public Dictionary<int, (string, int)> PastryComponents { get; set; }
    }
}
