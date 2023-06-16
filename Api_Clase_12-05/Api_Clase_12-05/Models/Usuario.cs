using System.ComponentModel.DataAnnotations.Schema;

namespace Api_Clase_12_05.Models
{
    [Table("usuarios")]
    public class Usuario
    {
        public int Id { get; set; }
        public string NombreUsu { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
