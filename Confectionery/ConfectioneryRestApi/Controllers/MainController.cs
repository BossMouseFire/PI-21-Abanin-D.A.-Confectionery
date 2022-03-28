using ConfectioneryContracts.BindingModels;
using ConfectioneryContracts.BusinessLogicsContracts;
using ConfectioneryContracts.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace ConfectioneryRestApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MainController : ControllerBase
    {
        private readonly IOrderLogic _order;
        private readonly IPastryLogic _pastry;
        public MainController(IOrderLogic order, IPastryLogic pastry)
        {
            _order = order;
            _pastry = pastry;
        }
        [HttpGet]
        public List<PastryViewModel> GetProductList() => _pastry.Read(null)?.ToList();
        [HttpGet]
        public PastryViewModel GetProduct(int productId) => _pastry.Read(new PastryBindingModel
        { Id = productId })?[0];
        [HttpGet]
        public List<OrderViewModel> GetOrders(int clientId) => _order.Read(new
       OrderBindingModel
        { ClientId = clientId });
        [HttpPost]
        public void CreateOrder(CreateOrderBindingModel model) =>
       _order.CreateOrder(model);
    }
}
