using BE;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class QuePedimosContext : DbContext
    {
        public QuePedimosContext()
            :base("DAL.QuePedimosDB")
        {
        }
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Equipo> Equipo { get; set; }
        public DbSet<Comida> Comida { get; set; }
        public DbSet<Pedido> Pedido { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder){
            base.OnModelCreating(modelBuilder);
        }

    }
}
