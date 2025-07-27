namespace TiendaVirtual.API.Models.DTOs
{
    public class CarritoproductoDTO
    {
        public int Id { get; set; }

        public int? CarritoId { get; set; }

        public int? ProductoId { get; set; }

        public int? Cantidad { get; set; }

        public decimal? PrecioUnitario { get; set; }

    }
}
