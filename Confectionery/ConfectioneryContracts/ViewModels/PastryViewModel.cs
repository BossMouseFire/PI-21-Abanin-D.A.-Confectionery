using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace ConfectioneryContracts.ViewModels
{
    [DataContract]
    public class PastryViewModel
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        [DisplayName("Название изделия")]
        public string PastryName { get; set; }

        [DataMember]
        [DisplayName("Цена")]
        public decimal Price { get; set; }

        [DataMember]
        public Dictionary<int, (string, int)> PastryComponents { get; set; }
    }
}
