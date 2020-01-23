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

        public void AgregarEquipo(Equipo enEquipo)
        {
            using (var contexto = new QuePedimosContext())
            {
                contexto.Equipo.Add(enEquipo);
                contexto.SaveChanges();
            };
        }

        public void ActualizarUsuario(Equipo enEquipo)
        {
            using (var contexto = new QuePedimosContext())
            {
                contexto.Entry(enEquipo).State = EntityState.Modified;
                contexto.SaveChanges();
            };
        }

        public void EliminarEquipo(Equipo enEquipo)
        {
            using (var contexto = new QuePedimosContext())
            {
                contexto.Equipo.Remove(enEquipo);
                contexto.SaveChanges();
            };
        }

        
    }
}
