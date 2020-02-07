using BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DAO
{
    public class PedidoDAO
    {
        public void VerificarPedidos()
        {
            using (var contexto = new QuePedimosContext())
            {
            }
        }

        public List<Pedido> ListarPedidos()
        {
            using (var contexto = new QuePedimosContext())
            {
                return contexto.Pedido.ToList();
            }
        }
    }
}
