using Final_Laboratorio3_Farias_Peluqueria.Repositorios;
using Final_Laboratorio3_Farias_Peluqueria.Repositorios.Implementaciones;
using Final_Laboratorio3_Farias_Peluqueria.Servicios;
using Final_Laboratorio3_Farias_Peluqueria.Servicios.Implementaciones;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Final_Laboratorio3_Farias_Peluqueria.Midlleware
{
    public static class InversionControl
    {
        public static IServiceCollection AddInterfaces(this IServiceCollection services)
        {
            // repositorios
            services.AddScoped<IRepositorioAdministradores, RepositorioAdministradores>(); 
            services.AddScoped<IRepositorioBloques, RepositorioBloques>();
            services.AddScoped<IRepositorioClientes, RepositorioClientes>();
            services.AddScoped<IRepositorioEmpleados, RepositorioEmpleados>();
            services.AddScoped<IRepositorioEstados, RepositorioEstados>();
            services.AddScoped<IRepositorioHorarios, RepositorioHorarios>();
            services.AddScoped<IRepositorioPagos, RepositorioPagos>();
            services.AddScoped<IRepositorioTipoDePagos, RepositorioTipoDePagos>();
            services.AddScoped<IRepositorioTipoDeTrabajos, RepositorioTipoDeTrabajos>();
            services.AddScoped<IRepositorioTrabajos, RepositorioTrabajos>();
            services.AddScoped<IRepositorioTurnos, RepositorioTurnos>();

            // servicios
            services.AddScoped<IServicioAdministradores, ServicioAdministradores>();
            services.AddScoped<IServicioBloques, ServicioBloques>();
            services.AddScoped<IServicioClientes, ServicioClientes>();
            services.AddScoped<IServicioEmpleados, ServicioEmpleados>();
            services.AddScoped<IServicioEstados, ServicioEstados>();
            services.AddScoped<IServicioHorarios, ServicioHorarios>();
            services.AddScoped<IServicioPagos, ServicioPagos>();
            services.AddScoped<IServicioTipoDePagos, ServicioTipoDePagos>();
            services.AddScoped<IServicioTipoDeTrabajos, ServicioTipoDeTrabajos>();
            services.AddScoped<IServicioTrabajos, ServicioTrabajos>();
            services.AddScoped<IServicioTurnos, ServicioTurnos>();



            return services;
        }
    }
}
