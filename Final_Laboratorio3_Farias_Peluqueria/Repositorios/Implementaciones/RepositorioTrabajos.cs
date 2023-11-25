using Final_Laboratorio3_Farias_Peluqueria.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Final_Laboratorio3_Farias_Peluqueria.Repositorios.Implementaciones
{
    public class RepositorioTrabajos : RepositorioBase<Trabajo>, IRepositorioTrabajos
    {
        private readonly DataContext contexto;

        public RepositorioTrabajos(DataContext context) : base(context)
        {
            this.contexto = context;
        }

        public async Task<List<Trabajo>> GetAllByTipoTrabajoByEmpleado(ConsultaByTipoTrabajo entidad)
        {
            var lista = await contexto.Trabajos
              .Include(x => x.Empleado)
              .Include(x => x.TipoDeTrabajo)
              .Where(x => x.TipoDeTrabajo.IdTipoDeTrabajo == entidad.IdTipoTrabajo)
              .ToListAsync();

            return lista;
        }

        public async Task<List<Trabajo>> GetAllFull()
        {
            var lista = await contexto.Trabajos
              .Include(x => x.Empleado)
              .Include(x => x.TipoDeTrabajo)
              .ToListAsync();

            return lista;
        }
    }
}
