using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Final_Laboratorio3_Farias_Peluqueria.Models
{
    public class Pago
    {
        [Key]
        public int IdPago { get; set; }

        public int IdTipoDePago { get; set; }

        public int IdTurno { get; set; }

        public bool Borrado { get; set; }

        public string Notas { get; set; }

        [ForeignKey(nameof(IdTipoDePago))]
        public TipoDePago TipoDePago { get; set; }
    }
}
