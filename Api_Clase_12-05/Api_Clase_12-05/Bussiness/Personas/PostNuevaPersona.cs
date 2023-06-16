using Api_Clase_12_05.Data;
using Api_Clase_12_05.Models;
using Api_Clase_12_05.Resultados.Personas;
using FluentValidation;
using MediatR;
using static Api_Clase_12_05.Bussiness.Personas.GetByID_Business;

namespace Api_Clase_12_05.Bussiness.Personas
{
    public class PostNuevaPersona
    {
        public class Post_NuevaPersona : IRequest<ListadoPersonas>
        {
            public string Nombre { get; set; }
            public string Apellido { get; set; }
            public int IdCategoria { get; set; }

        }
        public class EjecutarValidacion : AbstractValidator<Post_NuevaPersona>
        {
            public EjecutarValidacion()
            {             
                RuleFor(x => x.Nombre).NotEmpty(); 
                RuleFor(x => x.Apellido).NotEmpty();
            }
        }
        public class Manejador : IRequestHandler<Post_NuevaPersona, ListadoPersonas>
        {
            private readonly ContextDB _contexto; 

            
            public Manejador(ContextDB contexto) //inyect
            {
                _contexto = contexto;
            }
            public async Task<ListadoPersonas> Handle(Post_NuevaPersona comando, CancellationToken cancellationToken)
            {
                var result = new ListadoPersonas();
                
                //validaciones

                var persona = new Persona
                {
                    Apellido = comando.Apellido,
                    Nombre = comando.Nombre,
                    FechaAlta = DateTime.Now,
                    IdCategoria = comando.IdCategoria
                };

                await _contexto.Personas.AddAsync(persona);
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

        }
    }
}
