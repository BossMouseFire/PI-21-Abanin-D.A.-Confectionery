using System;
using ConfectioneryContracts.Enums;
using System.ComponentModel.DataAnnotations;

namespace ConfectioneryDatabaseImplement.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int PastryId { get; set; }
        public Pastry Pastry { get; set; }
        [Required]
        public int Count { get; set; }
        [Required]
        public decimal Sum { get; set; }
        [Required]
        public OrderStatus Status { get; set; }
        [Required]
        public DateTime DateCreate { get; set; }
        public DateTime DateImplement { get; set; }
    }
}
