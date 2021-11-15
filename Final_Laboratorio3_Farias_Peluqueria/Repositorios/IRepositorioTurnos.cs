using Final_Laboratorio3_Farias_Peluqueria.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Final_Laboratorio3_Farias_Peluqueria.Repositorios
{
    public interface IRepositorioTurnos : IRepositorioBase<Turno>
    {
        Task<List<Turno>> GetAllFull();

        Task<List<Turno>> GetAllFullByFecha(ConsultaHorarios entidad);
    }
}
