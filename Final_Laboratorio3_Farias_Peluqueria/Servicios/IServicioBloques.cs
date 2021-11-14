using Final_Laboratorio3_Farias_Peluqueria.Models;
using Final_Laboratorio3_Farias_Peluqueria.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Final_Laboratorio3_Farias_Peluqueria.Servicios
{
    
     public interface IServicioBloques : IServicioBase<Bloque>
    {
        Task<List<Bloque>> GetAllByHorarioByEmpleado(ConsultaHorarios entidad);
    }
}
