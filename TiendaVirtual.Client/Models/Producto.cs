
using System.Text.Json.Serialization;

namespace TiendaVirtual.Client.Models
{
    public class Producto
    {
        public int Id { get; set; }

        public string NombreProducto { get; set; } = null!;

        public decimal PrecioVenta { get; set; }

        public int? IdCategoria { get; set; }

        public string? Imagen { get; set; }

        [JsonPropertyName("proveedorproductos")]
        public List<ProveedorProducto> Proveedorproductos { get; set; } = new();

        [System.Text.Json.Serialization.JsonIgnore]
        public List<Proveedor>? Proveedores { get; set; } = new List<Proveedor>();

        [System.Text.Json.Serialization.JsonIgnore]
        public List<ProductoStockInfo>? StocksEnTiendas { get; set; }
    }
}
