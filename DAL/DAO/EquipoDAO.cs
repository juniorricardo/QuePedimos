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
                var lista = contexto.Equipo.ToList();
                return lista;
            };
        }
        public List<Usuario> ListarIntegrantesEquipo(int enEquipoId)
        {
            using (var contexto = new QuePedimosContext())
            {
                return contexto.Equipo.Find(enEquipoId).Integrantes.ToList();
            };
        }

        //public List<Usuario> ListarIntegrantesEquipo(int enEquipoId)
        //{
        //    using (var contexto = new QuePedimosContext())
        //    {
        //        /*  
        //         *  return (from empleado in entities.empleado
        //                    join his_estructura in entities.his_estructura on empleado.ternro equals his_estructura.ternro
        //                    join estructura in entities.estructura on his_estructura.estrnro equals estructura.estrnro
        //                    where empleado.empest == -1 && estructura.tenro == 101 && contiene.Contains(empleado.empreporta) && his_estructura.htethasta == null && estructura.estrnro != 2268
        //                    select new Cliente { codigo = estructura.estrnro.ToString(), descripcion = estructura.estrdabr }).Distinct().ToList();

        //         */
        //        return (from usuario in contexto.Usuario
        //                join instancia in contexto.UsuarioEquipo on usuario.Id equals instancia.UsuarioId
        //                where instancia.EquipoId == enEquipoId
        //                select usuario).ToList();
        //    };
        //}

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
