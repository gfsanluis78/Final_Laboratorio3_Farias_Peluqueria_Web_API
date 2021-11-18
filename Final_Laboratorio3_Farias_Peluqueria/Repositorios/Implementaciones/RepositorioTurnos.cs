using Final_Laboratorio3_Farias_Peluqueria.Models;
using Microsoft.AspNetCore.Mvc;
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
              .Include(x => x.Trabajo.TipoDeTrabajo)
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
               .Include(x => x.Trabajo.TipoDeTrabajo)
               .Where(x =>x.Fecha == entidad.Fecha)
               .ToListAsync();

            return lista;
        }

        public async Task<int> GetCantidadByEmpleado(Empleado empleado)
        {
            var cantidad = contexto.Turnos.Count(t => t.Trabajo.Empleado.IdEmpleado == empleado.IdEmpleado);
                
            return cantidad;
        }

        //public async Task<List<Turno>> GetTurnosAndEmpleado()
        //{

        //}

        public async Task<List<Turno>> GetTurnosByCliente(Cliente cliente)
        {
            var turnos = await contexto.Turnos
                .Include(x => x.Bloque)
                .Include(x => x.Cliente)
                .Include(x => x.Estado)
                .Include(x => x.Trabajo)
                .ThenInclude(x => x.Empleado)
                .Include(x => x.Trabajo.TipoDeTrabajo)
                .Where(x => x.Cliente.IdCliente == cliente.IdCliente)
                .ToListAsync();
            return turnos;
        }

        public async Task<Turno> GetUltimoByCliente(Cliente cliente)
        {
            var turno = await contexto.Turnos
                .Include(x => x.Bloque)
                .Include(x => x.Cliente)
                .Include(x => x.Estado)
                .Include(x=> x.Trabajo)
                .ThenInclude(x=>x.Empleado)
                .Include(x=>x.Trabajo.TipoDeTrabajo)
                .Where(x=>x.Cliente.IdCliente == cliente.IdCliente)
                .OrderBy(a => a.IdTurno)
                .LastOrDefaultAsync(x => x.Cliente.IdCliente == cliente.IdCliente);
            if(turno == null)
            {
                turno = new Turno();
                turno.Fecha = "";     
            }

            return turno;
                
        }
    }
}
