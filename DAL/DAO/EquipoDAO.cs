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
                var rand = new Random();
                contexto.Pedido.Add(new Pedido()
                {
                    DiaPedido = DateTime.Today.AddDays(1),
                    Equipo = equipo,
                    Usuario = equipo.Integrantes.ToList().ElementAt(rand.Next(equipo.Integrantes.Count)),
                    Comida = contexto.Comida.ToList().ElementAt(rand.Next(contexto.Comida.Count())),
                });

                contexto.SaveChanges();
            };
        }

        public void ActualizarEquipo(int enEquipoId, int[] enIntegrantesIds)
        {
            using (var contexto = new QuePedimosContext())
            {
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
    }
}
