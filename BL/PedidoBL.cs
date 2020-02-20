using DAL.DAO;
using BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class PedidoBL
    {
        private PedidoDAO pedidoDao = new PedidoDAO();

        public List<PedidoResumen> ListarPedidos()
        {
            return pedidoDao.ListarPedidos();
        }

        public void VerificarPedidos()
        {
            pedidoDao.VerificarPedidos();
        }
    }
}
