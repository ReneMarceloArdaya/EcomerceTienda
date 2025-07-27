namespace TiendaVirtual.API.Models.DTOs
{
    public class CarritoDTO
    {
        public int Id { get; set; }

        public int? UsuarioId { get; set; }

        public DateTime? FechaCreacion { get; set; }
    }
}
