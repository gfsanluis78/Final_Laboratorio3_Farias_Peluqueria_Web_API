using Final_Laboratorio3_Farias_Peluqueria.Models;
using Final_Laboratorio3_Farias_Peluqueria.Repositorios.Implementaciones;
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
    public class PagosController : ControllerBase
    {
        private DataContext context;
        private readonly IConfiguration configuration;

        public PagosController(DataContext context, IConfiguration configuration)
        {
            this.context = context;
            this.configuration = configuration;
        }

        // GET: api/Pagos/GetAll
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var servicioPagos = new ServicioPagos(new RepositorioPagos(context));
                return Ok(await servicioPagos.GetAll());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET api/Pagos/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            try
            {
                var servicioPagos = new ServicioPagos(new RepositorioPagos(context));
                return Ok(await servicioPagos.GetById(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST api/Pagos
        [HttpPost]
        public async Task<IActionResult> Insert([FromForm] Pago entidad)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var servicioPagos = new ServicioPagos(new RepositorioPagos(context));
                    await servicioPagos.Insert(entidad);

                    return CreatedAtAction(nameof(Get), new { id = entidad.IdTipoDePago }, entidad);
                }
                return BadRequest("Modelo invalido");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // PUT api/Pagos/5
        [HttpPatch("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Pago entidad)
        {
            var servicioPagos = new ServicioPagos(new RepositorioPagos(context));
            await servicioPagos.Update(entidad);
            return Ok(entidad);
        }

        // DELETE api/<PagosController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var servicioPagos = new ServicioPagos(new RepositorioPagos(context));
                await servicioPagos.Delete(id);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
