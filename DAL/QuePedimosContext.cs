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
            : base("name=QuePedimosConnectionString") { }

        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Equipo> Equipo { get; set; }
        public DbSet<Comida> Comida { get; set; }
        public DbSet<Pedido> Pedido { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            base.Configuration.LazyLoadingEnabled = false;
            /* Relacion Many to Many
                Equipo y Usuario    */
            modelBuilder.Entity<Usuario>()
                            .HasMany(u => u.Equipos)
                            .WithMany(e => e.Integrantes)
                            .Map(eu =>
                                {
                                    eu.MapLeftKey("IntegranteId");
                                    eu.MapRightKey("EquipoId");
                                    eu.ToTable("UsuarioEquipo");
                                });
            /* Relacion Many To Many
                 Equipo y Comida */
            modelBuilder.Entity<Comida>()
                            .HasMany(e => e.Equipos)
                            .WithMany(c => c.Comidas)
                            .Map(ce =>
                                {
                                    ce.MapLeftKey("ComidaId");
                                    ce.MapRightKey("EquipoId");
                                    ce.ToTable("ComidaEquipo");
                                });
        }
    }
}
