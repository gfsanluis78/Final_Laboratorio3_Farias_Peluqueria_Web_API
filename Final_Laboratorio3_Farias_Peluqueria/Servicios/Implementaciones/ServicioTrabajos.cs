using Final_Laboratorio3_Farias_Peluqueria.Models;
using Final_Laboratorio3_Farias_Peluqueria.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Final_Laboratorio3_Farias_Peluqueria.Servicios.Implementaciones
{
    public class ServicioTrabajos : ServicioBase<Trabajo>, IServicioTrabajos
    {
        private IRepositorioTrabajos repositorioTrabajos;

        public ServicioTrabajos(IRepositorioTrabajos repositorioTrabajos) : base(repositorioTrabajos)
        {
            this.repositorioTrabajos = repositorioTrabajos;
        }

        public async Task<List<Trabajo>> GetAllByTipoTrabajoByEmpleado(ConsultaByTipoTrabajo entidad)
        {
            return await repositorioTrabajos.GetAllByTipoTrabajoByEmpleado(entidad);
        }
    }
}
