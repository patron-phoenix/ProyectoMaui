using System.ComponentModel.DataAnnotations;

namespace ServidorPlatosMauiAPI.Models
{
    public class Platos
    {
        [Key]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public double Costo { get; set; }
        public string Ingredientes { get; set; }
    }
}
