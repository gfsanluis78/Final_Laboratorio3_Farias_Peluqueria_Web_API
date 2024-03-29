﻿using Final_Laboratorio3_Farias_Peluqueria.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Final_Laboratorio3_Farias_Peluqueria.Servicios
{
    public interface IServicioTrabajos : IServicioBase<Trabajo>
    {
        Task<List<Trabajo>> GetAllByTipoTrabajoByEmpleado(ConsultaByTipoTrabajo entidad);

        Task<List<Trabajo>> GetAllFull();
    }
}
