using Final_Laboratorio3_Farias_Peluqueria.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Final_Laboratorio3_Farias_Peluqueria.Servicios
{
    public interface IServicioEmpleados : IServicioBase<Empleado>
    {
        Task<List<Empleado>> GetEmpleadosByTipoTrabajo(int id);
        
        Task<List<Empleado>> GetAllByTipoTrabajo(TipoDeTrabajo tipoDeTrabajo);
    }
}
