using ConfectioneryContracts.BindingModels;
using ConfectioneryContracts.StoragesContracts;
using ConfectioneryContracts.ViewModels;
using ConfectioneryFileImplement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
namespace ConfectioneryFileImplement.Implements
{
    public class PastryStorage : IPastryStorage
    {
        private readonly FileDataListSingleton source;
        public PastryStorage()
        {
            source = FileDataListSingleton.GetInstance();
        }
        public List<PastryViewModel> GetFullList()
        {
            return source.Products
            .Select(CreateModel)
            .ToList();
        }
        public List<PastryViewModel> GetFilteredList(PastryBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            return source.Products
            .Where(rec => rec.PastryName.Contains(model.PastryName))
            .Select(CreateModel)
            .ToList();
        }
        public PastryViewModel GetElement(PastryBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            var pastry = source.Products
            .FirstOrDefault(rec => rec.PastryName == model.PastryName || rec.Id
           == model.Id);
            return pastry != null ? CreateModel(pastry) : null;
        }
        public void Insert(PastryBindingModel model)
        {
        int maxId = source.Products.Count > 0 ? source.Components.Max(rec => rec.Id)
: 0;
            var element = new Pastry
            {
                Id = maxId + 1,
                PastryComponents = new
           Dictionary<int, int>()
            };
            source.Products.Add(CreateModel(model, element));
        }
        public void Update(PastryBindingModel model)
        {
            var element = source.Products.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            CreateModel(model, element);
        }
        public void Delete(PastryBindingModel model)
        {
            Pastry element = source.Products.FirstOrDefault(rec => rec.Id == model.Id);
            if (element != null)
            {
                source.Products.Remove(element);
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
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
            return new PastryViewModel
            {
                Id = pastry.Id,
                PastryName = pastry.PastryName,
                Price = pastry.Price,
                PastryComponents = pastry.PastryComponents.ToDictionary(recPC => recPC.Key, recPC =>
                (source.Components.FirstOrDefault(recC => recC.Id ==
                recPC.Key)?.ComponentName, recPC.Value))
            };
        }
    }
}