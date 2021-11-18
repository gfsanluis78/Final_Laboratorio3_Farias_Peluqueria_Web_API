using Final_Laboratorio3_Farias_Peluqueria.Models;
using Final_Laboratorio3_Farias_Peluqueria.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Final_Laboratorio3_Farias_Peluqueria.Servicios.Implementaciones
{
    public class ServicioTurnos : ServicioBase<Turno>, IServicioTurnos
    {
        private IRepositorioTurnos repositorioTurnos;

        public ServicioTurnos(IRepositorioTurnos repositorioTurnos) : base(repositorioTurnos)
        {
            this.repositorioTurnos = repositorioTurnos;
        }

        public async Task<List<Turno>> GetAllFull()
        {
            return await repositorioTurnos.GetAllFull();
        }

        public async Task<List<Turno>> GetAllFullByFecha(ConsultaHorarios entidad)
        {
            return await repositorioTurnos.GetAllFullByFecha(entidad);
        }

        public async Task<int> GetCantidadByEmpleado(Empleado empleado)
        {
            return await repositorioTurnos.GetCantidadByEmpleado(empleado);
        }

        public async Task<List<Turno>> GetTurnosByCliente(Cliente cliente)
        {
            return await repositorioTurnos.GetTurnosByCliente(cliente);
        }

        public async Task<Turno> GetUltimoByCliente(Cliente cliente)
        {
            return await repositorioTurnos.GetUltimoByCliente(cliente);
        }
    }
}
