namespace TiendaVirtual.Client.Models
{
    public class Producto
    {
        public int Id { get; set; }

        public string NombreProducto { get; set; } = null!;

        public decimal PrecioVenta { get; set; }

        public int? IdCategoria { get; set; }

        public string? Imagen { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        public List<ProductoStockInfo>? StocksEnTiendas { get; set; }
    }
}
