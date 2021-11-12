using Final_Laboratorio3_Farias_Peluqueria.Models;
using Final_Laboratorio3_Farias_Peluqueria.Repositorios.Implementaciones;
using Final_Laboratorio3_Farias_Peluqueria.Servicios;
using Final_Laboratorio3_Farias_Peluqueria.Servicios.Implementaciones;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace Final_Laboratorio3_Farias_Peluqueria.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BloquesController : ControllerBase
    {
        private readonly IServicioBloques servicioBloques;
                      

        public BloquesController(IServicioBloques servicioBloques)
        {
            this.servicioBloques = servicioBloques;
          }

        // GET: api/Bloques/GetAll
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                return Ok(await servicioBloques.GetAll());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET api/Bloques/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            try
            {
                return Ok(await servicioBloques.GetById(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST api/Bloques
        [HttpPost]
        public async Task<IActionResult> Insert([FromForm] Bloque entidad)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await servicioBloques.Insert(entidad);

                    return CreatedAtAction(nameof(Get), new { id = entidad.IdBloque }, entidad);
                }
                return BadRequest("Modelo invalido");
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        // PATCH api/Bloques/5
        [HttpPatch("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Bloque entidad)
        {
            await servicioBloques.Update(entidad);
            return Ok(entidad);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await servicioBloques.Delete(id);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
