using ConfectioneryContracts.BindingModels;
using ConfectioneryContracts.ViewModels;
using System.Collections.Generic;

namespace ConfectioneryContracts.BusinessLogicsContracts
{
    public interface IReportLogic
    {
        List<ReportPastryComponentViewModel> GetPastryComponent();
        List<ReportWarehouseComponentViewModel> GetWarehouseComponent();
        List<ReportOrdersViewModel> GetOrders(ReportBindingModel model);
        void SaveComponentsToWordFile(ReportBindingModel model);
        void SavePastryComponentToExcelFile(ReportBindingModel model);
        void SaveWarehouseComponentToExcelFile(ReportBindingModel model);
        void SaveOrdersToPdfFile(ReportBindingModel model);
        void SaveWarehousesToWordFile(ReportBindingModel model);
    }
}
