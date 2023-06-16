using System.Net;
using Api_Clase_12_05.Bussiness.Personas;
using Api_Clase_12_05.Comandos.Personas;
using Api_Clase_12_05.Data;
using Api_Clase_12_05.Models;
using Api_Clase_12_05.Resultados.Personas;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api_Clase_12_05.Controllers
{
    [ApiController]
    public class PersonaController : ControllerBase
    {
        private readonly ContextDB _contexto;
        private readonly IMediator _mediator;

        public PersonaController(ContextDB contexto, IMediator mediator)
        {
            _contexto = contexto;
            _mediator = mediator;
        }

        [HttpGet]
        [Route("api/personas/getPersonas")]
        public async Task<ListadoPersonas> GetPersonas()
        {
            var result = new ListadoPersonas();
            var personas = await _contexto.Personas.ToListAsync();

            if (personas != null)
            {
                foreach (var item in personas)
                {
                    var itemPersona = new ItemPersona();
                    itemPersona.Id = item.Id;
                    itemPersona.Apellido = item.Apellido;
                    itemPersona.Nombre = item.Nombre;

                    result.ListPersonas.Add(itemPersona);
                }

                return result;
            }

            var mensajeError = "Personas no encontradas";
            result.SetMensajeError(mensajeError, HttpStatusCode.NotFound);
            return result;
        }

        [HttpGet]
        [Route("api/personas/getPersonaById/{id}")]
        public async Task<ListadoPersonas> GetPersonaById(int id)
        {

            var resultado = await _mediator.Send(new GetByID_Business.GetPersonaByIdComando
            {
                IdPersona = id
            });
            return resultado;

            //var result = new ListadoPersonas();

            //if (id == null || id <= 0)
            //{
            //    result.SetMensajeError("El parametro id es obligatorio", HttpStatusCode.BadRequest);
            //    return result;
            //}

            //var persona = await _contexto.Personas.Where(c => c.Id == id).Include(c=>c.Categoria).FirstOrDefaultAsync();

            //if (persona != null)
            //{
            //    var itemPersona = new ItemPersona
            //    {
            //        Apellido = persona.Apellido,
            //        Id = persona.Id,
            //        Nombre = persona.Nombre,
            //        NombreCategoria = persona.Categoria.Nombre
            //    };

            //    result.ListPersonas.Add(itemPersona);
            //    return result;
            //}

            //var mensajeError = "Persona con " + id.ToString() + " no encontrada";
            //result.SetMensajeError(mensajeError, HttpStatusCode.NotFound);

            //return result;
        }

        [HttpPost]
        [Route("api/personas/postNuevaPersona")]
        public async Task<ListadoPersonas> PostNuevaPersona([FromBody] NuevaPersona comando)
        {
            var resultado = await _mediator.Send(new PostNuevaPersona.Post_NuevaPersona
            {
                Nombre = comando.Nombre,
                Apellido = comando.Apellido,
                IdCategoria = comando.IdCategoria

            });
            return resultado;

            //var result = new ListadoPersonas();
            //// validaciones TODO

            //var persona = new Persona
            //{
            //    Apellido = comando.Apellido,
            //    Nombre = comando.Nombre,
            //    FechaAlta = DateTime.Now,
            //    IdCategoria = comando.IdCategoria
            //};

            //await _contexto.Personas.AddAsync(persona);
            //await _contexto.SaveChangesAsync();

            //var personaItem = new ItemPersona
            //{
            //    Apellido = persona.Apellido,
            //    Nombre = persona.Nombre,
            //    Id = persona.Id
            //};

            //result.ListPersonas.Add(personaItem);

            //return result;

        }
        
        [HttpPut]
        [Route("api/personas/putPersona")]
        public async Task<ListadoPersonas> PutPersona([FromBody] UpdatePersona comando)
        {
            var result = new ListadoPersonas();
            // validaciones TODO

            var persona = await _contexto.Personas.FirstOrDefaultAsync(c => c.Id == comando.Id);

            if (persona != null)
            {
                persona.Nombre = comando.Nombre;
                persona.Apellido = comando.Apellido;
                persona.IdCategoria = comando.IdCategoria;
                persona.FechaModificacion = DateTime.Now;

                _contexto.Update(persona);
                await _contexto.SaveChangesAsync();
                
                var personaItem = new ItemPersona
                {
                    Apellido = persona.Apellido,
                    Nombre = persona.Nombre,
                    Id = persona.Id
                };

                result.ListPersonas.Add(personaItem);

                return result;
            }
            else
            {
                result.SetMensajeError("persona no encontrada", HttpStatusCode.NotFound);
                return result;
            }
            
         
        }
    }




}