using BE;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DAL.DAO
{
    public class PedidoDAO
    {
        public void VerificarPedidos()
        {
            using (var contexto = new QuePedimosContext())
            {
                var rand = new Random();
                var listaPedidos = contexto.Pedido.ToList();
                foreach (var pedido in listaPedidos)
                {
                    if (NeedUpdate(pedido.DiaPedido))
                    {
                        contexto.Pedido.Add(new Pedido()
                        {
                            UsuarioId = SeleccionarUsuarioAleatorio(pedido.EquipoId),
                            EquipoId = pedido.EquipoId,
                            ComidaId = SeleccionarComidaAleatorio(pedido.EquipoId),
                            DiaPedido = NextDay()
                        });
                    }
                }
                contexto.SaveChanges();
            }
        }

        private int SeleccionarComidaAleatorio(int equipoId)
        {
            using (var contexto = new QuePedimosContext())
            {
                var rand = new Random();
                var equipo= contexto.Equipo.Include("Comidas")
                                            .FirstOrDefault(x => x.Id == equipoId);
                var comida = equipo.Comidas.ToList()
                                           .ElementAt(rand.Next(equipo.Comidas.Count));
                return comida.Id;
            }
        }

        /*  SeleccionarUsuarioAleatorio, retorna un usuario de manera aleatoria
            del equipo ('equipoId' corresponde al Id del dicho equipo), donde 
            el integrante se encuentra disponible       */
        private int SeleccionarUsuarioAleatorio(int equipoId)
        {
            using (var contexto = new QuePedimosContext())
            {
                var rand = new Random();
                var equipo = contexto.Equipo.Include("Integrantes")
                               .FirstOrDefault(x => x.Id == equipoId);
                var usuario = equipo.Integrantes.ToList()
                                                .Where(x => x.EstaDisponible == true)
                                                .ElementAt(rand.Next(equipo.Integrantes.Count));
                usuario.EstaDisponible = false;

                contexto.Usuario.Attach(usuario);
                contexto.Entry(usuario).Property(x => x.EstaDisponible).IsModified = true;
                contexto.SaveChanges();

                return usuario.Id;
            }
        }

        public List<PedidoResumen> ListarPedidos()
        {
            using (var contexto = new QuePedimosContext())
            {

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

        #region Util
      
        // Retorna el proximo dia habil
        private DateTime NextDay()
        {
            int days = 0;
            DateTime tomorrow = DateTime.Today.AddDays(1);
            while ((int)tomorrow.AddDays(days).DayOfWeek == 6 ||
                        tomorrow.AddDays(days).DayOfWeek == 0)
            {
                days += 1;
            }
            return tomorrow.AddDays(days);
        }
        private bool NeedUpdate(DateTime day)
        {
            var toDay = DateTime.Today;
            return (day.Month == toDay.Month &&
                    day.Day <= toDay.Day);
        }
        #endregion

    }
}
