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

        public Equipo BuscarEquipoPorId(int? enEquipoId)
        {
            using (var contexto = new QuePedimosContext())
            {
                return contexto.Equipo.Include("Integrantes")
                                      .FirstOrDefault(x => x.Id == enEquipoId);
            };
        }

        public void AgregarEquipo(int[] enListaIntegrantesId, int[] enListaComidasIds)
        {
            using (var contexto = new QuePedimosContext())
            {
                var rand = new Random();
                var equipo = new Equipo()
                {
                    Integrantes = contexto.Usuario.Where(r => enListaIntegrantesId.Contains(r.Id))
                                                  .ToList(),
                    Comidas = contexto.Comida.Where(s => enListaComidasIds.Contains(s.Id))
                                             .ToList(),
                    FechaCreado = DateTime.Now,
                    FechaUltimaModificacion = DateTime.Now
                };
                contexto.Equipo.Add(equipo);
                var usuarioSorteado = equipo.Integrantes.ToList()
                                                        .Where(x => x.EstaDisponible == true)
                                                        .ElementAt(rand.Next(equipo.Integrantes.Count));
                usuarioSorteado.EstaDisponible = false;
                contexto.Pedido.Add(new Pedido()
                {
                    DiaPedido = DateTime.Today.AddDays(1),
                    Equipo = equipo,
                    Usuario = usuarioSorteado,
                    Comida = contexto.Comida.Where(r => enListaComidasIds.Contains(r.Id))
                                            .ToList()
                                            .ElementAt(rand.Next(equipo.Comidas.Count))
                });
                contexto.SaveChanges();
            };
        }

        public void ActualizarEquipo(int enEquipoId, int[] enIntegrantesIds)
        {
            /*
             * F:
             *  Validar registro, si el usuario sorteado es removido del equipo, se debe realizar un 
             *  nuevo sorteo con los nuevos integrantes, ademas se tiene que devolver el estado del
             *  usuario removido a 'TRUE' disponible
             */
            using (var contexto = new QuePedimosContext())
            {
                var equipo = contexto.Equipo.Include("Integrantes").FirstOrDefault(x => x.Id == enEquipoId);
                var nuevosIntegrantes = contexto.Usuario.Where(r => enIntegrantesIds.Contains(r.Id))
                                                             .ToList();
                equipo.Integrantes.Clear();
                nuevosIntegrantes.ForEach(x => equipo.Integrantes.Add(x));
                equipo.FechaUltimaModificacion = DateTime.Now;
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
