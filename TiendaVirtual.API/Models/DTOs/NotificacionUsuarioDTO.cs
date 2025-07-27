namespace TiendaVirtual.API.Models.DTOs
{
    public class NotificacionUsuarioDTO
    {
        public int Id { get; set; }

        public int? UsuarioId { get; set; }

        public string? Mensaje { get; set; }

        public DateTime? FechaEnvio { get; set; }

        public bool? Leido { get; set; }

    }
}
