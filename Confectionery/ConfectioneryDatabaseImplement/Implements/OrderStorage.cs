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
                    .Select(order => new OrderViewModel
                    {
                        Id = order.Id,
                        PastryId = order.PastryId,
                        PastryName = context.Pastries.Include(pr => pr.Orders).FirstOrDefault(pr => pr.Id == order.PastryId).PastryName,
                        Count = order.Count,
                        Sum = order.Sum,
                        Status = order.Status.ToString(),
                        DateCreate = order.DateCreate,
                        DateImplement = order.DateImplement,
                    }
            )
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
                    .Where(rec => rec.PastryId == model.PastryId)
                    .Select(order => new OrderViewModel
                    {
                        Id = order.Id,
                        PastryId = order.PastryId,
                        PastryName = context.Pastries.Include(pr => pr.Orders).FirstOrDefault(pr => pr.Id == order.PastryId).PastryName,
                        Count = order.Count,
                        Sum = order.Sum,
                        Status = order.Status.ToString(),
                        DateCreate = order.DateCreate,
                        DateImplement = order.DateImplement,
                    }
            )
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
                Order order = context.Orders.FirstOrDefault(rec => rec.Id == model.Id);
                return order != null ?
                new OrderViewModel
                {
                    Id = order.Id,
                    PastryId = order.PastryId,
                    PastryName = context.Pastries.Include(pr => pr.Orders).FirstOrDefault(pr => pr.Id == order.PastryId).PastryName,
                    Count = order.Count,
                    Sum = order.Sum,
                    Status = order.Status.ToString(),
                    DateCreate = order.DateCreate,
                    DateImplement = order.DateImplement,
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

        private static Order CreateModel(OrderBindingModel model, Order order)
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