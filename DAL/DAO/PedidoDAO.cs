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

        public List<PedidoResumen> ListarPedidos()
        {
            using (var contexto = new QuePedimosContext())
            {
                // tipos anonimos
                //  Seleccionar varias columnas y utilizando un tipo 'anonimo'
                // var listaAnonima = db.Persona.Select(x => new { Nombre = x.Nombre, Edad = x.Edad }).ToList();
                //var pedido = contexto.Pedido.Select(x => new Pedido()
                //{
                //    NombreUsuario = x.Usuario.NombreApellido,
                //    Comida = x.Comida.Nombre
                //}).ToList();

                var listaPedido = contexto.Pedido.Select(x => new PedidoResumen()
                {
                    EquipoId = x.EquipoId,
                    Nombre = x.Usuario.Nombre,
                    Apellido = x.Usuario.Apellido,
                    Comida = x.Comida.Nombre,
                    Dia = x.DiaPedido
                }).ToList();

                return listaPedido;
            }
        }
    }
}
