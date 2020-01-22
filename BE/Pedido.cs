using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BE
{
    public class Pedido
    {
        public Pedido(Usuario enPersona, Comida enComida)
        {
            PersonaId = enPersona.Id;
            ComidaId = enComida.Id;
            Persona = enPersona;
            Comida = enComida;
            DiaPedido = new DateTime().Date;
        }
        public Pedido() { }
        public int Id { get; set; }
        public int PersonaId { get; set; }
        [ForeignKey("PersonaId")]
        public Usuario Persona { get; set; }
        public int ComidaId { get; set; }
        [ForeignKey("ComidaId")]
        public Comida Comida { get; set; }
        public DateTime DiaPedido { get; set; }
    }
}
