
namespace ConfectioneryContracts.BindingModels
{
    public class CreateOrderBindingModel
    {
        public int PastryId { get; set; }
        public int Count { get; set; }
        public decimal Sum { get; set; }
    }
}
