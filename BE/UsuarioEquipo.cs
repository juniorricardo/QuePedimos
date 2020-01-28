using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    //[Table("UsuarioEquipo")]
    public class UsuarioEquipo
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public int EquipoId { get; set; }
    }
}
