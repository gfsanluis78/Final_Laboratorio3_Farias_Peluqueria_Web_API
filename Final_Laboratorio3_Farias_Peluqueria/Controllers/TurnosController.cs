using Final_Laboratorio3_Farias_Peluqueria.Models;
using Final_Laboratorio3_Farias_Peluqueria.Repositorios.Implementaciones;
using Final_Laboratorio3_Farias_Peluqueria.Servicios;
using Final_Laboratorio3_Farias_Peluqueria.Servicios.Implementaciones;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Final_Laboratorio3_Farias_Peluqueria.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TurnosController : ControllerBase
    {
        private IServicioTurnos servicioTurnos;
        public TurnosController(IServicioTurnos servicioTurnos)
        {
            this.servicioTurnos = servicioTurnos;
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
            await servicioTurnos.Update(entidad);
            return Ok(entidad);
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
