using System;
using System.Collections.Generic;
using ConfectioneryContracts.BusinessLogicsContracts;
using ConfectioneryContracts.StoragesContracts;
using ConfectioneryContracts.BindingModels;
using ConfectioneryContracts.ViewModels;
using ConfectioneryContracts.Enums;
using ConfectioneryBusinessLogic.MailWorker;

namespace ConfectioneryBusinessLogic.BusinessLogics
{
    public class OrderLogic : IOrderLogic
    {
        private readonly IOrderStorage _orderStorage;
        private readonly AbstractMailWorker _mailWorker;
        private readonly IClientStorage _clientStorage;
        public OrderLogic(IOrderStorage orderStorage, AbstractMailWorker mailWorker, IClientStorage clientStorage)
        {
            _orderStorage = orderStorage;
            _mailWorker = mailWorker;
            _clientStorage = clientStorage;
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

            _mailWorker.MailSendAsync(new MailSendInfoBindingModel
            {
                MailAddress = _clientStorage.GetElement(new ClientBindingModel { Id = model.ClientId })?.Email,
                Subject = "Создан новый заказ",
                Text = $"Дата заказа: {DateTime.Now}, сумма заказа: {model.Sum}"
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
                    DateImplement = order.DateImplement,
                    ImplementerId = model.ImplementerId
                });
            }
            _mailWorker.MailSendAsync(new MailSendInfoBindingModel
            {
                MailAddress = _clientStorage.GetElement(new ClientBindingModel { Id = order.ClientId })?.Email,
                Subject = $"Смена статуса заказа№ {order.Id}",
                Text = $"Статус изменен на: {OrderStatus.Выполняется}"
            });
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
                    DateImplement = order.DateImplement,
                    ImplementerId = order.ImplementerId
                });
            }
            _mailWorker.MailSendAsync(new MailSendInfoBindingModel
            {
                MailAddress = _clientStorage.GetElement(new ClientBindingModel { Id = order.ClientId })?.Email,
                Subject = $"Смена статуса заказа№ {order.Id}",
                Text = $"Статус изменен на: {OrderStatus.Готов}"
            });
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
                    DateImplement = DateTime.Now,
                    ImplementerId = order.ImplementerId
                });
            }
            _mailWorker.MailSendAsync(new MailSendInfoBindingModel
            {
                MailAddress = _clientStorage.GetElement(new ClientBindingModel { Id = order.ClientId })?.Email,
                Subject = $"Смена статуса заказа№ {order.Id}",
                Text = $"Статус изменен на: {OrderStatus.Выдан}"
            });
        }
    }
}
