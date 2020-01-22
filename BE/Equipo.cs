using System;
using System.Collections.Generic;
using System.Text;

namespace BE
{
    public class Equipo
    {
        public int Id { get; set; }
        public List<Usuario> Integrantes { get; set; }
        public DateTime FechaCreado { get; set; }
        public DateTime FechaUltimaModificacion { get; set; }
    }
}
