using Final_Laboratorio3_Farias_Peluqueria.Models;
using Final_Laboratorio3_Farias_Peluqueria.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Final_Laboratorio3_Farias_Peluqueria.Servicios.Implementaciones
{
    public class ServicioPagos : ServicioBase<Pago>, IServicioPagos
    {
        private IRepositorioPagos repositorioPagos;

        public ServicioPagos(IRepositorioPagos repositorioPagos) : base(repositorioPagos)
        {
            this.repositorioPagos = repositorioPagos;
        }
    }
}
