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
        public async Task<List<Equipo>> ListaEquipos()
        {
            return await equipoDao.ListarEquipos();
        }

        public async Task<Equipo> BuscarEquipoPorId(int? enEquipoId)
        {
            return await equipoDao.BuscarEquipoPorId(enEquipoId);
        }

        public void AgregarEquipo(int[] enListaIntegrantesIds , int[] enListaComidasIds)
        {
            equipoDao.AgregarEquipo(enListaIntegrantesIds,enListaComidasIds);
        }

        public void ActualizarEquipo(int enEquipoId, int[] enIntegrantesIds)
        {
            equipoDao.ActualizarEquipo(enEquipoId, enIntegrantesIds);
        }

        public void EliminarEquipo(int enEquipoId)
        {
            equipoDao.EliminarEquipo(enEquipoId);
        }

    }
}
