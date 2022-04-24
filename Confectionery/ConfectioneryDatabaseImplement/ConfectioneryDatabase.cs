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
                optionsBuilder.UseSqlServer(@"Server=localhost;Database=confectioneryDB33;Trusted_Connection=True;");
            }
            base.OnConfiguring(optionsBuilder);
        }
        public virtual DbSet<Component> Components { set; get; }
        public virtual DbSet<Pastry> Pastries { set; get; }
        public virtual DbSet<PastryComponent> PastryComponents { set; get; }
        public virtual DbSet<Order> Orders { set; get; }
        public virtual DbSet<Client> Clients { set; get; }
        public virtual DbSet<Implementer> Implementers { get; set; }

        public virtual DbSet<MessageInfo> MessageInfoes { get; set; }
    }
}
