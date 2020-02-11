using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class PedidoResumen
    {
        public int EquipoId { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Comida { get; set; }
        public DateTime Dia { get; set; }
    }
}
