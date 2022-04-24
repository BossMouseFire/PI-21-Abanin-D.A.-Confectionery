using System;

namespace ConfectioneryContracts.BindingModels
{
    public class MessageInfoBindingModel
    {
        public int? ClientId { get; set; }
        public string MessageId { get; set; }
        public string FromMailAddress { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public DateTime DateDelivery { get; set; }
    }

}
