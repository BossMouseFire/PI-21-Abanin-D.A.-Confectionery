using ConfectioneryContracts.Attributes;
using System;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace ConfectioneryContracts.ViewModels
{
    [DataContract]
    public class ClientViewModel
    {
        [Column(title: "Номер", width: 100)]
        [DataMember]
        public int Id { get; set; }

        [Column(title: "Клиент", gridViewAutoSize: GridViewAutoSize.Fill)]
        [DisplayName("ФИО")]
        [DataMember]
        public string FIO { get; set; }

        [Column(title: "Почта", width: 100)]
        [DisplayName("Почта")]
        [DataMember]
        public string Email { get; set; }

        [Column(title: "Пароль", width: 100)]
        [DataMember]
        [DisplayName("Пароль")]
        public string Password { get; set; }
    }
}
