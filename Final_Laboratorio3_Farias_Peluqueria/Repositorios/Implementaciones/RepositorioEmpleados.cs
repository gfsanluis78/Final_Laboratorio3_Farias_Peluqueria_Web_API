using Final_Laboratorio3_Farias_Peluqueria.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Final_Laboratorio3_Farias_Peluqueria.Repositorios.Implementaciones
{
    public class RepositorioEmpleados : RepositorioBase<Empleado>, IRepositorioEmpleados
    {
        private readonly DataContext contexto;
        public RepositorioEmpleados(DataContext contexto) : base(contexto)
        {
            this.contexto = contexto;

        }

        public async Task<List<Empleado>> GetAllByTipoTrabajo(TipoDeTrabajo tipoDeTrabajo)
        {
            var lista = await contexto.Trabajos
               .Include(x=>x.Empleado)
               .Where(x => x.IdTipoDeTrabajo == tipoDeTrabajo.IdTipoDeTrabajo)
               .Select(x => x.Empleado)
               .ToListAsync();

            return lista;
        }

        public async Task<List<Empleado>> GetEmpleadosByTipoTrabajo(int id)
        {
            var lista = await contexto.Trabajos
                .Include(x => x.Empleado)
                .Where(x => x.IdTipoDeTrabajo == id)
                .Select(x => x.Empleado)
                .ToListAsync();
                
            return lista ;
        }
    }
}
