using Final_Laboratorio3_Farias_Peluqueria.Models;
using Final_Laboratorio3_Farias_Peluqueria.Repositorios;


namespace Final_Laboratorio3_Farias_Peluqueria.Servicios.Implementaciones
{
    public class ServicioEstados : ServicioBase<Estado>, IServicioEstados
    {
        private IRepositorioEstados repositorioEstados;

        public ServicioEstados(IRepositorioEstados repositorioEstados) : base(repositorioEstados)
        {
            this.repositorioEstados = repositorioEstados;
        }
    }
}
