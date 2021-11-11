using Final_Laboratorio3_Farias_Peluqueria.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Final_Laboratorio3_Farias_Peluqueria.Repositorios.Implementaciones
{
    public class RepositorioBase<TEntidad> : IRepositorioBase<TEntidad> where TEntidad : class
    {
        private readonly DataContext contexto;

        public RepositorioBase(DataContext contexto)
        {
            this.contexto = contexto;
        }


        public async Task Delete(int id)
        {
            var entidad = await GetById(id);

            if (entidad == null)
                throw new Exception("La entidad es nula");

            contexto.Set<TEntidad>().Remove(entidad);
            await contexto.SaveChangesAsync();
        }

        public async Task<List<TEntidad>> GetAll()
        {
            return await contexto.Set<TEntidad>().ToListAsync();
        }

        public async Task<TEntidad> GetById(int id)
        {
            return await contexto.Set<TEntidad>().FindAsync(id);
        }

        public async Task<TEntidad> Insert(TEntidad entidad)
        {
            await contexto.Set<TEntidad>().AddAsync(entidad);
            await contexto.SaveChangesAsync();
            return entidad;
        }

        public async Task<TEntidad> Update(TEntidad entidad)
        {
            contexto.Set<TEntidad>().Update(entidad);
            await contexto.SaveChangesAsync();
            return entidad;        }
    }
}
