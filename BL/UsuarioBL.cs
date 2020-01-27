using BE;
using DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class UsuarioBL
    {
        public List<Usuario> ListaUsuarios()
        {
            using (var contexto = new QuePedimosContext())
            {
                //var integrantes = 
                return contexto.Usuario.ToList();
            };
        }

        public Usuario BuscarUsuarioPorId(int? enUsuarioId)
        {
            using (var contexto = new QuePedimosContext())
            {
                return contexto.Usuario.Find(enUsuarioId);
            };
        }

        public void AgregarUsuario(Usuario enUsuario)
        {
            using (var contexto = new QuePedimosContext())
            {
                contexto.Usuario.Add(enUsuario);
                contexto.SaveChanges();
            };
        }

        public void ActualizarUsuario(Usuario enUsuario)
        {
            using (var contexto = new QuePedimosContext())
            {
                contexto.Entry(enUsuario).State = EntityState.Modified;
                contexto.SaveChanges();
            };
        }

        public void EliminarUsuario(Usuario enUsuario)
        {
            using (var contexto = new QuePedimosContext())
            {
                contexto.Entry(enUsuario).State = EntityState.Deleted;
                contexto.Usuario.Remove(enUsuario);
                contexto.SaveChanges();
            };
        }
    }
}
