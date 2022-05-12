using ConfectioneryContracts.Attributes;
using System;
using System.Runtime.Serialization;

namespace ConfectioneryContracts.ViewModels
{
    [DataContract]
    public class OrderViewModel
    {
        [Column(title: "Номер", width: 100)]
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public int PastryId { get; set; }

        [Column(title: "Изделие", gridViewAutoSize: GridViewAutoSize.Fill)]
        [DataMember]
        public string PastryName { get; set; }

        [DataMember]
        public int? ImplementerId { get; set; }

        [Column(title: "Исполнитель", width: 150)]
        [DataMember]
        public string ImplementerFIO { get; set; }

        [DataMember]
        public int ClientId { get; set; }

        [Column(title: "Клиент", width: 150)]
        [DataMember]
        public string ClientFIO { get; set; }

        [Column(title: "Количество", width: 100)]
        [DataMember]
        public int Count { get; set; }

        [Column(title: "Сумма", width: 50)]
        [DataMember]
        public decimal Sum { get; set; }

        [Column(title: "Статус", width: 100)]
        [DataMember]
        public string Status { get; set; }

        [Column(title: "Дата создания", width: 100)]
        [DataMember]
        public DateTime DateCreate { get; set; }

        [Column(title: "Дата выполнения", width: 100)]
        [DataMember]
        public DateTime? DateImplement { get; set; }
    }
}