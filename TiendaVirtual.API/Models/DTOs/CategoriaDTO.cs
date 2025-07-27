namespace TiendaVirtual.API.Models.DTOs
{
    public class CategoriaDTO
    {
        public int Id { get; set; }

        public string NombreCategoria { get; set; } = null!;

        public string? Descripcion { get; set; }

    }
}
