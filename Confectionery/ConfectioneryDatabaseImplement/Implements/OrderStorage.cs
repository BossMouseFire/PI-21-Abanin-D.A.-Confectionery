using Microsoft.EntityFrameworkCore;
using ConfectioneryContracts.BindingModels;
using ConfectioneryContracts.StoragesContracts;
using ConfectioneryContracts.ViewModels;
using ConfectioneryDatabaseImplement.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConfectioneryDatabaseImplement.Implements
{
    public class OrderStorage : IOrderStorage
    {
        public List<OrderViewModel> GetFullList()
        {
            using (var context = new ConfectioneryDatabase())
            {
                return context.Orders
                    .Include(rec => rec.Pastry)
                    .Include(rec => rec.Client)
                    .Select(rec => new OrderViewModel
                    {
                        Id = rec.Id,
                        PastryId = rec.PastryId,
                        PastryName = rec.Pastry.PastryName,
                        Count = rec.Count,
                        Sum = rec.Sum,
                        Status = rec.Status.ToString(),
                        DateCreate = rec.DateCreate,
                        DateImplement = rec.DateImplement,
                        ClientId = rec.ClientId,
                        ClientFIO = rec.Client.FIO
                    })
                    .ToList();
            }
        }

        public List<OrderViewModel> GetFilteredList(OrderBindingModel model)
        {
            if (model == null)
            {
                return null;
            }

            using (var context = new ConfectioneryDatabase())
            {
                return context.Orders
                    .Include(rec => rec.Pastry)
                    .Include(rec => rec.Client)
                    .Where(rec => (!model.DateFrom.HasValue && !model.DateTo.HasValue && rec.DateCreate.Date == model.DateCreate.Date)
                    || (model.DateFrom.HasValue && model.DateTo.HasValue && rec.DateCreate.Date >= model.DateFrom.Value.Date && rec.DateCreate.Date <= model.DateTo.Value.Date)
                    || (model.ClientId.HasValue && rec.ClientId == model.ClientId))
                    .Select(rec => new OrderViewModel
                    {
                        Id = rec.Id,
                        PastryId = rec.PastryId,
                        PastryName = rec.Pastry.PastryName,
                        Count = rec.Count,
                        Sum = rec.Sum,
                        Status = rec.Status.ToString(),
                        DateCreate = rec.DateCreate,
                        DateImplement = rec.DateImplement,
                        ClientId = rec.ClientId,
                        ClientFIO = rec.Client.FIO
                    })
                    .ToList();
            }
        }

        public OrderViewModel GetElement(OrderBindingModel model)
        {
            if (model == null)
            {
                return null;
            }

            using (var context = new ConfectioneryDatabase())
            {
                Order order = context.Orders
                    .Include(rec => rec.Pastry)
                    .Include(rec => rec.Client)
                    .FirstOrDefault(rec => rec.Id == model.Id);
                return order != null ?
                new OrderViewModel
                {
                    Id = order.Id,
                    PastryId = order.PastryId,
                    PastryName = order.Pastry.PastryName,
                    Count = order.Count,
                    Sum = order.Sum,
                    Status = order.Status.ToString(),
                    DateCreate = order.DateCreate,
                    DateImplement = order.DateImplement,
                    ClientId = order.ClientId,
                    ClientFIO = order.Client.FIO
                } :
                null;
            }
        }

        public void Insert(OrderBindingModel model)
        {
            using (var context = new ConfectioneryDatabase())
            {
                var order = new Order
                {
                    PastryId = model.PastryId,
                    Count = model.Count,
                    Sum = model.Sum,
                    Status = model.Status,
                    DateCreate = model.DateCreate,
                    DateImplement = model.DateImplement,
                    ClientId = model.ClientId.Value
                };

                context.Orders.Add(order);
                context.SaveChanges();
                CreateModel(model, order);
                context.SaveChanges();
            }
        }

        public void Update(OrderBindingModel model)
        {
            using (var context = new ConfectioneryDatabase())
            {
                Order order = context.Orders.FirstOrDefault(rec => rec.Id == model.Id);
                if (order == null)
                {
                    throw new Exception("Элемент не найден");
                }
                order.PastryId = model.PastryId;
                order.Count = model.Count;
                order.Sum = model.Sum;
                order.Status = model.Status;
                order.DateCreate = model.DateCreate;
                order.DateImplement = model.DateImplement;
                order.ClientId = model.ClientId.Value;

                CreateModel(model, order);
                context.SaveChanges();
            }
        }

        public void Delete(OrderBindingModel model)
        {
            using (var context = new ConfectioneryDatabase())
            {
                Order order = context.Orders.FirstOrDefault(rec => rec.Id == model.Id);
                if (order != null)
                {
                    context.Orders.Remove(order);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Элемент не найден");
                }
            }
        }

        private Order CreateModel(OrderBindingModel model, Order order)
        {
            if (model == null)
            {
                return null;
            }

            using (var context = new ConfectioneryDatabase())
            {
                Pastry pastry = context.Pastries.FirstOrDefault(rec => rec.Id == model.PastryId);
                if (pastry != null)
                {
                    if (pastry.Orders == null)
                    {
                        pastry.Orders = new List<Order>();
                    }

                    pastry.Orders.Add(order);
                    context.Pastries.Update(pastry);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Элемент не найден");
                }
            }
            return order;
        }
    }
}