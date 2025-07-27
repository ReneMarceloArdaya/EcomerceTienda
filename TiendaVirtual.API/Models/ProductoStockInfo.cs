namespace TiendaVirtual.API.Models
{
    public class ProductoStockInfo
    {
        public int IdTienda { get; set; }
        public string NombreTienda { get; set; } = null!;
        public string Direccion { get; set; } = null!;
        public string Ciudad { get; set; } = null!;
        public int Stock { get; set; }
        public int IdProducto { get; set; }
    }
}
