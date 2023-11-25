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
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.IdentityModel.Tokens;

namespace Final_Laboratorio3_Farias_Peluqueria.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpleadosController : ControllerBase
    {
        private readonly IConfiguration configuration;
        private readonly IServicioEmpleados servicioEmpleados;
        private readonly IWebHostEnvironment environment;

        public EmpleadosController(IConfiguration configuration, IServicioEmpleados servicioEmpleados, IWebHostEnvironment environment)
        {
            this.servicioEmpleados = servicioEmpleados;
            this.configuration = configuration;
            this.environment = environment;
        }

        // GET: api/Emplelados/GetAll
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

        // GetAllByTipoTrabajo
        // GET: api/Empelados/GetAllByTipoTrabajo
        [HttpPost("GetAllByTipoTrabajo")]
        public async Task<IActionResult> GetAllByTipoTrabajo([FromBody] TipoDeTrabajo tipoDeTrabajo)
        {
            try
            {
                return Ok(await servicioEmpleados.GetAllByTipoTrabajo(tipoDeTrabajo));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        // POST: api/Empleados/GetAllByTipo
        [HttpPost("GetAllByTipo")]
        public async Task<IActionResult> GetAllByTipo([FromBody] TipoDeTrabajo t)
        {
            try
            {
                return Ok(await servicioEmpleados.GetEmpleadosByTipoTrabajo(t.IdTipoDeTrabajo));
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
            if (id < 1)
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
                    string hashed = HasheoPassword(entidad);
                    entidad.Password = hashed;
                    Empleado res = await servicioEmpleados.Insert(entidad);

                    if (entidad.AvatarFile != null && res.IdEmpleado > 0)
                    {
                        Empleado conAvatar = RecibeAvatar(entidad);
                        await servicioEmpleados.Update(conAvatar);
                        return CreatedAtAction(nameof(Get), new { id = entidad.IdEmpleado }, entidad);
                    }
                    if (res.IdEmpleado > 0)
                    {
                        entidad.Avatar = "AvatarBase/avatar_50000.jpg";
                        await servicioEmpleados.Update(entidad);
                        return CreatedAtAction(nameof(Get), new { id = entidad.IdEmpleado }, entidad);
                    }
                    return BadRequest(Ok("No se pudo generar el empleado"));
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
        public async Task<IActionResult> Update(int id, [FromForm] Empleado entidad)
        {
            try
            {
                if (entidad.AvatarFile != null)
                {
                    Empleado conAvatarNuevo = RecibeAvatar(entidad);
                    await servicioEmpleados.Update(conAvatarNuevo);
                    return Ok(entidad);
                }
                await servicioEmpleados.Update(entidad);
                return Ok(entidad);
            }
            catch (Exception e)
            {
                return BadRequest("El error es" + e.Message);
            }
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
            name = "prof_" + id + "_" + name + ".jpg";
            return name;
        }

        private string Conversor(string Path)
        {
            byte[] imageArray = System.IO.File.ReadAllBytes(Path);
            string base64ImageRepresentation = Convert.ToBase64String(imageArray);
            return base64ImageRepresentation;
        }

        private Empleado RecibeAvatar(Empleado entidad)
        {
            string wwwPath = environment.WebRootPath;
            string path = Path.Combine(wwwPath, "Empleados");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            Random r = new Random();

            string fileName = CambiarNombre((int)entidad.IdEmpleado);
            string pathCompleto = Path.Combine(path, fileName);

            entidad.Avatar = Path.Combine("Empleados", fileName);
            using (FileStream stream = new FileStream(pathCompleto, FileMode.Create))
            {
                entidad.AvatarFile.CopyTo(stream);
            }
            return entidad;
        }

        private string HasheoPassword(Empleado entidad)
        {
            var salt = System.Text.Encoding.ASCII.GetBytes(configuration["Salt"]); /// Busca la sal en configuration

            // Hasheo el pass
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: entidad.Password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 1000,
                numBytesRequested: 256 / 8));
            return hashed;
        }

    }
}
