using ConfectioneryContracts.BindingModels;
using ConfectioneryContracts.ViewModels;
using System.Collections.Generic;


namespace ConfectioneryContracts.BusinessLogicsContracts
{
    public interface IComponentLogic
    {
        List<ComponentViewModel> Read(ComponentBindingModel model);
        void CreateOrUpdate(ComponentBindingModel model);
        void Delete(ComponentBindingModel model);
    }

}
