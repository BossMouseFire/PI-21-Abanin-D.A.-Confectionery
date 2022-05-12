using System;
using System.ComponentModel;
using ConfectioneryContracts.Attributes;

namespace ConfectioneryContracts.ViewModels
{
    public class MessageInfoViewModel
    {
        [Column(title: "Номер", width: 100)]
        public string MessageId { get; set; }

        [Column(title: "Отправитель", gridViewAutoSize: GridViewAutoSize.Fill)]
        [DisplayName("Отправитель")]
        public string SenderName { get; set; }

        [Column(title: "Дата письма", width: 100)]
        [DisplayName("Дата письма")]
        public DateTime DateDelivery { get; set; }

        [Column(title: "Заголовок", width: 100)]
        [DisplayName("Заголовок")]
        public string Subject { get; set; }

        [Column(title: "Текст", gridViewAutoSize: GridViewAutoSize.Fill)]
        [DisplayName("Текст")]
        public string Body { get; set; }
    }
}
