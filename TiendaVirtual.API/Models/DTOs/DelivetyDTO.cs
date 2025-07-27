namespace TiendaVirtual.API.Models.DTOs
{
    public class DelivetyDTO
    {
        public int Id { get; set; }

        public int? PedidoId { get; set; }

        public string Nombre { get; set; } = null!;

        public string Telefono { get; set; } = null!;

        public string? TelefonoDelyvery { get; set; }
    }
}
