using BE;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DAO
{
    public class EquipoDAO
    {
        public List<Equipo> ListarEquipos()
        {
            using (var contexto = new QuePedimosContext())
            {
                return contexto.Equipo.ToList();
            };
        }

        public Equipo BuscarEquipoPorId(int? enEquipoId)
        {
            using (var contexto = new QuePedimosContext())
            {
                return contexto.Equipo.Find(enEquipoId);
            };
        }

        public void AgregarEquipo(int[] enListaIntegrantesIds)
        {
            using (var contexto = new QuePedimosContext())
            {
                var nuevosIntegrantes = contexto.Usuario
                                                .Where(r => enListaIntegrantesIds.Contains(r.Id))
                                                .ToList();
                contexto.Equipo.Add(new Equipo()
                {
                    Integrantes = nuevosIntegrantes,
                    FechaCreado = DateTime.Now,
                    FechaUltimaModificacion = DateTime.Now,
                });
                contexto.SaveChanges();
            };

        }


        public void ActualizarEquipo(Equipo enEquipo)
        {
            using (var contexto = new QuePedimosContext())
            {
                contexto.Entry(enEquipo).State = EntityState.Modified;
                enEquipo.FechaUltimaModificacion = DateTime.Now;
                contexto.SaveChanges();
            };
        }
        public void EliminarEquipo(int enEquipoId)
        {
            using (var contexto = new QuePedimosContext())
            {
                Equipo equipo = contexto.Equipo.Find(enEquipoId);
                contexto.Entry(equipo).State = EntityState.Deleted;
                contexto.Equipo.Remove(equipo);
                contexto.SaveChanges();
            };
        }
    }
}
