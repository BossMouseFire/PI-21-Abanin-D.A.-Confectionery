using System;
using System.Collections.Generic;
using System.Linq;
using ConfectioneryContracts.BindingModels;
using ConfectioneryContracts.StoragesContracts;
using ConfectioneryContracts.ViewModels;
using ConfectioneryDatabaseImplement.Models;

namespace ConfectioneryDatabaseImplement.Implements
{
    public class ImplementerStorage : IImplementerStorage
    {
        public List<ImplementerViewModel> GetFullList()
        {
            using var context = new ConfectioneryDatabase();
            return context.Implementers.Select(CreateModel).ToList();
        }
        public List<ImplementerViewModel> GetFilteredList(ImplementerBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new ConfectioneryDatabase();
            return context.Implementers.Where(rec => rec.ImplementerFIO == model.ImplementerFIO).Select(CreateModel).ToList();
        }
        public ImplementerViewModel GetElement(ImplementerBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new ConfectioneryDatabase();
            var implementer = context.Implementers.FirstOrDefault(rec => rec.Id == model.Id || rec.ImplementerFIO == model.ImplementerFIO);
            return implementer != null ? CreateModel(implementer) : null;
        }
        public void Insert(ImplementerBindingModel model)
        {
            using var context = new ConfectioneryDatabase();
            context.Implementers.Add(CreateModel(model, new Implementer()));
            context.SaveChanges();
        }
        public void Update(ImplementerBindingModel model)
        {
            using var context = new ConfectioneryDatabase();
            var element = context.Implementers.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Исполнитель не найден");
            }
            CreateModel(model, element);
            context.SaveChanges();
        }
        public void Delete(ImplementerBindingModel model)
        {
            using var context = new ConfectioneryDatabase();
            Implementer element = context.Implementers.FirstOrDefault(rec => rec.Id == model.Id);
            if (element != null)
            {
                context.Implementers.Remove(element);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Исполнитель не найден");
            }
        }
        private static Implementer CreateModel(ImplementerBindingModel model, Implementer
            implementer)
        {
            implementer.ImplementerFIO = model.ImplementerFIO;
            implementer.PauseTime = model.PauseTime;
            implementer.WorkingTime = model.WorkingTime;
            return implementer;
        }
        private ImplementerViewModel CreateModel(Implementer implementer)
        {
            return new ImplementerViewModel
            {
                Id = implementer.Id,
                ImplementerFIO = implementer.ImplementerFIO,
                WorkingTime = implementer.WorkingTime,
                PauseTime = implementer.PauseTime
            };
        }
    }
}
