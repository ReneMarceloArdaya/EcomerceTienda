using System.ComponentModel.DataAnnotations;

namespace TiendaVirtual.Client.Models
{
    public class ProveedorProductoViewModel
    {
        public int IdProveedor { get; set; }
        public int IdProducto { get; set; }

        // Propiedades del Producto (para mostrar en la subtabla)
        public string? NombreProducto { get; set; }
        public string? ImagenProducto { get; set; } 

        [Required(ErrorMessage = "El nombre específico es requerido")]
        [StringLength(50, ErrorMessage = "El nombre específico no puede exceder 50 caracteres")]
        public string? NombreEspecifico { get; set; }
    }
}
