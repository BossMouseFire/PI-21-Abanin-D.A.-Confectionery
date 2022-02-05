using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfectioneryContracts.BindingModels
{
    public class PastryBindingModel
    {
        public int? Id { get; set; }
        public string PastryName { get; set; }
        public decimal Price { get; set; }
        public Dictionary<int, (string, int)> PastryComponents { get; set; }

    }
}
