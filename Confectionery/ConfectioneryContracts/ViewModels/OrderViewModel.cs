using System;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace ConfectioneryContracts.ViewModels
{
    [DataContract]
    public class OrderViewModel
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public int PastryId { get; set; }

        [DataMember]
        [DisplayName("Изделие")]
        public string PastryName { get; set; }

        [DataMember]
        public int? ImplementerId { get; set; }

        [DataMember]
        [DisplayName("ФИО исполнителя")]
        public string ImplementerFIO { get; set; }

        [DataMember]
        public int ClientId { get; set; }

        [DataMember]
        [DisplayName("ФИО клиента")]
        public string ClientFIO { get; set; }

        [DataMember]
        [DisplayName("Количество")]
        public int Count { get; set; }

        [DataMember]
        [DisplayName("Сумма")]
        public decimal Sum { get; set; }

        [DataMember]
        [DisplayName("Статус")]
        public string Status { get; set; }

        [DataMember]
        [DisplayName("Дата создания")]
        public DateTime DateCreate { get; set; }

        [DataMember]
        [DisplayName("Дата выполнения")]
        public DateTime? DateImplement { get; set; }
    }
}