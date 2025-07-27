namespace TiendaVirtual.API.Models.DTOs
{
    public class JuridicoDTO
    {
        public string Id { get; set; } = null!;

        public string? RazonSocial { get; set; }

        public string? RepresentanteLegal { get; set; }

        public int ClienteId { get; set; }

    }
}
