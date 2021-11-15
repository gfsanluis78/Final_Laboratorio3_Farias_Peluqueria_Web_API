using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Final_Laboratorio3_Farias_Peluqueria.Models
{
    public class Turno
    {
        [Key]
        public int IdTurno { get; set; }

        public int IdBloque { get; set; }

        public int IdEstado { get; set; }

        public int IdTrabajo { get; set; }

        public int IdCliente { get; set; }

        public string Notas { get; set; }

        public string Fecha { get; set; }

        public string Costo { get; set; }

        public string FechaCreacion { get; set; }

        [ForeignKey(nameof(IdBloque))]
        public Bloque Bloque { get; set; }

        [ForeignKey(nameof(IdEstado))]
        public Estado Estado { get; set; }

        [ForeignKey(nameof(IdTrabajo))]
        public Trabajo Trabajo { get; set; }

        [ForeignKey(nameof(IdCliente))]
        public Cliente Cliente { get; set; }


    }
}
