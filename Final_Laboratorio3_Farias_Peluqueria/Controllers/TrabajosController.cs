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
    public class TrabajosController : ControllerBase
    {
        private IServicioTrabajos servicioTrabajos;

        public TrabajosController(IServicioTrabajos servicioTrabajos)
        {
            this.servicioTrabajos = servicioTrabajos;
        }

        // GET: api/Trabajos/GetAll
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                return Ok(await servicioTrabajos.GetAll());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET api/Trabajos/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            try
            {
                return Ok(await servicioTrabajos.GetById(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST api/Trabajos
        [HttpPost]
        public async Task<IActionResult> Insert([FromForm] Trabajo entidad)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await servicioTrabajos.Insert(entidad);

                    return CreatedAtAction(nameof(Get), new { id = entidad.IdTrabajo }, entidad);
                }
                return BadRequest("Modelo invalido");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // PUT api/Trabajos/5
        [HttpPatch("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Trabajo entidad)
        {
            try
            {
                await servicioTrabajos.Update(entidad);
                return Ok(entidad);
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }

           
        }

        // DELETE api/<TrabajosController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await servicioTrabajos.Delete(id);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // POST: api/Trabajos/GetAllByTipoTrabajoByEmpleado
        [HttpPost("GetAllByTipoTrabajoByEmpleado")]
        public async Task<IActionResult> GetAllByTipoTrabajoByEmpleado([FromBody] ConsultaByTipoTrabajo e)
        {
            try
            {
                return Ok(await servicioTrabajos.GetAllByTipoTrabajoByEmpleado(e));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
