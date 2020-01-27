using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using DAL;
using DAL.DAO;

namespace BL
{
    public class ComidaBL
    {
        private ComidaDAO comidaDao = new ComidaDAO();
        public List<Comida> ListaComidas()
        {
            return comidaDao.ListarComida();
        }

        public Comida BuscarComidaPorId(int? enComidaId)
        {
            return comidaDao.BuscarComidaId(enComidaId);
        }

        public void AgregarComida(Comida enComida)
        {
            comidaDao.AgregarComida(enComida);
        }

        public void ActualizarComida(Comida enComida)
        {
            comidaDao.ActualizarComida(enComida);
        }

        public void EliminarComida(Comida enComida)
        {
            comidaDao.EliminarComida(enComida);
        }
    }
}
