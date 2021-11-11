﻿using Final_Laboratorio3_Farias_Peluqueria.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Final_Laboratorio3_Farias_Peluqueria.Repositorios.Implementaciones
{
    public class RepositorioTipoDePagos : RepositorioBase<TipoDePago>, IRepositorioTipoDePagos
    {
        public RepositorioTipoDePagos(DataContext context) : base(context) 
        { 

        }
    }
}
