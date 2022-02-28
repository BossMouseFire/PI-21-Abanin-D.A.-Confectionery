using System.ComponentModel.DataAnnotations;


namespace ConfectioneryDatabaseImplement.Models
{
    public class PastryComponent
    {
        public int Id { get; set; }

        public int PastryId { get; set; }

        public int ComponentId { get; set; }

        [Required]
        public int Count { get; set; }

        public virtual Component Component { get; set; }

        public virtual Pastry Pastry { get; set; }
    }
}
