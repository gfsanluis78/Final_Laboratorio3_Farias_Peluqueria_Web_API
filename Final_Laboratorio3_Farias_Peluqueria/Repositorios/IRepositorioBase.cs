using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Final_Laboratorio3_Farias_Peluqueria.Repositorios
{
    public interface IRepositorioBase<TEntidad> where TEntidad : class
    {
        Task<List<TEntidad>> GetAll();

        Task<TEntidad> GetById(int id);

        Task<TEntidad> Insert(TEntidad entidad);

        Task<TEntidad> Update(TEntidad entidad);

        Task Delete(int id);
    }
}
