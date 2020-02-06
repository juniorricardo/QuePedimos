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
                var lista = contexto.Equipo.Include("Integrantes")
                                           .ToList();
                return lista;
            };
        }
        //public List<Usuario> ListarIntegrantesEquipo(int enEquipoId)
        //{
        //    using (var contexto = new QuePedimosContext())
        //    {
        //        return contexto.Equipo.Find(enEquipoId).Integrantes.ToList();
        //    };
        //}

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
                return contexto.Equipo.Include("Integrantes")
                                      .FirstOrDefault(x => x.Id == enEquipoId);
            };
        }

        public void AgregarEquipo(int[] enListaIntegrantesId)
        {
            using (var contexto = new QuePedimosContext())
            {
                var equipo = new Equipo()
                {
                    Integrantes = contexto.Usuario.Where(r => enListaIntegrantesId.Contains(r.Id))
                                                             .ToList(),
                    FechaCreado = DateTime.Now,
                    FechaUltimaModificacion = DateTime.Now
                };

                contexto.Equipo.Add(equipo);

                contexto.SaveChanges();
            };
        }


        public void ActualizarEquipo(int enEquipoId, int[] enIntegrantesIds)
        {
            using (var contexto = new QuePedimosContext())
            {
                //  Traer el objeto y actualizar los integrantes, antes limpiar el contenido de ellos
                var equipo = contexto.Equipo.Include("Integrantes").FirstOrDefault(x => x.Id == enEquipoId);
                var nuevosIntegrantes = contexto.Usuario.Where(r => enIntegrantesIds.Contains(r.Id))
                                                             .ToList();
                equipo.Integrantes.Clear();
                nuevosIntegrantes.ForEach(x => equipo.Integrantes.Add(x));
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
        //public List<Usuario> ListarUsuarios(int[] enListaIntegrantesId)
        //{
        //    using (var contexto = new QuePedimosContext())
        //    {
        //        var lista = contexto.Usuario.Where(r => enListaIntegrantesId.Contains(r.Id))
        //                                .ToList();
        //        return lista;
        //    }
        //}
    }
}
