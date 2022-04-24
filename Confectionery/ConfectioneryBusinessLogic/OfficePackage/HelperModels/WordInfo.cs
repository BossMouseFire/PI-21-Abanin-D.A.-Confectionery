using ConfectioneryContracts.ViewModels;
using System.Collections.Generic;

namespace ConfectioneryBusinessLogic.OfficePackage.HelperModels
{
    public class WordInfo
    {
        public string FileName { get; set; }
        public string Title { get; set; }
        public List<PastryViewModel> Pastries { get; set; }
        public List<WarehouseViewModel> Warehouses { get; set; }
    }
}