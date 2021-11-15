using Final_Laboratorio3_Farias_Peluqueria.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Final_Laboratorio3_Farias_Peluqueria.Repositorios.Implementaciones
{
    public class RepositorioTurnos : RepositorioBase<Turno>, IRepositorioTurnos
    {
        private readonly DataContext contexto;

        public RepositorioTurnos(DataContext context) : base(context)
        {
            this.contexto = context;
        }

        public async Task<List<Turno>> GetAllFull()
        {

            var lista = await contexto.Turnos
              .Include(x => x.Cliente)
              .Include(x => x.Bloque)
              .Include(x => x.Estado)
              .Include(x => x.Trabajo)
              .ThenInclude(x=>x.Empleado)
              .ToListAsync();

            return lista;
        }

        public async Task<List<Turno>> GetAllFullByFecha(ConsultaHorarios entidad)
        {
            var lista = await contexto.Turnos
           .Include(x => x.Cliente)
           .Include(x => x.Bloque)
           .Include(x => x.Estado)
           .Include(x => x.Trabajo)
           .ThenInclude(x => x.Empleado)
           .Where(x =>x.Fecha == entidad.Fecha)
           .ToListAsync();

            return lista;
        }
    }
}
