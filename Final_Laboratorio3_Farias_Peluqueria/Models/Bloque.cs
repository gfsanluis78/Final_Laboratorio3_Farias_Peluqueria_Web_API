using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Final_Laboratorio3_Farias_Peluqueria.Models
{
    public class Bloque
    {
        [Key]
        [Display(Name = "Código")]
        public int IdBloque { get; set; }

        public int Orden { get; set; }

        public string desde { get; set; }
        
        public string hasta { get; set; }

        public string descripcion { get; set; }

        public string desdeHasta()
        {
            return this.desde + " " + this.hasta;
        }


    }
}
