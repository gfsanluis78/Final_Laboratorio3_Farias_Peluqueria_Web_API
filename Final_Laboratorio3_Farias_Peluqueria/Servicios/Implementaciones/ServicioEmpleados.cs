using Final_Laboratorio3_Farias_Peluqueria.Models;
using Final_Laboratorio3_Farias_Peluqueria.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Final_Laboratorio3_Farias_Peluqueria.Servicios.Implementaciones
{
    public class ServicioEmpleados : ServicioBase<Empleado>, IServicioEmpleados
    {
        private IRepositorioEmpleados repositorioEmpleado;
        public ServicioEmpleados(IRepositorioEmpleados repositorioEmpleado) : base(repositorioEmpleado)
        {
            this.repositorioEmpleado = repositorioEmpleado;
        }

        public async Task<List<Empleado>> GetEmpleadosByTipoTrabajo(int id)
        {
            return await repositorioEmpleado.GetEmpleadosByTipoTrabajo(id);
        }
    }
}
