namespace TiendaVirtual.API.Models.DTOs
{
    public class DireccionEnvioDTO
    {
        public int Id { get; set; }

        public int? UsuarioId { get; set; }

        public string? Direccion { get; set; }

        public string? Ciudad { get; set; }

        public string? Departamento { get; set; }

        public decimal? Latitud { get; set; }

        public decimal? Longitud { get; set; }
    }
}
