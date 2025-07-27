namespace TiendaVirtual.API.Models.DTOs
{
    public class ClienteDTO
    {
        public int Id { get; set; }

        public string? Direccion { get; set; }

        public string? Telefono { get; set; }

        public int? BarrioId { get; set; }

        public int? Edad { get; set; }

        public string? Genero { get; set; }

        public int? UsuarioId { get; set; }

        
    }
}
