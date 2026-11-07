using Final_Laboratorio3_Farias_Peluqueria.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Final_Laboratorio3_Farias_Peluqueria.Repositorios.Implementaciones;
using Final_Laboratorio3_Farias_Peluqueria.Servicios;
using Final_Laboratorio3_Farias_Peluqueria.Servicios.Implementaciones;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Final_Laboratorio3_Farias_Peluqueria.Controllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)] // Requiere el token JWT
    [ApiController]
    public class TurnosController : ControllerBase
    {
        private IServicioTurnos servicioTurnos;
        private DataContext contexto;
        public TurnosController(IServicioTurnos servicioTurnos, DataContext context)
        {
            this.servicioTurnos = servicioTurnos;
            this.contexto = context;
        }

        // GET: api/Turnos/GetAll
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                return Ok(await servicioTurnos.GetAll());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GetTurnosAndEmpleado
        // GET: api/Turnos/GetAll
        [HttpGet("GetTurnosAndEmpleado")]
        public async Task<IActionResult> GetTurnosAndEmpleado()
        {
            try
            {
                var resultado = await contexto.Turnos
                    .Include(t => t.Trabajo.Empleado)
                    .GroupBy(x => new { x.Trabajo.Empleado.Apellido, x.Trabajo.Empleado.IdEmpleado })
                    .Select(x=> new {Empleado = x.Key, Cantidad = x.Count()})
                    .ToListAsync();

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        // GET: api/Turnos/GetAllFull
        [HttpGet("GetAllFull")]
        public async Task<IActionResult> GetAllFull()
        {
            try
            {
                return Ok(await servicioTurnos.GetAllFull());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Post: api/Turnos/GetUltimoByCliente
        [HttpPost("GetUltimoByCliente")]
        public async Task<IActionResult> GetUltimoByCliente([FromBody] Cliente entidad)
        {
            try
            {
                return Ok(await servicioTurnos.GetUltimoByCliente(entidad));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Post: api/Turnos/GetTurnosByCliente
        [HttpPost("GetTurnosByCliente")]
        public async Task<IActionResult> GetTurnosByCliente([FromBody] Cliente entidad)
        {
            try
            {
                return Ok(await servicioTurnos.GetTurnosByCliente(entidad));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        // Post: api/Turnos/GetAllFullByFecha
        [HttpPost("GetAllFullByFecha")]
        public async Task<IActionResult> GetAllFullByFecha([FromBody] ConsultaHorarios entidad)
        {
            try
            {
                return Ok(await servicioTurnos.GetAllFullByFecha(entidad));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        // Post: api/Turnos/GetCantidadByEmpleado
        [HttpPost("GetCantidadByEmpleado")]
        public async Task<IActionResult> GetCantidadByEmpleado([FromBody] Empleado entidad)
        {
            try
            {
                return Ok(await servicioTurnos.GetCantidadByEmpleado(entidad));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET api/Turnos/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            try
            {
                return Ok(await servicioTurnos.GetById(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST api/Turnos
        [HttpPost]
        public async Task<IActionResult> Insert([FromBody] Turno entidad)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (await HorarioOcupado(entidad, 0))
                    {
                        return Conflict("El empleado ya tiene un turno en esa fecha y horario.");
                    }
                    await servicioTurnos.Insert(entidad);

                    return CreatedAtAction(nameof(Get), new { id = entidad.IdTurno }, entidad);
                }
                return BadRequest("Modelo invalido");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // PUT api/Turnos/5
        [HttpPatch("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Turno entidad)
        {
            try
            {
                if (await HorarioOcupado(entidad, id))
                {
                    return Conflict("El empleado ya tiene un turno en esa fecha y horario.");
                }
                await servicioTurnos.Update(entidad);
                return Ok(entidad);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // true si el empleado del trabajo ya tiene otro turno en la misma fecha y bloque.
        // Es la misma regla que usa la app Android para mostrar los horarios libres
        // (Bloques/GetAllByHorarioByEmpleado): cualquier turno existente ocupa el horario.
        // Los turnos con estado "Cancelado" no ocupan el horario.
        private async Task<bool> HorarioOcupado(Turno entidad, int idTurnoActual)
        {
            // Un turno que se guarda como cancelado no ocupa nada.
            bool esCancelado = await contexto.Estados
                .AnyAsync(e => e.IdEstado == entidad.IdEstado && e.Nombre == "Cancelado");
            if (esCancelado)
            {
                return false;
            }

            int idEmpleado = await contexto.Trabajos
                .Where(t => t.IdTrabajo == entidad.IdTrabajo)
                .Select(t => t.IdEmpleado)
                .FirstOrDefaultAsync();

            return await contexto.Turnos.AnyAsync(t =>
                t.IdTurno != idTurnoActual &&
                t.Fecha == entidad.Fecha &&
                t.IdBloque == entidad.IdBloque &&
                t.Trabajo.IdEmpleado == idEmpleado &&
                t.Estado.Nombre != "Cancelado");
        }

        // DELETE api/<TurnosController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {

                await servicioTurnos.Delete(id);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
