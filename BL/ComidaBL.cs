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
        public async Task<List<Comida>> ListaComidas()
        {
            return await comidaDao.ListarComida();
        }

        public async Task<Comida> BuscarComidaPorId(int? enComidaId)
        {
            return await comidaDao.BuscarComidaId(enComidaId);
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
