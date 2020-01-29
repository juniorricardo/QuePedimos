﻿using BE;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DAO
{
    public class UsuarioDAO
    {
        public List<Usuario> ListarUsuarios()
        {
            using (var contexto = new QuePedimosContext())
            {
                return contexto.Usuario.ToList();
            };
        }

        public Usuario BuscarPorId(int? enUsuarioId)
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
        public void EliminarUsuario(int enUsuarioId)
        {
            using (var contexto = new QuePedimosContext())
            {
                Usuario usuario = contexto.Usuario.Find(enUsuarioId);
                contexto.Entry(usuario).State = EntityState.Deleted;
                contexto.Usuario.Remove(usuario);
                contexto.SaveChanges();
            };
        }
    }
}
