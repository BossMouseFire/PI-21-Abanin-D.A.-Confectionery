using System.ComponentModel;
using ConfectioneryContracts.Attributes;

namespace ConfectioneryContracts.ViewModels
{
    public class ComponentViewModel
    {
        [Column(title: "Номер", width: 100)]
        public int Id { get; set; }

        [Column(title: "Компонент", gridViewAutoSize: GridViewAutoSize.Fill)]
        [DisplayName("Название компонента")]
        public string ComponentName { get; set; }
    }
}
