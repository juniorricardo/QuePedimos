using BE;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DAO
{
    public class ComidaDAO
    {
        public async Task<List<Comida>> ListarComida()
        {
            using (var contexto = new QuePedimosContext())
            {
                return await contexto.Comida.ToListAsync();
            }
        }

        public async Task<Comida> BuscarComidaId(int? enComidaId)
        {
            using (var contexto = new QuePedimosContext())
            {
                return await contexto.Comida.FindAsync(enComidaId);
            };
        }

        public void AgregarComida(Comida enComida)
        {
            using (var contexto = new QuePedimosContext())
            {
                contexto.Comida.Add(enComida);
                contexto.SaveChanges();
            }
        }

        public void ActualizarComida(Comida enComida)
        {
            using (var contexto = new QuePedimosContext())
            {
                contexto.Entry(enComida).State = EntityState.Modified;
                contexto.SaveChanges();
            }
        }
        public void EliminarComida(Comida enComida)
        {
            using (var contexto = new QuePedimosContext())
            {
                contexto.Entry(enComida).State = EntityState.Deleted;
                contexto.Comida.Remove(enComida);
                contexto.SaveChanges();
            };
        }
    }
}
