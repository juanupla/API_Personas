using System.ComponentModel.DataAnnotations.Schema;

namespace Api_Clase_12_05.Models
{
   [Table("categorias")]
    public class Categoria
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
    }
}
