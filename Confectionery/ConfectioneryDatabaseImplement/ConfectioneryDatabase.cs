using ConfectioneryDatabaseImplement.Models;
using Microsoft.EntityFrameworkCore;

namespace ConfectioneryDatabaseImplement
{
    public class ConfectioneryDatabase : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured == false)
            {
                optionsBuilder.UseSqlServer(@"Data Source=HOME\SQLEXPRESS;Initial
Catalog=ConfectioneryDatabase;Integrated Security=True;MultipleActiveResultSets=True;");
            }
            base.OnConfiguring(optionsBuilder);
        }
        public virtual DbSet<Component> Components { set; get; }
        public virtual DbSet<Pastry> Pastries { set; get; }
        public virtual DbSet<PastryComponent> PastryComponents { set; get; }
        public virtual DbSet<Order> Orders { set; get; }
    }
}
