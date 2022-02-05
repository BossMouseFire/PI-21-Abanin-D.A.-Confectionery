using ConfectioneryContracts.BindingModels;
using ConfectioneryContracts.StoragesContracts;
using ConfectioneryContracts.ViewModels;
using ConfectioneryListImplement.Models;
using System;
using System.Collections.Generic;
using System.Linq;


namespace ConfectioneryListImplement.Implements
{
    public class PastryStorage : IPastryStorage
    {
        private readonly DataListSingleton source;
        public PastryStorage()
        {
            source = DataListSingleton.GetInstance();
        }
        public List<PastryViewModel> GetFullList()
        {
            var result = new List<PastryViewModel>();
            foreach (var component in source.Products)
            {
                result.Add(CreateModel(component));
            }
            return result;
        }
        public List<PastryViewModel> GetFilteredList(PastryBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            var result = new List<PastryViewModel>();
            foreach (var product in source.Products)
            {
                if (product.PastryName.Contains(model.PastryName))
                {
                    result.Add(CreateModel(product));
                }
            }
            return result;
        }
        public PastryViewModel GetElement(PastryBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            foreach (var product in source.Products)
            {
                if (product.Id == model.Id || product.PastryName ==
                model.PastryName)
                {
                    return CreateModel(product);
                }
            }
            return null;
        }
        public void Insert(PastryBindingModel model)
        {
            var tempPastry = new Pastry
            {
                Id = 1,
                PastryComponents = new
            Dictionary<int, int>()
            };
            foreach (var product in source.Products)
            {
                if (product.Id >= tempPastry.Id)
                {
                    tempPastry.Id = product.Id + 1;
                }
            }
            source.Products.Add(CreateModel(model, tempPastry));
        }
        public void Update(PastryBindingModel model)
        {
            Pastry tempPastry = null;
            foreach (var product in source.Products)
            {
                if (product.Id == model.Id)
                {
                    tempPastry = product;
                }
            }
            if (tempPastry == null)
            {
                throw new Exception("Элемент не найден");
            }
            CreateModel(model, tempPastry);
        }
        public void Delete(PastryBindingModel model)
        {
            for (int i = 0; i < source.Products.Count; ++i)
            {
                if (source.Products[i].Id == model.Id)
                {
                    source.Products.RemoveAt(i);
                    return;
                }
            }
            throw new Exception("Элемент не найден");
        }
        private static Pastry CreateModel(PastryBindingModel model, Pastry product)
        {
            product.PastryName = model.PastryName;
            product.Price = model.Price;
            // удаляем убранные
            foreach (var key in product.PastryComponents.Keys.ToList())
            {
                if (!model.PastryComponents.ContainsKey(key))
                {
                    product.PastryComponents.Remove(key);
                }
            }
            // обновляем существуюущие и добавляем новые
            foreach (var component in model.PastryComponents)
            {
                if (product.PastryComponents.ContainsKey(component.Key))
                {
                    product.PastryComponents[component.Key] =
                    model.PastryComponents[component.Key].Item2;
                }
                else
                {
                    product.PastryComponents.Add(component.Key,
                    model.PastryComponents[component.Key].Item2);
                }
            }
            return product;
        }
        private PastryViewModel CreateModel(Pastry product)
        {
            var pastryComponents = new Dictionary<int, (string, int)>();
            foreach (var pc in product.PastryComponents)
            {
                string componentName = string.Empty;
                foreach (var component in source.Components)
                {
                    if (pc.Key == component.Id)
                    {
                        componentName = component.ComponentName;
                        break;
                    }
                }
                pastryComponents.Add(pc.Key, (componentName, pc.Value));
            }
            return new PastryViewModel
            {
                Id = product.Id,
                PastryName = product.PastryName,
                Price = product.Price,
                PastryComponents = pastryComponents
            };
        }
    }
}
