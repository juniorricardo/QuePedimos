using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BE
{
    [Table("Pedido")]
    public class Pedido
    {
        #region Constructores
        public Pedido(Equipo enEquipo,
                      Usuario enUsuario, 
                      Comida enComida)
        {
            EquipoId = enEquipo.Id;
            UsuarioId = enUsuario.Id;
            ComidaId = enComida.Id;
        }
        public Pedido() { } 
        #endregion

        public int Id { get; set; }

        #region Equipo-ID
        [Required]
        public int EquipoId { get; set; }
        [ForeignKey("EquipoId")]
        public Equipo Equipo { get; set; }
        #endregion

        #region Usuario-ID
        [Required]
        public int UsuarioId { get; set; }
        [ForeignKey("UsuarioId")]
        public Usuario Usuario { get; set; }
        #endregion

        #region Comida-ID
        [Required]
        public int ComidaId { get; set; }
        [ForeignKey("ComidaId")]
        public Comida Comida { get; set; } 
        #endregion

        public DateTime DiaPedido { get; set; }
    }
}
