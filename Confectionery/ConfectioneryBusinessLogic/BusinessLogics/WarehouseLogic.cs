using System;
using System.Collections.Generic;
using ConfectioneryContracts.BindingModels;
using ConfectioneryContracts.BusinessLogicsContracts;
using ConfectioneryContracts.StoragesContracts;
using ConfectioneryContracts.ViewModels;

namespace ConfectioneryBusinessLogic.BusinessLogics
{
    public class WarehouseLogic : IWarehouseLogic
    {
        private readonly IWarehouseStorage warehouseStorage;
        private readonly IComponentStorage componentStorage;
        public WarehouseLogic(IWarehouseStorage warehouseStorage, IComponentStorage componentStorage)
        {
            this.warehouseStorage = warehouseStorage;
            this.componentStorage = componentStorage;
        }
        public List<WarehouseViewModel> Read(WarehouseBindingModel model)
        {
            if (model == null)
            {
                return warehouseStorage.GetFullList();
            }
            if (model.Id.HasValue)
            {
                return new List<WarehouseViewModel> { warehouseStorage.GetElement(model) };
            }
            return warehouseStorage.GetFilteredList(model);
        }
        public void CreateOrUpdate(WarehouseBindingModel model)
        {
            var element = warehouseStorage.GetElement(new WarehouseBindingModel { WarehouseName = model.WarehouseName });
            if (element != null && element.Id != model.Id)
            {
                throw new Exception("Имеется склад с данным названием");
            }
            if (model.Id.HasValue)
            {
                warehouseStorage.Update(model);
            }
            else
            {
                warehouseStorage.Insert(model);
            }
        }
        public void Delete(WarehouseBindingModel model)
        {
            var element = warehouseStorage.GetElement(new WarehouseBindingModel { Id = model.Id });
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            warehouseStorage.Delete(model);
        }
        public void AddComponent(WarehouseBindingModel model, int ComponentId, int Count)
        {
            var warehouse = warehouseStorage.GetElement(new WarehouseBindingModel { Id = model.Id });
            if (warehouse == null)
            {
                throw new Exception("Элемент не найден");
            }
            var component = componentStorage.GetElement(new ComponentBindingModel { Id = ComponentId });
            if (component == null)
            {
                throw new Exception("Элемент не найден");
            }
            if (warehouse.WarehouseComponents.ContainsKey(ComponentId))
            {
                int oldCount = warehouse.WarehouseComponents[ComponentId].Item2;
                warehouse.WarehouseComponents[ComponentId] = (component.ComponentName, oldCount + Count);
            }
            else
            {
                warehouse.WarehouseComponents.Add(ComponentId, (component.ComponentName, Count));
            }
            warehouseStorage.Update(new WarehouseBindingModel
            {
                Id = warehouse.Id,
                WarehouseName = warehouse.WarehouseName,
                Responsible = warehouse.Responsible,
                DateCreate = warehouse.DateCreate,
                WarehouseComponents = warehouse.WarehouseComponents
            });
        }
    }
}