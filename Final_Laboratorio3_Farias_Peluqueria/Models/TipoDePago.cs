using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Final_Laboratorio3_Farias_Peluqueria.Models
{
    public class TipoDePago
    {
        [Key]
        public int IdTipoDePago { get; set; }

        public string Nombre { get; set; }

        public string Descripcion { get; set; }

    }
}
