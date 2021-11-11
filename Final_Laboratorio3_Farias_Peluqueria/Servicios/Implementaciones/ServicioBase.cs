using Final_Laboratorio3_Farias_Peluqueria.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Final_Laboratorio3_Farias_Peluqueria.Servicios.Implementaciones
{
    public class ServicioBase<TEntidad> : IServicioBase<TEntidad> where TEntidad : class
    {
        private IRepositorioBase<TEntidad> repositorioBase;

        public ServicioBase(IRepositorioBase<TEntidad> repositorioBase)
        {
            this.repositorioBase = repositorioBase;
        }

        public async Task Delete(int id)
        {
            await repositorioBase.Delete(id);
        }

        public async Task<List<TEntidad>> GetAll()
        {
            return await repositorioBase.GetAll();
        }

        public async Task<TEntidad> GetById(int id)
        {
            return await repositorioBase.GetById(id);
        }

        public async Task<TEntidad> Insert(TEntidad entidad)
        {
            return await repositorioBase.Insert(entidad);
        }

        public async Task<TEntidad> Update(TEntidad entidad)
        {
            return await repositorioBase.Update(entidad);
        }
    }
}
