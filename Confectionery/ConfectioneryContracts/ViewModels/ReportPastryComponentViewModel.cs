using System;
using System.Collections.Generic;

namespace ConfectioneryContracts.ViewModels
{
    public class ReportPastryComponentViewModel
    {
        public string ComponentName { get; set; }
        public int TotalCount { get; set; }
        public List<Tuple<string, int>> Pastries { get; set; }
    }
}
