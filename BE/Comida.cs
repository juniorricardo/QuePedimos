using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BE
{
    public class Comida
    {
        public int Id { get; set; }
        [Required]
        [StringLength(25)]
        public string nombre { get; set; }
    }
}
