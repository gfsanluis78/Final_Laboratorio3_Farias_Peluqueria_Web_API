using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Final_Laboratorio3_Farias_Peluqueria.Models
{
    public class TipoDeTrabajo
    {
        [Key]
        public int IdTipoDeTreabajo { get; set; }

        public string Nombre { get; set; }

        public string Descripcion { get; set; }

        public string Duracion { get; set; }

        public string Costo { get; set; }

        public string Genero { get; set; }

        public string Imagen { get; set; }



    }
}
