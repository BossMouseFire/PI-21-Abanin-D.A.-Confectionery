using System.ComponentModel;
using System.Runtime.Serialization;

namespace ConfectioneryContracts.ViewModels
{
    [DataContract]
    public class ClientViewModel
    {
        [DataMember]
        public int Id { get; set; }

        [DisplayName("ФИО")]
        [DataMember]
        public string FIO { get; set; }

        [DisplayName("Почта")]
        [DataMember]
        public string Email { get; set; }

        [DataMember]
        [DisplayName("Пароль")]
        public string Password { get; set; }
    }
}
