using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BE
{
    [Table("Comida")]
    public class Comida
    {
        public int Id { get; set; }
        [Required]
        [StringLength(25)]
        public string Nombre { get; set; }
    }
}
