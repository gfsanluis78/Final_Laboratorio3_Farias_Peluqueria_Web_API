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
    public class TipoDeTrabajosController : ControllerBase
    {
        private IServicioTipoDeTrabajos servicioTipoDeTrabajos;

        public TipoDeTrabajosController(IServicioTipoDeTrabajos servicioTipoDeTrabajos)
        {
            this.servicioTipoDeTrabajos = servicioTipoDeTrabajos;
        }

        // GET: api/TipoDeTrabajos/GetAll
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                return Ok(await servicioTipoDeTrabajos.GetAll());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET api/TipoDeTrabajos/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            try
            {
                return Ok(await servicioTipoDeTrabajos.GetById(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST api/TipoDeTrabajos
        [HttpPost]
        public async Task<IActionResult> Insert([FromForm] TipoDeTrabajo entidad)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await servicioTipoDeTrabajos.Insert(entidad);

                    return CreatedAtAction(nameof(Get), new { id = entidad.IdTipoDeTrabajo }, entidad);
                }
                return BadRequest("Modelo invalido");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // PUT api/TipoDeTrabajos/5
        [HttpPatch("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] TipoDeTrabajo entidad)
        {
            await servicioTipoDeTrabajos.Update(entidad);
            return Ok(entidad);
        }

        // DELETE api/<TipoDeTrabajosController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await servicioTipoDeTrabajos.Delete(id);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
