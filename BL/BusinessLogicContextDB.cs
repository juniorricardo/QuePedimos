using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using DAL;

namespace BL
{
    public class BusinessLogicContextDB
    {

        public List<Comida> ListaComidas()
        {
            using(var contexto = new QuePedimosContext())
            {
                return contexto.Comida.ToList();
            };
        }

        public Comida BuscarComida(int? enComidaId)
        {
            using (var contexto = new QuePedimosContext())
            {
                return contexto.Comida.Find(enComidaId);
            };
        }

        public void AgregarComida(Comida enComida)
        {
            using (var contexto = new QuePedimosContext()) 
            {
                contexto.Comida.Add(enComida);
                contexto.SaveChanges();
            };
        }

        public void ActualizarComida(Comida comida)
        {
            using (var contexto = new QuePedimosContext())
            {
                contexto.Entry(comida).State = EntityState.Modified;
                contexto.SaveChanges();
            };
        }

        public void EliminarComida(Comida comida)
        {
            using (var contexto = new QuePedimosContext()) {
                contexto.Comida.Remove(comida);
                contexto.SaveChanges();
            };
        }
    }
}
