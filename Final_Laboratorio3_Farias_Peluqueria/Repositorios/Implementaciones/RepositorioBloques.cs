using Final_Laboratorio3_Farias_Peluqueria.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Final_Laboratorio3_Farias_Peluqueria.Repositorios.Implementaciones
{
    public class RepositorioBloques : RepositorioBase<Bloque>, IRepositorioBloques 
    {
        public RepositorioBloques(DataContext contexto) : base(contexto)
        {

        }
    }
}
