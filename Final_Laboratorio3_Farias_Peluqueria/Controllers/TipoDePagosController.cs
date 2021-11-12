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
    public class TipoDePagosController : ControllerBase
    {
        private IServicioTipoDePagos servicioTipoDePagos; 

        public TipoDePagosController(IServicioTipoDePagos servicioTipoDePagos)
        {
            this.servicioTipoDePagos = servicioTipoDePagos;
        }

        // GET: api/TipoDePagos/GetAll
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                return Ok(await servicioTipoDePagos.GetAll());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET api/TipoDePagos/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            try
            {
                return Ok(await servicioTipoDePagos.GetById(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST api/TipoDePagos
        [HttpPost]
        public async Task<IActionResult> Insert([FromForm] TipoDePago entidad)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await servicioTipoDePagos.Insert(entidad);

                    return CreatedAtAction(nameof(Get), new { id = entidad.IdTipoDePago }, entidad);
                }
                return BadRequest("Modelo invalido");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // PUT api/TipoDePagos/5
        [HttpPatch("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] TipoDePago entidad)
        {
            await servicioTipoDePagos.Update(entidad);
            return Ok(entidad);
        }

        // DELETE api/<TipoDePagosController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await servicioTipoDePagos.Delete(id);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
