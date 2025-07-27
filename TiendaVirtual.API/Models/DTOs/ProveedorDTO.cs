namespace TiendaVirtual.API.Models.DTOs
{
    public class ProveedorDTO
    {
        public int Id { get; set; }

        public string NombreProveedor { get; set; } = null!;

        public string? Direccion { get; set; }

        public string? Telefono { get; set; }
    }
}
