using Api_Clase_12_05.Data;
using Api_Clase_12_05.Resultados.Personas;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Net;
using static Api_Clase_12_05.Bussiness.Personas.GetByID_Business;

namespace Api_Clase_12_05.Bussiness.Personas
{
    public class GetByID_Business
    {
        public class GetPersonaByIdComando: IRequest<ListadoPersonas>
        {
            public int IdPersona { get; set; }
        }
        public class EjecutarValidacion : AbstractValidator<GetPersonaByIdComando>
        {
            public EjecutarValidacion()
            {
                RuleFor(x => x.IdPersona).NotEmpty().WithMessage("Debe ingresar un id"); //no debe ser nulo
                
            }
        }
        
        public class Manejador : IRequestHandler<GetPersonaByIdComando, ListadoPersonas> // logica de negocio
        {
            

            private readonly ContextDB _contexto; 
            private readonly IValidator<GetPersonaByIdComando> _validator;

            //ctor
            public Manejador(ContextDB contexto, IValidator<GetPersonaByIdComando> validator) // para conectar la base de dato
            {
                _contexto = contexto;
                _validator = validator;
            }

            //metodo de la logica de negocio

            public async Task<ListadoPersonas> Handle(GetPersonaByIdComando comando, CancellationToken cancellationToken)
            {
                var result = new ListadoPersonas();

                var validation = await _validator.ValidateAsync(comando);

                
                if(!validation.IsValid) 
                {
                    var errors = string.Join(Environment.NewLine, validation.Errors);
                    result.SetMensajeError(errors, HttpStatusCode.InternalServerError);
                    return result;
                }

                if (comando.IdPersona == null || comando.IdPersona <= 0)
                {
                    result.SetMensajeError("El parametro id es obligatorio", HttpStatusCode.BadRequest);
                    return result;
                }

                var persona = await _contexto.Personas.Where(c => c.Id == comando.IdPersona).Include(c => c.Categoria).FirstOrDefaultAsync();

                if (persona != null)
                {
                    var itemPersona = new ItemPersona
                    {
                        Apellido = persona.Apellido,
                        Id = persona.Id,
                        Nombre = persona.Nombre,
                        NombreCategoria = persona.Categoria.Nombre
                    };

                    result.ListPersonas.Add(itemPersona);
                    return result;
                }

                var mensajeError = "Persona con " + comando.IdPersona.ToString() + " no encontrada";
                result.SetMensajeError(mensajeError, HttpStatusCode.NotFound);

                return result;
            }


        }

    }

   
}
