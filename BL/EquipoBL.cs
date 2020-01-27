using BE;
using DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class EquipoBL
    {
        public List<Equipo> ListaEquipos()
        {
            using (var contexto = new QuePedimosContext())
            {
                var nueva = from contexto.
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

        public void AgregarEquipo(int[] listaIntegrantesIds)
        {
            using (var contexto = new QuePedimosContext())
            {
                //List<Usuario> nuevosIntegrantes = (List<Usuario>)(contexto.Usuario.Select(x => listaIntegrantesIds.Where( y => y == x.Id) ));
                var nuevosIntegrantes = contexto.Usuario.Where(r => listaIntegrantesIds.Contains(r.Id)).ToList();
                var nuevoEquipo = new Equipo()
                {
                    FechaCreado = DateTime.Now,
                    FechaUltimaModificacion = DateTime.Now,
                    Integrantes = nuevosIntegrantes,
                    IntegrantesIds = listaIntegrantesIds.Select(s => s.ToString()).ToArray()
                };
                contexto.Equipo.Add(nuevoEquipo);
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

        public void EliminarEquipo(Equipo enEquipo)
        {
            using (var contexto = new QuePedimosContext())
            {
                contexto.Entry(enEquipo).State = EntityState.Deleted;
                contexto.Equipo.Remove(enEquipo);
                contexto.SaveChanges();
            };
        }


    }
}
