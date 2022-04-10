using ConfectioneryContracts.BindingModels;
using ConfectioneryContracts.StoragesContracts;
using ConfectioneryContracts.ViewModels;
using ConfectioneryListImplement.Models;
using System;
using System.Collections.Generic;

namespace ConfectioneryListImplement.Implements
{
    public class ClientStorage : IClientStorage
    {
        private readonly DataListSingleton _source;

        public ClientStorage()
        {
            _source = DataListSingleton.GetInstance();
        }

        public List<ClientViewModel> GetFullList()
        {
            var result = new List<ClientViewModel>();

            foreach (var client in _source.Clients)
            {
                result.Add(CreateModel(client));
            }

            return result;
        }

        public List<ClientViewModel> GetFilteredList(ClientBindingModel model)
        {
            if (model == null)
            {
                return null;
            }

            var result = new List<ClientViewModel>();

            foreach (var client in _source.Clients)
            {
                if (client.Login.Contains(model.Email))
                {
                    result.Add(CreateModel(client));
                }
            }

            return result;
        }

        public ClientViewModel GetElement(ClientBindingModel model)
        {
            if (model == null)
            {
                return null;
            }

            foreach (var client in _source.Clients)
            {
                if (client.Id == model.Id)
                {
                    return CreateModel(client);
                }
            }

            return null;
        }

        public void Insert(ClientBindingModel model)
        {
            var tmpClient = new Client { Id = 1 };

            foreach (var client in _source.Clients)
            {
                if (client.Id >= tmpClient.Id)
                {
                    tmpClient.Id = client.Id + 1;
                }
            }

            _source.Clients.Add(CreateModel(model, tmpClient));
        }

        public void Update(ClientBindingModel model)
        {
            Client tmpClient = null;

            foreach (var client in _source.Clients)
            {
                if (client.Id == model.Id)
                {
                    tmpClient = client;
                }
            }

            if (tmpClient == null)
            {
                throw new Exception("Элемент не найден");
            }

            CreateModel(model, tmpClient);
        }

        public void Delete(ClientBindingModel model)
        {
            for (int i = 0; i < _source.Clients.Count; ++i)
            {
                if (_source.Clients[i].Id == model.Id.Value)
                {
                    _source.Clients.RemoveAt(i);
                    return;
                }
            }
            throw new Exception("Элемент не найден");
        }

        private Client CreateModel(ClientBindingModel model, Client client)
        {
            client.FIO = model.FIO;
            client.Login = model.Email;
            client.Password = model.Password;
            return client;
        }

        private ClientViewModel CreateModel(Client client)
        {
            return new ClientViewModel
            {
                Id = client.Id,
                FIO = client.FIO,
                Email = client.Login,
                Password = client.Password
            };
        }
    }
}