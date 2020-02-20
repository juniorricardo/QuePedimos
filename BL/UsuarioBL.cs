using BE;
using DAL;
using DAL.DAO;
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
        private UsuarioDAO usuarioDao = new UsuarioDAO();

        public async Task<List<Usuario>> ListaUsuarios()
        {
            return await usuarioDao.ListarUsuarios();
        }

        public async Task<Usuario> BuscarUsuarioPorId(int? enUsuarioId)
        {
            return await usuarioDao.BuscarPorId(enUsuarioId);
        }

        public void AgregarUsuario(Usuario enUsuario)
        {
            usuarioDao.AgregarUsuario(enUsuario);
        }

        public void ActualizarUsuario(Usuario enUsuario)
        {
            usuarioDao.ActualizarUsuario(enUsuario);
        }

        public void EliminarUsuario(int enUsuarioId)
        {
            usuarioDao.EliminarUsuario(enUsuarioId);
        }
    }
}
