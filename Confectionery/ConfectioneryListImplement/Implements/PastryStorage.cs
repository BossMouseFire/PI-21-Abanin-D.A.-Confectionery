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
            foreach (var component in source.Pastries)
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
            foreach (var product in source.Pastries)
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
            foreach (var product in source.Pastries)
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
            foreach (var product in source.Pastries)
            {
                if (product.Id >= tempPastry.Id)
                {
                    tempPastry.Id = product.Id + 1;
                }
            }
            source.Pastries.Add(CreateModel(model, tempPastry));
        }
        public void Update(PastryBindingModel model)
        {
            Pastry tempPastry = null;
            foreach (var product in source.Pastries)
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
            for (int i = 0; i < source.Pastries.Count; ++i)
            {
                if (source.Pastries[i].Id == model.Id)
                {
                    source.Pastries.RemoveAt(i);
                    return;
                }
            }
            throw new Exception("Элемент не найден");
        }
        private static Pastry CreateModel(PastryBindingModel model, Pastry pastry)
        {
            pastry.PastryName = model.PastryName;
            pastry.Price = model.Price;

            // удаляем убранные
            foreach (var key in pastry.PastryComponents.Keys.ToList())
            {
                if (!model.PastryComponents.ContainsKey(key))
                {
                    pastry.PastryComponents.Remove(key);
                }
            }
            // обновляем существуюущие и добавляем новые
            foreach (var component in model.PastryComponents)
            {
                if (pastry.PastryComponents.ContainsKey(component.Key))
                {
                    pastry.PastryComponents[component.Key] =
                    model.PastryComponents[component.Key].Item2;
                }
                else
                {
                    pastry.PastryComponents.Add(component.Key,
                    model.PastryComponents[component.Key].Item2);
                }
            }
            return pastry;
        }
        private PastryViewModel CreateModel(Pastry pastry)
        {
            var pastryComponents = new Dictionary<int, (string, int)>();
            foreach (var pc in pastry.PastryComponents)
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
                Id = pastry.Id,
                PastryName = pastry.PastryName,
                Price = pastry.Price,
                PastryComponents = pastryComponents
            };
        }
    }
}
