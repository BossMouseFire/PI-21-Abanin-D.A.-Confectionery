using System.ComponentModel;
using ConfectioneryContracts.Attributes;

namespace ConfectioneryContracts.ViewModels
{
    public class ImplementerViewModel
    {
        [Column(title: "Номер", width: 100)]
        public int Id { get; set; }

        [Column(title: "Исполнитель", width: 100)]
        [DisplayName("ФИО исполнителя")]
        public string ImplementerFIO { get; set; }

        [Column(title: "Время на заказ", width: 100)]
        [DisplayName("Время работы исполнителя")]
        public int WorkingTime { get; set; }

        [Column(title: "Время на перерыв", width: 100)]
        [DisplayName("Время отдыха исполнителя")]
        public int PauseTime { get; set; }
    }
}
