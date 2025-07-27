namespace TiendaVirtual.API.Models.DTOs
{
    public class ClienteNaturalDTO
    {
        public int Id { get; set; }

        public string NombreCompleto { get; set; } = null!;

        public string? ApellidoPaterno { get; set; }

        public string? ApellidoMaterno { get; set; }

        public int ClienteId { get; set; }

    }
}
