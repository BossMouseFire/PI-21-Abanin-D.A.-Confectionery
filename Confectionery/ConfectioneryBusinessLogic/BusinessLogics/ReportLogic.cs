using ConfectioneryBusinessLogic.OfficePackage;
using ConfectioneryBusinessLogic.OfficePackage.HelperModels;
using ConfectioneryContracts.BindingModels;
using ConfectioneryContracts.BusinessLogicsContracts;
using ConfectioneryContracts.StoragesContracts;
using ConfectioneryContracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConfectioneryBusinessLogic.BusinessLogics
{
    public class ReportLogic : IReportLogic
    {
        private readonly IComponentStorage _componentStorage;
        private readonly IPastryStorage _productStorage;
        private readonly IOrderStorage _orderStorage;
        private readonly IWarehouseStorage _warehouseStorage;
        private readonly AbstractSaveToExcel _saveToExcel;
        private readonly AbstractSaveToWord _saveToWord;
        private readonly AbstractSaveToPdf _saveToPdf;
        public ReportLogic(IPastryStorage productStorage, IComponentStorage componentStorage, 
            IOrderStorage orderStorage, IWarehouseStorage warehouseStorage, 
            AbstractSaveToExcel saveToExcel, AbstractSaveToWord saveToWord, 
            AbstractSaveToPdf saveToPdf)
        {
            _productStorage = productStorage;
            _componentStorage = componentStorage;
            _orderStorage = orderStorage;
            _saveToExcel = saveToExcel;
            _saveToWord = saveToWord;
            _saveToPdf = saveToPdf;
            _warehouseStorage = warehouseStorage;
        }
        /// <summary>
        /// Получение списка компонент с указанием, в каких изделиях используются
        /// </summary>
        /// <returns></returns>
        public List<ReportPastryComponentViewModel> GetPastryComponent()
        {
            var products = _productStorage.GetFullList();
            var list = new List<ReportPastryComponentViewModel>();
            foreach (var product in products)
            {
                var record = new ReportPastryComponentViewModel
                {
                    PastryName = product.PastryName,
                    Components = new List<Tuple<string, int>>(),
                    TotalCount = 0
                };
                foreach (var component in product.PastryComponents)
                {
                    record.Components.Add(new Tuple<string, int>(component.Value.Item1,
                            component.Value.Item2));
                    record.TotalCount += component.Value.Item2;
                }
                list.Add(record);
            }
            return list;
        }
        public List<ReportWarehouseComponentViewModel> GetWarehouseComponent()
        {
            var warehouses = _warehouseStorage.GetFullList();
            var list = new List<ReportWarehouseComponentViewModel>();
            foreach (var warehouse in warehouses)
            {
                var record = new ReportWarehouseComponentViewModel
                {
                    WarehouseName = warehouse.WarehouseName,
                    Components = new List<Tuple<string, int>>(),
                    TotalCount = 0
                };
                foreach (var component in warehouse.WarehouseComponents)
                {
                    record.Components.Add(new Tuple<string, int>(component.Value.Item1, component.Value.Item2));
                    record.TotalCount += component.Value.Item2;
                }
                list.Add(record);
            }
            return list;
        }

        /// <summary>
        /// Получение списка заказов за определенный период
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public List<ReportOrdersViewModel> GetOrders(ReportBindingModel model)
        {
            return _orderStorage.GetFilteredList(new OrderBindingModel
            {
                DateFrom = model.DateFrom,
                DateTo = model.DateTo
            })
            .Select(x => new ReportOrdersViewModel
            {
                DateCreate = x.DateCreate,
                PastryName = x.PastryName,
                Count = x.Count,
                Sum = x.Sum,
                Status = x.Status
            })
           .ToList();
        }

        public List<ReportOrdersByDateViewModel> GetOrdersByDate()
        {
            return _orderStorage.GetFullList()
            .GroupBy(rec => rec.DateCreate.ToShortDateString())
            .Select(x => new ReportOrdersByDateViewModel
            {
                DateCreate = Convert.ToDateTime(x.Key),
                Count = x.Count(),
                Sum = x.Sum(rec => rec.Sum)
            })
           .ToList();
        }

        /// <summary>
        /// Сохранение компонент в файл-Word
        /// </summary>
        /// <param name="model"></param>
        public void SaveComponentsToWordFile(ReportBindingModel model)
        {
            _saveToWord.CreateDoc(new WordInfo
            {
                FileName = model.FileName,
                Title = "Список компонент",
                Pastries = _productStorage.GetFullList()
            });
        }
        public void SaveWarehouseComponentToExcelFile(ReportBindingModel model)
        {
            _saveToExcel.CreateReportWarehouse(new ExcelInfo
            {
                FileName = model.FileName,
                Title = "Список компонент",
                WarehouseComponents = GetWarehouseComponent()
            });
        }
        /// <summary>
        /// Сохранение компонент с указаеним продуктов в файл-Excel
        /// </summary>
        /// <param name="model"></param>
        public void SavePastryComponentToExcelFile(ReportBindingModel model)
        {
            _saveToExcel.CreateReport(new ExcelInfo
            {
                FileName = model.FileName,
                Title = "Список компонент",
                PastryComponents = GetPastryComponent()
            });
        }
        /// <summary>
        /// Сохранение заказов в файл-Pdf
        /// </summary>
        /// <param name="model"></param>
        public void SaveOrdersToPdfFile(ReportBindingModel model)
        {
            _saveToPdf.CreateDoc(new PdfInfo
            {
                FileName = model.FileName,
                Title = "Список заказов",
                DateFrom = model.DateFrom.Value,
                DateTo = model.DateTo.Value,
                Orders = GetOrders(model)
            });
        }
        public void SaveWarehousesToWordFile(ReportBindingModel model)
        {
            _saveToWord.CreateDocWarehouse(new WordInfo
            {
                FileName = model.FileName,
                Title = "Список складов",
                Warehouses = _warehouseStorage.GetFullList()
            });
        }
        public void SaveOrdersByDateToPdfFile(ReportBindingModel model)
        {
            _saveToPdf.CreateDocOrdersByDate(new PdfInfo
            {
                FileName = model.FileName,
                Title = "Список заказов по датам",
                OrdersByDate = GetOrdersByDate()
            });
        }
    }
}
