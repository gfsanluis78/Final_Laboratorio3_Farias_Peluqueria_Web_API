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
    public class HorariosController : ControllerBase
    {
        private readonly IServicioHorarios servicioHorarios;

        public HorariosController(IServicioHorarios servicioHorarios)
        {
            this.servicioHorarios = servicioHorarios;
        }

        // GET: api/Horarios/GetAll
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                return Ok(await servicioHorarios.GetAll());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET api/Horarios/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            try
            {
                return Ok(await servicioHorarios.GetById(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST api/Horarios
        [HttpPost]
        public async Task<IActionResult> Insert([FromForm] Horario entidad)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await servicioHorarios.Insert(entidad);

                    return CreatedAtAction(nameof(Get), new { id = entidad.IdHorario }, entidad);
                }
                return BadRequest("Modelo invalido");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // PUT api/Horarios/5
        [HttpPatch("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Horario entidad)
        {
            await servicioHorarios.Update(entidad);
            return Ok(entidad);
        }

        // DELETE api/<HorariosController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await servicioHorarios.Delete(id);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

       

    }
}
