using ConfectioneryContracts.BindingModels;
using ConfectioneryContracts.ViewModels;
using System.Collections.Generic;

namespace ConfectioneryContracts.BusinessLogicsContracts
{
    public interface IImplementerLogic
    {
        List<ImplementerViewModel> Read(ImplementerBindingModel model);
        void CreateOrUpdate(ImplementerBindingModel model);
        void Delete(ImplementerBindingModel model);
    }
}
