using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcConciertos.Models
{
    public class CategoriaEvento
    {
        public int Idcategoria { get; set; }

        public string Nombre { get; set; }
    }
}
