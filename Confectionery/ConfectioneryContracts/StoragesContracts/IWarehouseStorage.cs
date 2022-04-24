using System.Collections.Generic;
using ConfectioneryContracts.BindingModels;
using ConfectioneryContracts.ViewModels;

namespace ConfectioneryContracts.StoragesContracts
{
    public interface IWarehouseStorage
    {
        List<WarehouseViewModel> GetFullList();
        List<WarehouseViewModel> GetFilteredList(WarehouseBindingModel model);
        WarehouseViewModel GetElement(WarehouseBindingModel model);
        void Insert(WarehouseBindingModel model);
        void Update(WarehouseBindingModel model);
        void Delete(WarehouseBindingModel model);

        void changeBalance(Dictionary<int, int> components);
    }
}