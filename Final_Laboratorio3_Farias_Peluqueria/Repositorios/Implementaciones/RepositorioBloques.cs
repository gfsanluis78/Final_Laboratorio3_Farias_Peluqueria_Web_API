using Final_Laboratorio3_Farias_Peluqueria.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Final_Laboratorio3_Farias_Peluqueria.Repositorios.Implementaciones
{
    public class RepositorioBloques : RepositorioBase<Bloque>, IRepositorioBloques 
    {
        private readonly DataContext contexto;

        public RepositorioBloques(DataContext contexto) : base(contexto)
        {
            this.contexto = contexto;
        }

        public async Task<List<Bloque>> GetAllByHorarioByEmpleado(ConsultaHorarios entidad)
        {
            var lista = await contexto.Turnos
                .Include(x => x.Bloque)
                .Include(x => x.Trabajo)
                .ThenInclude(x => x.Empleado)
                .Where(x => x.Trabajo.Empleado.IdEmpleado == entidad.IdEmpleado && x.Fecha == entidad.Fecha)
                .Select(x => x.Bloque)
                .ToListAsync();

            return lista;
        }


    }
}
