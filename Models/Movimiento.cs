
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinTrack.Models
{
    public class Movimiento
    {
        public int Id { get; set; }

        [Required]
        [StringLength(150)]
        public string Concepto { get; set; } = string.Empty;

        [Range(
         typeof(decimal),
        "0.01",
        "999999999",
        ParseLimitsInInvariantCulture = true,
        ConvertValueInInvariantCulture = true,
        ErrorMessage = "El monto debe ser mayor o igual a 0.01."
        )]
        public decimal Monto { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Fecha { get; set; } = DateTime.Today;

        [Required(ErrorMessage = "Seleccione una categoría")]
        [Range(1, int.MaxValue,
            ErrorMessage = "Seleccione una categoría válida")]
        public int CategoriaId { get; set; }

        [ForeignKey(nameof(CategoriaId))]
        public Categoria? Categoria { get; set; }

        [StringLength(250)]
        public string? Descripcion { get; set; }
    }
}
