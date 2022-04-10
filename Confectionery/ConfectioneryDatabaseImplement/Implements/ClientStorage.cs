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
    public class ClientStorage : IClientStorage
    {
        public List<ClientViewModel> GetFullList()
        {
            using (var context = new ConfectioneryDatabase())
            {
                return context.Clients
                    .Select(rec => new ClientViewModel
                    {
                        Id = rec.Id,
                        FIO = rec.FIO,
                        Email = rec.Login,
                        Password = rec.Password
                    })
                .ToList();
            }
        }

        public List<ClientViewModel> GetFilteredList(ClientBindingModel model)
        {
            if (model == null)
            {
                return null;
            }

            using (var context = new ConfectioneryDatabase())
            {
                return context.Clients
                    .Include(x => x.Orders)
                    .Where(rec => rec.Login == model.Email && rec.Password == model.Password)
                    .Select(rec => new ClientViewModel
                    {
                        Id = rec.Id,
                        FIO = rec.FIO,
                        Email = rec.Login,
                        Password = rec.Password
                    })
                .ToList();
            }
        }

        public ClientViewModel GetElement(ClientBindingModel model)
        {
            if (model == null)
            {
                return null;
            }

            using (var context = new ConfectioneryDatabase())
            {
                Client client = context.Clients
                    .Include(x => x.Orders)
                    .FirstOrDefault(rec => rec.Login == model.Email || rec.Id == model.Id);
                return client != null ?
                new ClientViewModel
                {
                    Id = client.Id,
                    FIO = client.FIO,
                    Email = client.Login,
                    Password = client.Password
                } :
                null;
            }
        }

        public void Insert(ClientBindingModel model)
        {
            using (var context = new ConfectioneryDatabase())
            {
                context.Clients.Add(CreateModel(model, new Client()));
                context.SaveChanges();
            }
        }

        public void Update(ClientBindingModel model)
        {
            using (var context = new ConfectioneryDatabase())
            {
                Client element = context.Clients.FirstOrDefault(rec => rec.Id == model.Id);
                if (element == null)
                {
                    throw new Exception("Клиент не найден");
                }

                CreateModel(model, element);
                context.SaveChanges();
            }
        }

        public void Delete(ClientBindingModel model)
        {
            using (var context = new ConfectioneryDatabase())
            {
                Client element = context.Clients.FirstOrDefault(rec => rec.Id == model.Id);
                if (element != null)
                {
                    context.Clients.Remove(element);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Клиент не найден");
                }
            }
        }

        private Client CreateModel(ClientBindingModel model, Client client)
        {
            client.FIO = model.FIO;
            client.Login = model.Email;
            client.Password = model.Password;
            return client;
        }
    }
}