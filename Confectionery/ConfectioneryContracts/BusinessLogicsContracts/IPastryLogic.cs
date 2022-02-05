using ConfectioneryContracts.BindingModels;
using ConfectioneryContracts.ViewModels;
using System.Collections.Generic;


namespace ConfectioneryContracts.BusinessLogicsContracts
{
    public interface IPastryLogic
    {
        List<PastryViewModel> Read(PastryBindingModel model);
        void CreateOrUpdate(PastryBindingModel model);
        void Delete(PastryBindingModel model);
    }

}
