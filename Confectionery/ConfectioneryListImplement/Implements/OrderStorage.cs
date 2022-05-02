using ConfectioneryContracts.BindingModels;
using ConfectioneryContracts.StoragesContracts;
using ConfectioneryContracts.ViewModels;
using ConfectioneryListImplement.Models;
using System;
using System.Collections.Generic;

namespace ConfectioneryListImplement.Implements
{
    public class OrderStorage : IOrderStorage
    {
        private readonly DataListSingleton _source;

        public OrderStorage()
        {
            _source = DataListSingleton.GetInstance();
        }

        public List<OrderViewModel> GetFullList()
        {
            List<OrderViewModel> result = new List<OrderViewModel>();
            foreach (var component in _source.Orders)
            {
                result.Add(CreateModel(component));
            }
            return result;
        }

        public List<OrderViewModel> GetFilteredList(OrderBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            List<OrderViewModel> result = new List<OrderViewModel>();
            foreach (var order in _source.Orders)
            {
                if ((!model.DateFrom.HasValue && !model.DateTo.HasValue && order.DateCreate.Date == model.DateCreate.Date)
                    || (model.DateFrom.HasValue && model.DateTo.HasValue && order.DateCreate.Date >= model.DateFrom.Value.Date && order.DateCreate.Date <= model.DateTo.Value.Date)
                    || (model.ClientId.HasValue && order.ClientId == model.ClientId))
                {
                    result.Add(CreateModel(order));
                }
            }
            return result;
        }

        public OrderViewModel GetElement(OrderBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            foreach (var Order in _source.Orders)
            {
                if (Order.Id == model.Id)
                {
                    return CreateModel(Order);
                }
            }
            return null;
        }

        public void Insert(OrderBindingModel model)
        {
            Order tempOrder = new Order { Id = 1 };
            foreach (var Order in _source.Orders)
            {
                if (Order.Id >= tempOrder.Id)
                {
                    tempOrder.Id = Order.Id + 1;
                }
            }
            _source.Orders.Add(CreateModel(model, tempOrder));
        }

        public void Update(OrderBindingModel model)
        {
            Order tmpOrder = null;
            foreach (var Order in _source.Orders)
            {
                if (Order.Id == model.Id)
                {
                    tmpOrder = Order;
                }
            }
            if (tmpOrder == null)
            {
                throw new Exception("Элемент не найден");
            }
            CreateModel(model, tmpOrder);
        }

        public void Delete(OrderBindingModel model)
        {
            for (int i = 0; i < _source.Orders.Count; ++i)
            {
                if (_source.Orders[i].Id == model.Id)
                {
                    _source.Orders.RemoveAt(i);
                    return;
                }
            }
            throw new Exception("Элемент не найден");
        }

        private Order CreateModel(OrderBindingModel model, Order order)
        {
            order.PastryId = model.PastryId;
            order.Count = model.Count;
            order.Status = model.Status;
            order.Sum = model.Sum;
            order.DateCreate = model.DateCreate;
            order.DateImplement = model.DateImplement;
            return order;
        }

        private OrderViewModel CreateModel(Order order)
        {
            string pastryName = "";
            foreach (var pastry in _source.Pastries)
            {
                if (order.PastryId == pastry.Id)
                {
                    pastryName = pastry.PastryName;
                    break;
                }
            }
            string clientFIO = null;
            foreach (var client in _source.Clients)
            {
                if (client.Id == order.ClientId)
                {
                    clientFIO = client.FIO;
                    break;
                }
            }
            return new OrderViewModel
            {
                Id = order.Id,
                ClientId = order.ClientId,
                ClientFIO = clientFIO,
                PastryId = order.PastryId,
                PastryName = pastryName,
                Count = order.Count,
                Status = order.Status.ToString(),
                Sum = order.Sum,
                DateCreate = order.DateCreate,
                DateImplement = order.DateImplement,
            };
        }
    }
}