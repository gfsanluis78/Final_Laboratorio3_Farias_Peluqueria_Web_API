using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Final_Laboratorio3_Farias_Peluqueria.Models
{
    public class Trabajo
    {
        [Key]
        public int IdTrabajo { get; set; }

        public int IdTipoDeTrabajo { get; set; }

        public int IdEmpleado { get; set; }

        public string Comentarios { get; set; }

        [ForeignKey(nameof(IdTipoDeTrabajo))]
        public TipoDeTrabajo TipoDeTrabajo { get; set; }

        [ForeignKey(nameof(IdEmpleado))]
        public Empleado Empleado { get; set; }







    }
}
