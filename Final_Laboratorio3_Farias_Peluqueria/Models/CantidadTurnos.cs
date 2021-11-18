using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Final_Laboratorio3_Farias_Peluqueria.Models
{
    public class CantidadTurnos
    {
        private int cantidad;
        private String empleado;

        public CantidadTurnos(String empleado, int cantidad)
        {
            this.cantidad = cantidad;
            this.empleado = empleado;
        }

        CantidadTurnos(int cantidad)
        {
            this.Cantidad = cantidad;
        }

        String Empleado { get; set; }

        int Cantidad { get; set; }


    }
}
