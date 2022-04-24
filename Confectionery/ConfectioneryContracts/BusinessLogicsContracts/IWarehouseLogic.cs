using System.Collections.Generic;
using ConfectioneryContracts.ViewModels;
using ConfectioneryContracts.BindingModels;

namespace ConfectioneryContracts.BusinessLogicsContracts
{
    public interface IWarehouseLogic
    {
        List<WarehouseViewModel> Read(WarehouseBindingModel model);
        void CreateOrUpdate(WarehouseBindingModel model);
        void Delete(WarehouseBindingModel model);
        void AddComponent(WarehouseBindingModel model, int componentId, int Count);
    }
}