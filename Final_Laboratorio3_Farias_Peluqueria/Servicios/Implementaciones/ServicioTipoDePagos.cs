using Final_Laboratorio3_Farias_Peluqueria.Models;
using Final_Laboratorio3_Farias_Peluqueria.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Final_Laboratorio3_Farias_Peluqueria.Servicios.Implementaciones
{
    public class ServicioTipoDePagos : ServicioBase<TipoDePago>, IServicioTipoDePagos 
    {
        private IRepositorioTipoDePagos repositorioTipoDePagos;

        public ServicioTipoDePagos(IRepositorioTipoDePagos repositorioTipoDePagos) : base(repositorioTipoDePagos)
        {
            this.repositorioTipoDePagos = repositorioTipoDePagos;
        }
    }
}
