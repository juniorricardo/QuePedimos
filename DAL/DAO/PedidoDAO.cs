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
                // obtener el ultimo registro de EquipoX y verificar la fecha
                var listaPedidos = contexto.Pedido.ToList();
                for (int indice = 0; indice < listaPedidos.Count; indice++)
                {
                    var equipoActual = (Equipo)contexto.Equipo.Include("Integrantes")
                                                              .FirstOrDefault(x => (x.Id == listaPedidos[indice].EquipoId));

                    var usuarioSorteado = equipoActual.Integrantes.ToList()
                                                      .Where(x => x.EstaDisponible == true)
                                                      .ElementAt(rand.Next(equipoActual.Integrantes.Count));
                    usuarioSorteado.EstaDisponible = false;
                    if (NeedUpdate(listaPedidos[indice].DiaPedido))
                    {
                        var nuevoPedido = new Pedido()
                        {
                            Usuario = usuarioSorteado,
                            Comida = contexto.Comida.ToList().ElementAt(rand.Next(contexto.Comida.Count())),
                            DiaPedido = UpdateDate(listaPedidos[indice].DiaPedido),
                            Equipo = equipoActual
                        };
                    }
                }
                contexto.SaveChanges();
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
        // Verifica si el pedido tiene la fecha actualizada
        // no debe de ser el mismo dia de hoy
        private DateTime UpdateDate(DateTime lastDayRecord)
        {
            var toDay = DateTime.Today;
            return (lastDayRecord.Month == toDay.Month &&
                    lastDayRecord.Day == toDay.Day) ? NextDay() : lastDayRecord;
        }
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
                    day.Day == toDay.Day);
        }
        #endregion

    }
}
