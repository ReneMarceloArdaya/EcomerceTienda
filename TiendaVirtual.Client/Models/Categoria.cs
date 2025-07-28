using System.ComponentModel.DataAnnotations;

namespace TiendaVirtual.Client.Models
{
    public class Categoria
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre de la categoría es requerido")]
        [StringLength(90, ErrorMessage = "El nombre de la categoría no puede exceder 90 caracteres")]
        public string? NombreCategoria { get; set; }

        [StringLength(90, ErrorMessage = "La descripción no puede exceder 90 caracteres")]
        public string? Descripcion { get; set; }
    }
}
