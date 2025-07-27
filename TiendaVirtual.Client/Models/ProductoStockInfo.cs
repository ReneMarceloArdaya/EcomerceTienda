namespace TiendaVirtual.Client.Models
{
    public class ProductoStockInfo
    {
        public int IdTienda { get; set; }
        public string NombreTienda { get; set; }
        public string Direccion { get; set; }
        public string Ciudad { get; set; }
        public int Stock { get; set; }
        public int IdProducto { get; set; }
    }
}
