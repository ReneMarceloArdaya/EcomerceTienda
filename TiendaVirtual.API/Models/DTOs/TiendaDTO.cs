namespace TiendaVirtual.API.Models.DTOs
{
    public class TiendaDTO
    {
        public int Id { get; set; }

        public string? NombreTienda { get; set; }

        public string? Direccion { get; set; }

        public string? Ciudad { get; set; }

        public decimal? Latitud { get; set; }

        public decimal? Longitud { get; set; }
    }
}
