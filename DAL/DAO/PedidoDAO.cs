using BE;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DAL.DAO
{
    public class PedidoDAO
    {
        /**
         *  Verifica en que 'DiaPedido' este actulizado, de no ser asi
         *  este llamara a metodos que permitar realizar dicha actualizacion
         */
        public void VerificarPedidos()
        {
            using (var contexto = new QuePedimosContext())
            {
                /*
                 * F:
                 *  Verificar si existe un registro de cada equipo
                 */
                var rand = new Random();
                //var listaPedidos = contexto.Pedido.ToList().GroupBy(x => x.EquipoId).LastOrDefault();
                // Obtiene el listado del ultimo pedido de cada equipo
                var listaPedidoEquipos = contexto.Pedido.GroupBy(x => x.EquipoId)
                                                  .Select(x => x.OrderByDescending(i => i.Id)
                                                                  .FirstOrDefault())
                                                  .ToList();
                
                foreach (var pedido in listaPedidoEquipos)
                {
                    if (NecesitaActulizarDiaPedido(pedido.DiaPedido))
                    {
                        contexto.Pedido.Add(new Pedido()
                        {
                            UsuarioId = SeleccionarUsuarioAleatorio(pedido.EquipoId),
                            EquipoId = pedido.EquipoId,
                            ComidaId = SeleccionarComidaAleatorio(pedido.EquipoId),
                            DiaPedido = ObtenerProximoDia()
                        });
                    }
                }
                contexto.SaveChanges();
            }
        }
        /**
         * Selecciona el ID de una comida del equipo aleatoria
         */
        private int SeleccionarComidaAleatorio(int equipoId)
        {
            using (var contexto = new QuePedimosContext())
            {
                var rand = new Random();
                var equipo = contexto.Equipo.Include("Comidas")
                                            .FirstOrDefault(x => x.Id == equipoId);
                var comida = equipo.Comidas.ToList()
                                           .ElementAt(rand.Next(equipo.Comidas.Count));
                return comida.Id;
            }
        }

        /**
         * Permite seleccionar el Id de un integrante del equipo de
         * manera aleatoria
         */
        private int SeleccionarUsuarioAleatorio(int equipoId)
        {
            using (var contexto = new QuePedimosContext())
            {
                /*
                 * F:
                 *  Validar cantidad de integrantes disponibles, y reiniciar cuando todos ellos 
                 *  ya no se encuentren disponibles.
                 */
                var rand = new Random();
                var listaUsuariosDisponibles = contexto.Equipo.Include("Integrantes")
                                                              .FirstOrDefault(x => x.Id == equipoId)
                                                              .Integrantes
                                                              .Where(x => x.EstaDisponible == true)
                                                              .ToList();
                //var listaUsuariosDisponibles = equipo.Integrantes.Where(x => x.EstaDisponible == true)
                //                                                 .ToList();

                // Si lista de usuario es 1 seleecionar ese usuario
                //  Si no (si) lista de usuarios es 0, reiniciar estado de disponibilidad de todos ellos
                //   Si no, seleccionar con el random al nuevo usuario
                var usuario = listaUsuariosDisponibles.ElementAt(rand.Next(listaUsuariosDisponibles.Count));
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
                var listaEquipoIds = contexto.Equipo.Select(x => x.Id).ToList();
                //List<PedidoResumen> listaPedido = new List<PedidoResumen>();
                var demo = contexto.Pedido.GroupBy(g => g.EquipoId)
                                          .Select(i => i.OrderByDescending(d => d.Id)
                                                        .FirstOrDefault())
                                          .Select(p => new PedidoResumen()
                                          {
                                              EquipoId = p.EquipoId,
                                              Nombre = p.Usuario.Nombre,
                                              Apellido = p.Usuario.Apellido,
                                              Comida = p.Comida.Nombre,
                                              Dia = p.DiaPedido
                                          })
                                          .ToList();
                #region Comment
                //foreach (var item in listaEquipoIds)
                //{
                //    var pedidoEquipo = contexto.Pedido.Where(x => x.EquipoId == item)
                //                                      .Select(x => new PedidoResumen()
                //                                      {
                //                                          EquipoId = x.EquipoId,
                //                                          Nombre = x.Usuario.Nombre,
                //                                          Apellido = x.Usuario.Apellido,
                //                                          Comida = x.Comida.Nombre,
                //                                          Dia = x.DiaPedido
                //                                      })
                //                                      .ToList()
                //                                      .LastOrDefault();
                //    listaPedido.Add(pedidoEquipo);
                //} 
                #endregion
                return demo;
            }
        }

        #region Util

        // Retorna el proximo dia habil
        private DateTime ObtenerProximoDia()
        {
            int dias = 0;
            DateTime manana = DateTime.Today.AddDays(1);
            while ((int)manana.AddDays(dias).DayOfWeek == 6 ||
                        manana.AddDays(dias).DayOfWeek == 0)
            {
                dias += 1;
            }
            return manana.AddDays(dias);
        }
        private bool NecesitaActulizarDiaPedido(DateTime enDia)
        {
            var hoy = DateTime.Today;
            return (enDia.Month == hoy.Month &&
                    enDia.Day <= hoy.Day);
        }
        #endregion

    }
}
