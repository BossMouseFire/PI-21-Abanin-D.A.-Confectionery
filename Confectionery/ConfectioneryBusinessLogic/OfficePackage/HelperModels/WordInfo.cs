using ConfectioneryContracts.ViewModels;
using System.Collections.Generic;

namespace ConfectioneryBusinessLogic.OfficePackage.HelperModels
{
    public class WordInfo
    {
        public string FileName { get; set; }
        public string Title { get; set; }
        public List<ComponentViewModel> Components { get; set; }
    }
}