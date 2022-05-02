using ConfectioneryContracts.Enums;
using ConfectioneryFileImplement.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace ConfectioneryFileImplement
{
    public class FileDataListSingleton
    {
        private static FileDataListSingleton instance;
        private readonly string ComponentFileName = "Component.xml";
        private readonly string OrderFileName = "Order.xml";
        private readonly string PastryFileName = "Pastry.xml";
        private readonly string WarehouseFileName = "Warehouse.xml";
        private readonly string ClientFileName = "Client.xml";
        private readonly string ImplementerFileName = "Implementer.xml";

        public List<Component> Components { get; set; }
        public List<Order> Orders { get; set; }
        public List<Pastry> Pastries { get; set; }
        public List<Warehouse> Warehouses { get; set; }
        public List<Client> Clients { get; set; }
        public List<Implementer> Implementers { get; set; }

        private FileDataListSingleton()
        {
            Components = LoadComponents();
            Orders = LoadOrders();
            Pastries = LoadPastries();
            Warehouses = LoadWarehouses();
            Clients = LoadClients();
            Implementers = LoadImplementers();
        }
        public static FileDataListSingleton GetInstance()
        {
            if (instance == null)
            {
                instance = new FileDataListSingleton();
            }
            return instance;
        }

        public static void SaveData()
        {
            var singleton  = new FileDataListSingleton();
            singleton.SaveComponents(instance.Components);
            singleton.SaveOrders(instance.Orders);
            singleton.SaveParties(instance.Pastries);
            singleton.SaveWarehouses(instance.Warehouses);
            singleton.SaveClients(instance.Clients);
            singleton.SaveImplementers(instance.Implementers);
        }

        private List<Client> LoadClients()
        {
            var list = new List<Client>();
            if (File.Exists(ClientFileName))
            {
                var xDocument = XDocument.Load(ClientFileName);
                var xElements = xDocument.Root.Elements("Client").ToList();
                foreach (var elem in xElements)
                {
                    list.Add(new Client
                    {
                        Id = Convert.ToInt32(elem.Attribute("Id").Value),
                        FIO = elem.Element("FIO").Value,
                        Login = elem.Element("Login").Value,
                        Password = elem.Element("Password").Value
                    }) ;
                }
            }
            return list;
        }

        private List<Component> LoadComponents()
        {
            var list = new List<Component>();
            if (File.Exists(ComponentFileName))
            {
                var xDocument = XDocument.Load(ComponentFileName);
                var xElements = xDocument.Root.Elements("Component").ToList();
                foreach (var elem in xElements)
                {
                    list.Add(new Component
                    {
                        Id = Convert.ToInt32(elem.Attribute("Id").Value),
                        ComponentName = elem.Element("ComponentName").Value
                    });
                }
            }
            return list;
        }
        private List<Order> LoadOrders()
        {
            var list = new List<Order>();
            if (File.Exists(OrderFileName))
            {
                var xDocument = XDocument.Load(OrderFileName);
                var xElements = xDocument.Root.Elements("Order").ToList();
                foreach (var elem in xElements)
                {
                    var dataCreate = elem.Element("DateCreate").Value;
                    var dataImplement = elem.Element("DateImplement").Value;

                    DateTime? dataResult = null;
                    int? implementerId = null;

                    if (dataImplement != "")
                    {
                        dataResult = DateTime.ParseExact(dataImplement, "yyyy-MM-dd hh:mm",
                                           null);
                    }
                    if (elem.Element("ImplementerId").Value != "")
                    {
                        implementerId = Convert.ToInt32(elem.Element("ImplementerId").Value);
                    }

                    list.Add(new Order
                    {
                        Id = Convert.ToInt32(elem.Attribute("Id").Value),
                        ClientId = Convert.ToInt32(elem.Element("ClientId").Value),
                        PastryId = Convert.ToInt32(elem.Element("PastryId").Value),
                        ImplementerId = implementerId,
                        Count = Convert.ToInt32(elem.Element("Count").Value),
                        Sum = Convert.ToInt32(elem.Element("Sum").Value),
                        Status = (OrderStatus)Enum.Parse(typeof(OrderStatus), elem.Element("Status").Value),
                        DateCreate = DateTime.ParseExact(dataCreate, "yyyy-MM-dd hh:mm",
                                           null),
                        DateImplement = dataResult,
                    });
                }
            }
            return list;
        }
        private List<Pastry> LoadPastries()
        {
            var list = new List<Pastry>();
            if (File.Exists(PastryFileName))
            {
                var xDocument = XDocument.Load(PastryFileName);
                var xElements = xDocument.Root.Elements("Pastry").ToList();
                foreach (var elem in xElements)
                {
                    var prodComp = new Dictionary<int, int>();
                    foreach (var component in
                   elem.Element("PastryComponents").Elements("PastryComponent").ToList())
                    {
                        prodComp.Add(Convert.ToInt32(component.Element("Key").Value),
                       Convert.ToInt32(component.Element("Value").Value));
                    }
                    list.Add(new Pastry
                    {
                        Id = Convert.ToInt32(elem.Attribute("Id").Value),
                        PastryName = elem.Element("PastryName").Value,
                        Price = Convert.ToDecimal(elem.Element("Price").Value),
                        PastryComponents = prodComp
                    });
                }
            }
            return list;
        }

        private List<Warehouse> LoadWarehouses()
        {
            var list = new List<Warehouse>();
            if (File.Exists(WarehouseFileName))
            {
                var xDocument = XDocument.Load(WarehouseFileName);
                var xElements = xDocument.Root.Elements("Warehouse").ToList();
                foreach (var elem in xElements)
                {
                    var warComp = new Dictionary<int, int>();
                    foreach (var component in
                        elem.Element("WarehouseComponents").Elements("WarehouseComponent").ToList())
                    {
                        warComp.Add(Convert.ToInt32(component.Element("Key").Value),
                            Convert.ToInt32(component.Element("Value").Value));
                    }
                    list.Add(new Warehouse
                    {
                        Id = Convert.ToInt32(elem.Attribute("Id").Value),
                        WarehouseName = elem.Element("WarehouseName").Value,
                        Responsible = elem.Element("Responsible").Value,
                        DateCreate = DateTime.Parse(elem.Element("DateCreate").Value),
                        WarehouseComponents = warComp
                    });
                }
            }
            return list;
        }
        private List<Implementer> LoadImplementers()
        {
            var list = new List<Implementer>();
            if (File.Exists(ImplementerFileName))
            {
                var xDocument = XDocument.Load(ImplementerFileName);
                var xElements = xDocument.Root.Elements("Imlementer").ToList();
                foreach (var elem in xElements)
                {
                    list.Add(new Implementer
                    {
                        Id = Convert.ToInt32(elem.Attribute("Id").Value),
                        ImplementerFIO = elem.Attribute("ImplementerFIO").Value,
                        PauseTime = Convert.ToInt32(elem.Attribute("PauseTime").Value),
                        WorkingTime = Convert.ToInt32(elem.Attribute("WorkingTime").Value)
                    });
                }
            }
            return list;
        }
        private void SaveClients(List<Client> Clients)
        {
            if (Clients != null)
            {
                var xElement = new XElement("Clients");
                foreach (var client in Clients)
                {
                    xElement.Add(new XElement("Client",
                    new XAttribute("Id", client.Id),
                    new XElement("FIO", client.FIO)));
                }
                var xDocument = new XDocument(xElement);
                xDocument.Save(ClientFileName);
            }
        }
        private void SaveComponents(List<Component> Components)
        {
            if (Components != null)
            {
                var xElement = new XElement("Components");
                foreach (var component in Components)
                {
                    xElement.Add(new XElement("Component",
                    new XAttribute("Id", component.Id),
                    new XElement("ComponentName", component.ComponentName)));
                }
                var xDocument = new XDocument(xElement);
                xDocument.Save(ComponentFileName);
            }
        }
        private void SaveOrders(List<Order> Orders)
        {
            if (Orders != null)
            {
                var xElement = new XElement("Orders");
                foreach (var order in Orders)
                {
                    xElement.Add(new XElement("Order",
                        new XAttribute("Id", order.Id),
                        new XElement("ClientId", order.ClientId),
                        new XElement("PastryId", order.PastryId),
                        new XElement("ImplementerId", order.ImplementerId),
                        new XElement("Count", order.Count),
                        new XElement("Sum", order.Sum),
                        new XElement("Status", order.Status),
                        new XElement("DateCreate", order.DateCreate.ToString("yyyy-MM-dd hh:mm")),
                        new XElement("DateImplement", order.DateImplement?.ToString("yyyy-MM-dd hh:mm"))
                        ));
                }
                var xDocument = new XDocument(xElement);
                xDocument.Save(OrderFileName);
            }
        }
        private void SaveParties(List<Pastry> Pastries)
        {
            if (Pastries != null)
            {
                var xElement = new XElement("Pastries");
                foreach (var product in Pastries)
                {
                    var compElement = new XElement("PastryComponents");
                    foreach (var component in product.PastryComponents)
                    {
                        compElement.Add(new XElement("PastryComponent",
                        new XElement("Key", component.Key),
                        new XElement("Value", component.Value)));
                    }
                    xElement.Add(new XElement("Pastry",
                     new XAttribute("Id", product.Id),
                     new XElement("PastryName", product.PastryName),
                     new XElement("Price", product.Price),
                     compElement));
                }
                var xDocument = new XDocument(xElement);
                xDocument.Save(PastryFileName);
            }
        }
        private void SaveWarehouses(List<Warehouse> Warehouses)
        {
            if (Warehouses != null)
            {
                var xElement = new XElement("Warehouses");
                foreach (var warehouse in Warehouses)
                {
                    var compElement = new XElement("WarehouseComponents");
                    foreach (var component in warehouse.WarehouseComponents)
                    {
                        compElement.Add(new XElement("WarehouseComponent",
                            new XElement("Key", component.Key),
                            new XElement("Value", component.Value)));
                    }
                    xElement.Add(new XElement("Warehouse",
                        new XAttribute("Id", warehouse.Id),
                        new XElement("WarehouseName", warehouse.WarehouseName),
                        new XElement("Responsible", warehouse.Responsible),
                        new XElement("DateCreate", warehouse.DateCreate),
                        compElement));
                }
                var xDocument = new XDocument(xElement);
                xDocument.Save(WarehouseFileName);
            }
        }
        private void SaveImplementers(List<Implementer> Implementers)
        {
            if (Implementers != null)
            {
                var xElement = new XElement("Implementers");
                foreach (var implementer in Implementers)
                {
                    xElement.Add(new XElement("Implementer",
                        new XAttribute("Id", implementer.Id),
                        new XAttribute("ImplementerFIO", implementer.ImplementerFIO),
                        new XAttribute("WorkingTime", implementer.WorkingTime),
                        new XAttribute("PauseTime", implementer.PauseTime)));
                }
                var xDocument = new XDocument(xElement);
                xDocument.Save(ImplementerFileName);
            }
        }
    }
}