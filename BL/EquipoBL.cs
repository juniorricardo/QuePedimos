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
    public class EquipoBL
    {
        private EquipoDAO equipoDao = new EquipoDAO();
        public List<Equipo> ListaEquipos()
        {
            return equipoDao.ListarEquipos();
        }
        public List<Usuario> ListarIntegranteEquipo(int enEquipoId)
        {
            return equipoDao.ListarIntegrantesEquipo(enEquipoId);
        }

        public Equipo BuscarEquipoPorId(int? enEquipoId)
        {
            return equipoDao.BuscarEquipoPorId(enEquipoId);
        }

        public void AgregarEquipo(int[] enListaIntegrantesIds)
        {
            equipoDao.AgregarEquipo(enListaIntegrantesIds);
        }

        public void ActualizarEquipo(Equipo enEquipo)
        {
            equipoDao.ActualizarEquipo(enEquipo);
        }

        public void EliminarEquipo(int enEquipoId)
        {
            equipoDao.EliminarEquipo(enEquipoId);
        }

    }
}
