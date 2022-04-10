using System;
using System.Collections.Generic;
using ConfectioneryContracts.BusinessLogicsContracts;
using ConfectioneryContracts.StoragesContracts;
using ConfectioneryContracts.BindingModels;
using ConfectioneryContracts.ViewModels;
using ConfectioneryContracts.Enums;

namespace ConfectioneryBusinessLogic.BusinessLogics
{
    public class OrderLogic : IOrderLogic
    {
        private readonly IOrderStorage _orderStorage;
        public OrderLogic(IOrderStorage orderStorage)
        {
            _orderStorage = orderStorage;
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
                ClientId = model.ClientId,
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
                _orderStorage.Update(new OrderBindingModel{
                    Id = order.Id,
                    ClientId = order.ClientId,
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
                    ClientId = order.ClientId,
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
                    ClientId = order.ClientId,
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
