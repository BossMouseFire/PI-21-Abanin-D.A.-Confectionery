using System;
using System.Collections.Generic;
using ConfectioneryContracts.BusinessLogicsContracts;
using ConfectioneryContracts.StoragesContracts;
using ConfectioneryContracts.BindingModels;
using ConfectioneryContracts.ViewModels;
using ConfectioneryContracts.Enums;
using System.Linq;

namespace ConfectioneryBusinessLogic.BusinessLogics
{
    public class OrderLogic : IOrderLogic
    {
        private readonly IOrderStorage _orderStorage;
        private readonly IWarehouseStorage _warehouseStorage;
        private readonly IPastryStorage _pastryStorage;
        public OrderLogic(IOrderStorage orderStorage, IWarehouseStorage warehouseStorage, IPastryStorage pastryStorage)
        {
            _orderStorage = orderStorage;
            _warehouseStorage = warehouseStorage;
            _pastryStorage = pastryStorage;
        }

        public List<OrderViewModel> Read(OrderBindingModel model)
        {
            if (model == null)
            {
                return _orderStorage.GetFullList();
            }
            if (model.Id.HasValue)
            {
                return new List<OrderViewModel> { _orderStorage.GetElement(model) };
            }
            return _orderStorage.GetFilteredList(model);
        }

        public void CreateOrder(CreateOrderBindingModel model)
        {
            var elements = _orderStorage.GetFullList();

            _orderStorage.Insert(new OrderBindingModel { 
                Id = elements.Count,
                PastryId = model.PastryId,
                Count = model.Count,
                Sum = model.Sum,
                Status = OrderStatus.Принят,
                DateCreate = DateTime.Now
            });
        }

        public void TakeOrderInWork(ChangeStatusBindingModel model)
        {
            var order = _orderStorage.GetElement(new OrderBindingModel{
                Id = model.OrderId
            });
            if (order.Status == "Принят")
            {
                PastryViewModel pastry = _pastryStorage.GetElement(new PastryBindingModel { Id = order.PastryId });
                Dictionary<int, int> components = pastry
                    .PastryComponents
                    .ToDictionary(pastry => pastry.Key, pastry => pastry.Value.Item2 * order.Count);

                if (!_warehouseStorage.CheckBalance(components))
                {
                    throw new Exception("Недостаточно компонентов");
                }
                _warehouseStorage.WriteOffBalance(components);

                _orderStorage.Update(new OrderBindingModel{
                    Id = order.Id,
                    PastryId = order.PastryId,
                    Count = order.Count,
                    Sum = order.Sum,
                    Status = OrderStatus.Выполняется,
                    DateCreate = order.DateCreate,
                    DateImplement = order.DateImplement
                });
            }
        }

        public void FinishOrder(ChangeStatusBindingModel model)
        {
            var order = _orderStorage.GetElement(new OrderBindingModel
            {
                Id = model.OrderId
            });
            if (order.Status == "Выполняется")
            {
                _orderStorage.Update(new OrderBindingModel
                {
                    Id = order.Id,
                    PastryId = order.PastryId,
                    Count = order.Count,
                    Sum = order.Sum,
                    Status = OrderStatus.Готов,
                    DateCreate = order.DateCreate,
                    DateImplement = order.DateImplement
                });
            }
        }

        public void DeliveryOrder(ChangeStatusBindingModel model)
        {
            var order = _orderStorage.GetElement(new OrderBindingModel
            {
                Id = model.OrderId
            });
            if (order.Status == "Готов")
            {
                _orderStorage.Update(new OrderBindingModel
                {
                    Id = order.Id,
                    PastryId = order.PastryId,
                    Count = order.Count,
                    Sum = order.Sum,
                    Status = OrderStatus.Выдан,
                    DateCreate = order.DateCreate,
                    DateImplement = DateTime.Now
                });
            }
        }
    }
}
