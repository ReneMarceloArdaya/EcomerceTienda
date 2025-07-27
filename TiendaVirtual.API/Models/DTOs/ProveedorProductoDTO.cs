namespace TiendaVirtual.API.Models.DTOs
{
    public class ProveedorProductoDTO
    {
        public int IdProveedor { get; set; }

        public int IdProducto { get; set; }

        public string NombreEspecifico { get; set; } = null!;
    }
}
