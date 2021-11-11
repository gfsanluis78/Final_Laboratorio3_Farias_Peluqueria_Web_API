using Final_Laboratorio3_Farias_Peluqueria.Models;
using Final_Laboratorio3_Farias_Peluqueria.Repositorios.Implementaciones;
using Final_Laboratorio3_Farias_Peluqueria.Servicios.Implementaciones;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Final_Laboratorio3_Farias_Peluqueria.Controllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]     // Aca aclaro que para autorizar revise lo tokens
    [ApiController]
    public class ClientesController : ControllerBase

    {
        private DataContext contexto;
        private readonly IConfiguration configuration;

        public ClientesController(DataContext contexto, IConfiguration configuration)
        {
            this.contexto = contexto;
            this.configuration = configuration;
        }


        // GET: api/<ClientesController>
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var serviciosCliente = new ServicioClientes(new RepositorioClientes(contexto));
                return Ok(await serviciosCliente.GetAll());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // GET api/<ClientesController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if (id == null)
            {
                return Ok("No existe id");
            }
            try
            {
                var serviciosCliente = new ServicioClientes(new RepositorioClientes(contexto));
                return Ok(serviciosCliente.GetById(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // POST api/<ClientesController>
        [HttpPost]
        public async Task<IActionResult> Post([FromForm] Cliente entidad)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var serviciosCliente = new ServicioClientes(new RepositorioClientes(contexto));
                    await serviciosCliente.Insert(entidad);

                    return CreatedAtAction(nameof(Get), new { id = entidad.IdCliente }, entidad);
                }
                return BadRequest("Modelo invalido");
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
            
           
        }

        // PUT api/<ClientesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ClientesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
