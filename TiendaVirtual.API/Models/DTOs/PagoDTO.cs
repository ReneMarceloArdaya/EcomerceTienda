namespace TiendaVirtual.API.Models.DTOs
{
    public class PagoDTO
    {
        public int Id { get; set; }

        public int IdPedido { get; set; }

        public DateTime FechaPago { get; set; }

        public decimal Monto { get; set; }

        public string TipoPago { get; set; } = null!;

    }
}
