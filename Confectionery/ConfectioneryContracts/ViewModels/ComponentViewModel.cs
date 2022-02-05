using System.ComponentModel;


namespace ConfectioneryContracts.ViewModels
{
    public class ComponentViewModel
    {
        public int Id { get; set; }
        [DisplayName("Название компонента")]
        public string ComponentName { get; set; }
    }
}
