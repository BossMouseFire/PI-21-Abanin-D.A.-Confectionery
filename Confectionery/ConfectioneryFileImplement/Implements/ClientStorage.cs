using ConfectioneryContracts.BindingModels;
using ConfectioneryContracts.StoragesContracts;
using ConfectioneryContracts.ViewModels;
using ConfectioneryFileImplement.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConfectioneryFileImplement.Implements
{
    public class ClientStorage : IClientStorage
    {
        private readonly FileDataListSingleton _source;

        public ClientStorage()
        {
            _source = FileDataListSingleton.GetInstance();
        }

        public List<ClientViewModel> GetFullList()
        {
            return _source.Clients.Select(CreateModel).ToList();
        }

        public List<ClientViewModel> GetFilteredList(ClientBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            return _source.Clients
                .Where(rec => rec.Login == model.Email && rec.Password == model.Password)
                .Select(CreateModel).ToList();
        }

        public ClientViewModel GetElement(ClientBindingModel model)
        {
            if (model == null)
            {
                return null;
            }

            var client = _source.Clients.FirstOrDefault(rec => rec.Id == model.Id);
            return client != null ? CreateModel(client) : null;
        }

        public void Insert(ClientBindingModel model)
        {
            int maxId = _source.Clients.Count > 0 ? _source.Clients.Max(rec => rec.Id) : 0;
            var element = new Client { Id = maxId + 1 };
            _source.Clients.Add(CreateModel(model, element));
        }

        public void Update(ClientBindingModel model)
        {
            var element = _source.Clients.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Клиент не найден");
            }

            CreateModel(model, element);
        }

        public void Delete(ClientBindingModel model)
        {
            Client element = _source.Clients.FirstOrDefault(rec => rec.Id == model.Id);
            if (element != null)
            {
                _source.Clients.Remove(element);
            }
            else
            {
                throw new Exception("Клиент не найден");
            }
        }

        private Client CreateModel(ClientBindingModel model, Client client)
        {
            client.Login = model.Email;
            client.Password = model.Password;
            client.FIO = model.FIO;
            return client;
        }

        private ClientViewModel CreateModel(Client client)
        {
            return new ClientViewModel
            {
                Id = client.Id,
                Email = client.Login,
                Password = client.Password,
                FIO = client.FIO,
            };
        }
    }
}