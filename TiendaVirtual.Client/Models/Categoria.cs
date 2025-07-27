namespace TiendaVirtual.Client.Models
{
    public class Categoria
    {
        public int Id { get; set; }

        public string NombreCategoria { get; set; } = null!;

        public string? Descripcion { get; set; }
    }
}
