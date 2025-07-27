namespace TiendaVirtual.API.Models.DTOs
{
    public class FacturaDTO
    {
        public int Id { get; set; }

        public decimal Monto { get; set; }

        public DateTime FechaEmision { get; set; }

        public string CodigoControl { get; set; } = null!;

        public string Qr { get; set; } = null!;

        public int? IdPedido { get; set; }

    }
}
