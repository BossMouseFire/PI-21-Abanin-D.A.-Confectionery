using System.Runtime.Serialization;

namespace ConfectioneryContracts.BindingModels
{
    public class CreateOrderBindingModel
    {
        [DataMember]
        public int ClientId { get; set; }
        [DataMember]
        public int PastryId { get; set; }
        [DataMember]
        public int Count { get; set; }
        [DataMember]
        public decimal Sum { get; set; }
    }
}
