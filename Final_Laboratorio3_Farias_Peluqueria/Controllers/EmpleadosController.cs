using Final_Laboratorio3_Farias_Peluqueria.Models;
using Final_Laboratorio3_Farias_Peluqueria.Repositorios.Implementaciones;
using Final_Laboratorio3_Farias_Peluqueria.Servicios;
using Final_Laboratorio3_Farias_Peluqueria.Servicios.Implementaciones;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
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
    public class EmpleadosController : ControllerBase
    {
        private readonly IConfiguration configuration;
        private readonly IServicioEmpleados servicioEmpleados; 

        public EmpleadosController(IConfiguration configuration, IServicioEmpleados servicioEmpleados)
        {
            this.servicioEmpleados = servicioEmpleados;
            this.configuration = configuration;
        }

        // GET: api/Empelados/GetAll
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                return Ok(await servicioEmpleados.GetAll());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET api/Empleados/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            try
            {
                return Ok(await servicioEmpleados.GetById(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST api/Empleados
        [HttpPost]
        public async Task<IActionResult> Insert([FromForm] Empleado entidad)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var salt = System.Text.Encoding.ASCII.GetBytes(configuration["Salt"]); /// Busca la sal en configuration

                    // Hasheo el pass
                    string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                        password: entidad.Password,
                        salt: salt,
                        prf: KeyDerivationPrf.HMACSHA1,
                        iterationCount: 1000,
                        numBytesRequested: 256 / 8));

                    entidad.Password = hashed;

                    await servicioEmpleados.Insert(entidad);

                    return CreatedAtAction(nameof(Get), new { id = entidad.IdEmpleado }, entidad);
                }
                return BadRequest("Modelo invalido");
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        // PUT api/Empleados/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Empleado entidad)
        {
            await servicioEmpleados.Update(entidad);
            return Ok(entidad);
        }

        // DELETE api/<EmpleadosController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await servicioEmpleados.Delete(id);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}
