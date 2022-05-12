using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using ConfectioneryContracts.Attributes;

namespace ConfectioneryContracts.ViewModels
{
    [DataContract]
    public class PastryViewModel
    {
        [Column(title: "Номер", width: 100)]
        [DataMember]
        public int Id { get; set; }

        [Column(title: "Изделие", gridViewAutoSize: GridViewAutoSize.Fill)]
        [DataMember]
        [DisplayName("Название изделия")]
        public string PastryName { get; set; }

        [Column(title: "Цена", width: 100)]
        [DataMember]
        [DisplayName("Цена")]
        public decimal Price { get; set; }

        [DataMember]
        public Dictionary<int, (string, int)> PastryComponents { get; set; }
    }
}
