using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BE
{
    [Table("Equipo")]
    public class Equipo
    {
        public int Id { get; set; }
        public Usuario Lider { get; set; }
        public DateTime FechaCreado { get; set; }
        public DateTime FechaUltimaModificacion { get; set; }

        //public string[] IntegrantesIds { get; set; }

        public ICollection<Usuario> Integrantes { get; set; }
        public ICollection<Comida> Comidas{ get; set; }
        public Equipo()
        {
            this.Integrantes = new HashSet<Usuario>();
            this.Comidas = new HashSet<Comida>();
        }

    }
}
