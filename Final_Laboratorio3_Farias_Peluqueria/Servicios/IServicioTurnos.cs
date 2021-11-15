using Final_Laboratorio3_Farias_Peluqueria.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Final_Laboratorio3_Farias_Peluqueria.Servicios
{
    public interface IServicioTurnos : IServicioBase<Turno>
    {
        Task<List<Turno>> GetAllFull();

        Task<List<Turno>> GetAllFullByFecha(ConsultaHorarios entidad);
    }
}
