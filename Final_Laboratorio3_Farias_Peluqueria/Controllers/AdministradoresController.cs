using Final_Laboratorio3_Farias_Peluqueria.Models;
using Final_Laboratorio3_Farias_Peluqueria.Repositorios.Implementaciones;
using Final_Laboratorio3_Farias_Peluqueria.Servicios.Implementaciones;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;

using System.Security.Claims;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Final_Laboratorio3_Farias_Peluqueria.Controllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]     // Aca aclaro que para autorizar revise lo tokens
    [ApiController]
    public class AdministradoresController : ControllerBase
    {
        private readonly DataContext dataContext;
        private readonly IConfiguration iconfiguration;

        public AdministradoresController(DataContext dataContext, IConfiguration configuration)
        {
            this.dataContext = dataContext;
            this.iconfiguration = configuration;
        }

        // GET: api/<AdministradoresController>
       
        /// <summary>
        /// Toma los datos de token y devuelve un administrador si lo encuentra en la bd
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var usuario = User.Identity.Name;

                Administrador admin = await dataContext.Administradores.SingleOrDefaultAsync(x => x.Email == usuario);
                return admin != null? Ok(admin) : Ok("No se encontro " + usuario + ".");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET api/<AdministradoresController>/5
        /// <summary>
        /// Recibe un Id y devuelve un objeto administrador
        /// </summary>
        /// <param name="id">id del administrador a buscar</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            try
            {
                var servicioAdministradores = new ServicioAdministradores(new RepositorioAdministradores(dataContext));
                return Ok(servicioAdministradores.GetById(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET api/<controller>/GetAll					// Ok
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var servicioAdministradores = new ServicioAdministradores(new RepositorioAdministradores(dataContext));
                return Ok(await servicioAdministradores.GetAll());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // POST api/<controller>/login					// Ok
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromForm] String usuario, [FromForm] String clave)
        {
            try
            {
                string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                    password: clave,
                    salt: System.Text.Encoding.ASCII.GetBytes(iconfiguration["Salt"]),
                    prf: KeyDerivationPrf.HMACSHA1,
                    iterationCount: 1000,
                    numBytesRequested: 256 / 8));
                var a = await dataContext.Administradores.FirstOrDefaultAsync(x => x.Email == usuario);
                if (a == null || a.Password != hashed)
                {
                    return BadRequest("Nombre de usuario o clave incorrecta");
                }
                else
                {
                    // Para el json w token

                    var key = new SymmetricSecurityKey(
                        System.Text.Encoding.ASCII.GetBytes(iconfiguration["TokenAuthentication:SecretKey"]));
                    var credenciales = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var claims = new List<Claim>
                    {
                        new Claim("Id", a.IdAdministrador+""),
                        new Claim(ClaimTypes.Name, a.Email),
                        new Claim("FullName", a.Nombre + " " + a.Apellido),
                        new Claim(ClaimTypes.Role, "Administrador"),
                    };

                    var token = new JwtSecurityToken(
                        issuer: iconfiguration["TokenAuthentication:Issuer"],
                        audience: iconfiguration["TokenAuthentication:Audience"],
                        claims: claims,
                        expires: DateTime.Now.AddYears(1),
                        signingCredentials: credenciales
                    );

                    var rta =  new LoginRetrofit ( 
                        new JwtSecurityTokenHandler().WriteToken(token)
                        );

                    return Ok(rta);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        // POST api/Administradores/Crear					// Ok faltaria mejorar el mensaje de fallo insertar datos
        [HttpPost("Crear")]
        public async Task<IActionResult> Crear([FromForm] Administrador entidad)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var salt = System.Text.Encoding.ASCII.GetBytes(iconfiguration["Salt"]); /// Busca la sal en configuration

                    // Hasheo el pass
                    string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                        password: entidad.Password,
                        salt: salt,
                        prf: KeyDerivationPrf.HMACSHA1,
                        iterationCount: 1000,
                        numBytesRequested: 256 / 8));

                    entidad.Password = hashed;


                    var serviciosAdministradores = new ServicioAdministradores(new RepositorioAdministradores(dataContext));
                    await serviciosAdministradores.Insert(entidad);

                    return CreatedAtAction(nameof(Get), new { id = entidad.IdAdministrador }, entidad);
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<AdministradoresController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Administrador entidad)
        {
             var serviciosAdministradores = new ServicioAdministradores(new RepositorioAdministradores(dataContext));
                await serviciosAdministradores.Update(entidad);
            return Ok(entidad); 
        }

        // DELETE api/<AdministradoresController>/5
        [HttpDelete("{id}")]
        public async void Delete(int id)
        {
            var serviciosAdministradores = new ServicioAdministradores(new RepositorioAdministradores(dataContext));
            await serviciosAdministradores.Delete(id);
        }
    }
}
