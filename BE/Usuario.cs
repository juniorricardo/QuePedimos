using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BE
{
    [Table("Usuario")]
    public class Usuario
    {
        public int Id { get; set; }
        [Required]
        [StringLength(15)]
        public string Nombre { get; set; }
        [StringLength(20)]
        public string Apellido { get; set; }
        [StringLength(25)]
        public string Correo { get; set; }
        [Required]
        public int Dni { get; set; }
        [Required]
        public Boolean EstaDisponible { get; set; }
        [Required]
        public RolUsuarioEmun Rol { get; }

        public string NombreApellido { get { return string.Format("{0} {1}", Nombre, Apellido);  } }

        public virtual ICollection<Equipo> Equipos { get; set; }
        public Usuario()
        {
            this.Equipos = new HashSet<Equipo>();
        }

    }
}
