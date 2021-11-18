using Final_Laboratorio3_Farias_Peluqueria.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Final_Laboratorio3_Farias_Peluqueria.Repositorios.Implementaciones
{
   

    public class RepositorioClientes : RepositorioBase<Cliente>, IRepositorioClientes
    {
               

        public RepositorioClientes(DataContext context) : base(context)
        {
       
        }

    }
}
