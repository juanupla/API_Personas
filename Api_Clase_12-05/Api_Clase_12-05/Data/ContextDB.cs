using Api_Clase_12_05.Models;
using Microsoft.EntityFrameworkCore;

namespace Api_Clase_12_05.Data
{
    public class ContextDB : DbContext
    {
        public ContextDB(DbContextOptions<ContextDB> options) : base(options)
        { 

        }


        public DbSet<Persona> Personas { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
    }
}
