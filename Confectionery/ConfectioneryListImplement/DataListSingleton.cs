using ConfectioneryListImplement.Models;
using System.Collections.Generic;

namespace ConfectioneryListImplement
{
    public class DataListSingleton
    {
        private static DataListSingleton instance;
        public List<Component> Components { get; set; }
        public List<Order> Orders { get; set; }
        public List<Pastry> Products { get; set; }
        private DataListSingleton()
        {
            Components = new List<Component>();
            Orders = new List<Order>();
            Products = new List<Pastry>();
        }
        public static DataListSingleton GetInstance()
        {
            if (instance == null)
        {
                instance = new DataListSingleton();
            }
            return instance;
        }

    }
}
