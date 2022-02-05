using System;
using System.Collections.Generic;
using ConfectioneryContracts.BusinessLogicsContracts;
using ConfectioneryContracts.StoragesContracts;
using ConfectioneryContracts.BindingModels;
using ConfectioneryContracts.ViewModels;

namespace ConfectioneryBusinessLogic.BusinessLogics
{
    public class PastryLogic: IPastryLogic
    {
        private readonly IPastryStorage _pastryStorage;
        public PastryLogic(IPastryStorage pastryStorage)
        {
            _pastryStorage = pastryStorage;
        }
        public List<PastryViewModel> Read(PastryBindingModel model)
        {
            if (model == null)
            {
                return _pastryStorage.GetFullList();
            }
            if (model.Id.HasValue)
            {
                return new List<PastryViewModel> { _pastryStorage.GetElement(model) };
            }
            return _pastryStorage.GetFilteredList(model);
        }
        public void CreateOrUpdate(PastryBindingModel model)
        {
            var element = _pastryStorage.GetElement(new PastryBindingModel
            {
                 PastryName = model.PastryName
            });
            if (element != null && element.Id != model.Id)
            {
                throw new Exception("Уже есть изделие с таким названием");
            }
            if (model.Id.HasValue)
            {
                _pastryStorage.Update(model);
            }
            else
            {
                _pastryStorage.Insert(model);
            }
        }
        public void Delete(PastryBindingModel model)
        {
            var element = _pastryStorage.GetElement(new PastryBindingModel
            {
                Id = model.Id
            });
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            _pastryStorage.Delete(model);
        }
    }
}
