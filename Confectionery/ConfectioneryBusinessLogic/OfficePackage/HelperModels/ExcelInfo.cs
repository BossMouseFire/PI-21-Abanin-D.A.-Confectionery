using ConfectioneryContracts.ViewModels;
using System.Collections.Generic;

namespace ConfectioneryBusinessLogic.OfficePackage.HelperModels
{
    public class ExcelInfo
    {
        public string FileName { get; set; }
        public string Title { get; set; }
        public List<ReportPastryComponentViewModel> ProductComponents { get; set; }
    }
}
