﻿using Final_Laboratorio3_Farias_Peluqueria.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Final_Laboratorio3_Farias_Peluqueria.Repositorios.Implementaciones
{
    public class RepositorioHorarios : RepositorioBase<Horario>, IRepositorioHorarios

    {
        
        public RepositorioHorarios(DataContext context) : base(context)
        {
           
        }


    }
}
