
using System.ComponentModel.DataAnnotations;

namespace FinTrack.Models
{
    public class Categoria
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(100)]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "El tipo es obligatorio")]
        [RegularExpression("^(Ingreso|Gasto)$",
            ErrorMessage = "El tipo debe ser Ingreso o Gasto")]
        public string Tipo { get; set; } = string.Empty;

        [StringLength(250)]
        public string? Descripcion { get; set; }

        public bool Activa { get; set; } = true;
    }
}
