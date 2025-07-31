namespace TiendaVirtual.API.Models.DTOs
{
    public class ProductoDTO
    {
        public int Id { get; set; }

        public string NombreProducto { get; set; } = null!;

        public decimal PrecioVenta { get; set; }

        public int? IdCategoria { get; set; }

        public string? Imagen { get; set; }

        
    }
}
