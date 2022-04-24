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
        private readonly IMessageInfoLogic _message;
        public MainController(IOrderLogic order, IPastryLogic pastry, IMessageInfoLogic message)
        {
            _order = order;
            _pastry = pastry;
            _message = message;
        }
        [HttpGet]
        public List<PastryViewModel> GetPastryList() => _pastry.Read(null)?.ToList();
        [HttpGet]
        public PastryViewModel GetPastry(int pastryId) => _pastry.Read(new PastryBindingModel
        { Id = pastryId })?[0];
        [HttpGet]
        public List<OrderViewModel> GetOrders(int clientId) => _order.Read(new OrderBindingModel
        { ClientId = clientId });
        [HttpGet]
        public List<MessageInfoViewModel> GetMessages(int clientId) => _message.Read(new MessageInfoBindingModel { ClientId = clientId });
        [HttpPost]
        public void CreateOrder(CreateOrderBindingModel model) =>
       _order.CreateOrder(model);
    }
}
