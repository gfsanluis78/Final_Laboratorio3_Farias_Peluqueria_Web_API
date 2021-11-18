using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Final_Laboratorio3_Farias_Peluqueria.Models
{
    public class CanntidadTurnos
    {
        private object cantidad;

        public CanntidadTurnos(object cantidad)
        {
            this.cantidad = cantidad;
        }

        CanntidadTurnos(int cantidad)
        {
            this.Cantidad = cantidad;
        }

        int Cantidad { get; set; }
    }
}
