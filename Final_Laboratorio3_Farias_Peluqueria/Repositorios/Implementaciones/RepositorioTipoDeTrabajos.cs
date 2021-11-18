using Final_Laboratorio3_Farias_Peluqueria.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Final_Laboratorio3_Farias_Peluqueria.Repositorios.Implementaciones
{
    public class RepositorioTipoDeTrabajos : RepositorioBase<TipoDeTrabajo>, IRepositorioTipoDeTrabajos 
    {
        private readonly DataContext contexto;

        public RepositorioTipoDeTrabajos(DataContext context) : base(context)
        {
            this.contexto = context;
        }

        public async Task<List<TipoDeTrabajo>> GetAllByEmpleado(Empleado empleado)
        {
            var lista = await contexto.Trabajos
             .Include(x => x.TipoDeTrabajo)
             .Where(x => x.IdEmpleado == empleado.IdEmpleado)
             .Select(x => x.TipoDeTrabajo)
             .ToListAsync();

            return lista;
        }
    }
}
