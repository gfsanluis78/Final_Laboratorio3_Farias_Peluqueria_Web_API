using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Final_Laboratorio3_Farias_Peluqueria.Models
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        
        }
            public DbSet<Cliente> Clientes { get; set; }

            public DbSet<Administrador> Administradores { get; set; }

            public DbSet<Empleado> Empleados { get; set; }

            public DbSet<Trabajo> Trabajos { get; set; }

            public DbSet<TipoDeTrabajo> TipoDeTrabajos { get; set; }

            public DbSet<Turno> Turnos { get; set; }

            public DbSet<Estado> Estados { get; set; }

            public DbSet<Horario> Horarios { get; set; }

            public DbSet<Bloque> Bloques { get; set; }

            public DbSet<TipoDePago> TipoDePagos { get; set; }
        
            public DbSet<Pago> Pagos { get; set; }


            

    }
}

