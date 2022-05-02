using System.ComponentModel;

namespace ConfectioneryContracts.ViewModels
{
    public class ImplementerViewModel
    {
        public int Id { get; set; }
        [DisplayName("ФИО исполнителя")]
        public string ImplementerFIO { get; set; }
        [DisplayName("Время работы исполнителя")]
        public int WorkingTime { get; set; }
        [DisplayName("Время отдыха исполнителя")]
        public int PauseTime { get; set; }
    }
}
