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

        public void AgregarEquipo(int[] enListaIntegrantesId)
        {
            using (var contexto = new QuePedimosContext())
            {
                contexto.Equipo.Add(new Equipo()
                {
                    Integrantes = ListarUsuarios(enListaIntegrantesId),
                    FechaCreado = DateTime.Now,
                    FechaUltimaModificacion = DateTime.Now,
                });
                contexto.SaveChanges();
            };
        }

            
        public void ActualizarEquipo(int enEquipoId, int[] enIntegrantesIds)
        {
            using (var db = new QuePedimosContext())
            {
                var equipo = db.Equipo.Find(enEquipoId);
                var lista = ListarUsuarios(enIntegrantesIds);
                equipo.Integrantes.Clear();
                lista.ForEach(x => equipo.Integrantes.Add(x));

                db.SaveChanges();

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
        public List<Usuario> ListarUsuarios(int[] enListaIntegrantesId)
        {
            using (var contexto = new QuePedimosContext())
            {
                return contexto.Usuario.Where(r => enListaIntegrantesId.Contains(r.Id))
                                        .ToList();
            }
        }
    }
}
