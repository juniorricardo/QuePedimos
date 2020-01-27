using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class UsuarioEquipo
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public int EquipoId { get; set; }
    }
}
