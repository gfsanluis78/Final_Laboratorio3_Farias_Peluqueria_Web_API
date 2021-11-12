using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Final_Laboratorio3_Farias_Peluqueria.Models
{
    public class Horario
    {
        [Key]
        public int IdHorario { get; set; }

        public int IdBloque { get; set; }

        public DateTime Fecha { get; set; }

        public string notas { get; set; }

        [ForeignKey(nameof(IdBloque))]
        public Bloque Bloque { get; set; }
    }
}
