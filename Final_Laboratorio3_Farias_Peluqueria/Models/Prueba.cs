using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Final_Laboratorio3_Farias_Peluqueria.Models
{
    public class Prueba
    {
        [Key]
        public int Id { get; set; }

        public string Mensaje { get; set; }

    }
}
