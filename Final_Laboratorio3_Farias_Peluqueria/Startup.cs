using Final_Laboratorio3_Farias_Peluqueria.Midlleware;
using Final_Laboratorio3_Farias_Peluqueria.Models;
using Final_Laboratorio3_Farias_Peluqueria.Repositorios;
using Final_Laboratorio3_Farias_Peluqueria.Repositorios.Implementaciones;
using Final_Laboratorio3_Farias_Peluqueria.Servicios;
using Final_Laboratorio3_Farias_Peluqueria.Servicios.Implementaciones;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Final_Laboratorio3_Farias_Peluqueria
{
    public class Startup
    {

        private readonly IConfiguration configuration;

        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {
                    Title = "Final Laboratorio3 - App Peluqueria Ariel",
                    Version = "v1",
                    Description = "Web Api para App en Android. Trabajo Final de Laboratorio 3",
                    Contact = new OpenApiContact
                    {
                        Name = "Genaro Farias",
                        Email = "gfsanluis78@gmail.com"
                    }
                });
                // hace un archivo donde va aguardar los comentarios de los controladores
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
                /* en cs.project tambien se pone la propiedad de grupo
                < PropertyGroup >
                    < GenerateDocumentationFile > true </ GenerateDocumentationFile >
                    < NoWarn >$(NoWarm); 1591 </ NoWarn >
                    </ PropertyGroup >
                */

            });
            // SOLO SI USA ENTITY FRAMEWORK:    //injeccion de dependecia contexto
            services.AddDbContext<DataContext>(
                options => options.UseSqlServer(
                    configuration["ConnectionStrings:DefaultConnection"]
                )
            );
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>//el sitio web valida con cookie
                {
                    options.LoginPath = "/Usuario/Login";
                    options.LogoutPath = "/Usuario/Logout";
                    options.AccessDeniedPath = "/Home/Restringido";
                })
                .AddJwtBearer(options =>//la api web valida con token
                {
                    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = configuration["TokenAuthentication:Issuer"],
                        ValidAudience = configuration["TokenAuthentication:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.ASCII.GetBytes(
                            configuration["TokenAuthentication:SecretKey"])),
                    };
                    // opción extra para usar el token el hub
                    options.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        {
                            // Read the token out of the query string
                            var accessToken = context.Request.Query["access_token"];
                            // If the request is for our hub...
                            var path = context.HttpContext.Request.Path;
                            if (!string.IsNullOrEmpty(accessToken) &&
                                path.StartsWithSegments("/chatsegurohub"))
                            {//reemplazar la url por la usada en la ruta ⬆
                                context.Token = accessToken;
                            }
                            return Task.CompletedTask;
                        }
                    };
                });
            services.AddAuthorization(options =>
            {
                //options.AddPolicy("Empleado", policy => policy.RequireClaim(ClaimTypes.Role, "Administrador", "Empleado"));
                options.AddPolicy("Administrador", policy => policy.RequireRole("Administrador", "SuperAdministrador"));
            });
            services.AddMvc();
            services.AddSignalR();//añade signalR

            // SOLO PARA INYECCIÓN DE DEPENDECIAS:
            /*
            Transient objects are always different; a new instance is provided to every controller and every service.
            Scoped objects are the same within a request, but different across different requests.
            Singleton objects are the same for every object and every request.
            */
            // services.AddTransient<IRepositorioBloques, RepositorioBloques>();
            // repositorios

            // Llamo a las referencias de interfaces
            InversionControl.AddInterfaces(services);
           
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                //app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Final_Laboratorio3_Farias_Peluqueria v1"));
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                    options.RoutePrefix = string.Empty;
                });

            }
            app.UseDeveloperExceptionPage();

            //Habilitar CORS
            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());
            //

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            // Permitir cookies
            app.UseCookiePolicy(new CookiePolicyOptions
            {
                MinimumSameSitePolicy = SameSiteMode.None,
            });

            // Habilitar autenticación
            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
               endpoints.MapControllers();
            });
        }
    }
}
