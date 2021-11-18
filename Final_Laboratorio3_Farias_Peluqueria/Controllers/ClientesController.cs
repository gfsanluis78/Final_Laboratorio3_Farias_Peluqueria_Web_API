using Final_Laboratorio3_Farias_Peluqueria.Models;
using Final_Laboratorio3_Farias_Peluqueria.Repositorios.Implementaciones;
using Final_Laboratorio3_Farias_Peluqueria.Servicios;
using Final_Laboratorio3_Farias_Peluqueria.Servicios.Implementaciones;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
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
        private readonly IServicioClientes servicioClientes;
        private readonly IWebHostEnvironment environment;
        private readonly DataContext contexto;

        public ClientesController(IServicioClientes servicioClientes, IWebHostEnvironment environment, DataContext contexto)
        {
            this.contexto = contexto;
            this.servicioClientes = servicioClientes;
            this.environment = environment;
        }


        // GET: api/<ClientesController>
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                return Ok(await servicioClientes.GetAll());
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
                return Ok(await servicioClientes.GetById(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // POST api/<ClientesController>
        [HttpPost]
        public async Task<IActionResult> Insert([FromBody] Cliente entidad)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await servicioClientes.Insert(entidad);

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
        public async Task<IActionResult> Update(int id, [FromBody] Cliente entidad)
        {
            await servicioClientes.Update(entidad);
            return Ok(entidad);
        }

        // DELETE api/<EmpleadosController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await servicioClientes.Delete(id);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // Post
        [HttpPost("InsertConFoto")]
        public async Task<IActionResult> InsertConFoto([FromBody] Cliente entidad)
        {
            try
            {
                if (entidad.Avatar.Length > 0)
                {
                    if (ModelState.IsValid)
                    {
                        var stream1 = new MemoryStream(Convert.FromBase64String(entidad.Avatar));
                        IFormFile ImagenInmo = new FormFile(stream1, 0, stream1.Length, "cliente", ".jpg");
                        string wwwPath = environment.WebRootPath;
                        string path = Path.Combine(wwwPath, "Clientes");
                        if (!Directory.Exists(path))
                        {
                            Directory.CreateDirectory(path);
                        }
                        Random r = new Random();
                        //Path.GetFileName(u.AvatarFile.FileName);//este nombre se puede repetir
                        string fileName = CambiarNombre((int)entidad.IdCliente);          // "inmueble_" + entidad.IdPropietario + r.Next(0, 100000) + Path.GetExtension(ImagenInmo.FileName);
                        string pathCompleto = Path.Combine(path, fileName);

                        entidad.Avatar = Path.Combine("/Clientes", fileName);
                        using (FileStream stream = new FileStream(pathCompleto, FileMode.Create))
                        {
                            ImagenInmo.CopyTo(stream);
                        }

                        contexto.Clientes.Add(entidad);
                        contexto.SaveChanges();
                        return CreatedAtAction(nameof(Get), new { id = entidad.IdCliente }, entidad);
                    }
                    return BadRequest(ModelState);
                }
                return BadRequest(Ok("Falta imagen"));
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }


        //################################################################
        //    Metodo para generar un nombre al azar para las imagenes
        //################################################################
        private string CambiarNombre(int id)
        {
            //caracteres para el nombre nuevo
            string chars = "23456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            //crean un generador al azar
            Random rnd = new Random();
            string name = string.Empty;
            while (name.Length < 8)
            {
                name += chars.Substring(rnd.Next(chars.Length), 1);
            }
            //agregamos un prefijo al nombre generado al azar y la extension del mismo
            name = "pelu_" + id + "_" + name + ".jpg";
            return name;
        }


    }
}
