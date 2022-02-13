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
        public List<Component> Components { get; set; }
        public List<Order> Orders { get; set; }
        public List<Pastry> Products { get; set; }
        private FileDataListSingleton()
        {
            Components = LoadComponents();
            Orders = LoadOrders();
            Products = LoadProducts();
        }
        public static FileDataListSingleton GetInstance()
        {
            if (instance == null)
            {
                instance = new FileDataListSingleton();
            }
            return instance;
        }
        ~FileDataListSingleton()
        {
            SaveComponents();
            SaveOrders();
            SaveProducts();
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
            // прописать логику
        }
        private List<Pastry> LoadProducts()
        {
            var list = new List<Pastry>();
            if (File.Exists(PastryFileName))
            {
                var xDocument = XDocument.Load(PastryFileName);
                var xElements = xDocument.Root.Elements("Product").ToList();
                foreach (var elem in xElements)
                {
                    var prodComp = new Dictionary<int, int>();
                    foreach (var component in
                   elem.Element("ProductComponents").Elements("ProductComponent").ToList())
                    {
                        prodComp.Add(Convert.ToInt32(component.Element("Key").Value),
                       Convert.ToInt32(component.Element("Value").Value));
                    }
                    list.Add(new Pastry
                    {
                        Id = Convert.ToInt32(elem.Attribute("Id").Value),
                        PastryName = elem.Element("ProductName").Value,
                        Price = Convert.ToDecimal(elem.Element("Price").Value),
                        PastryComponents = prodComp
                    });
                }
            }
            return list;
        }
        private void SaveComponents()
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
        private void SaveOrders()
        {
            // прописать логику
        }
        private void SaveProducts()
        {
            if (Products != null)
            {
                var xElement = new XElement("Products");
                foreach (var product in Products)
                {
                    var compElement = new XElement("ProductComponents");
                    foreach (var component in product.PastryComponents)
                    {
                        compElement.Add(new XElement("ProductComponent",
                        new XElement("Key", component.Key),
                        new XElement("Value", component.Value)));
                    }
                    xElement.Add(new XElement("Product",
                     new XAttribute("Id", product.Id),
                     new XElement("ProductName", product.PastryName),
                     new XElement("Price", product.Price),
                     compElement));
                }
                var xDocument = new XDocument(xElement);
                xDocument.Save(PastryFileName);
            }
        }
    }
}