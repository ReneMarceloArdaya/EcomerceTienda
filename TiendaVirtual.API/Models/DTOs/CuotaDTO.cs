namespace TiendaVirtual.API.Models.DTOs
{
    public class CuotaDTO
    {
        public int Id { get; set; }

        public decimal Monto { get; set; }

        public decimal Interes { get; set; }

        public DateTime FechaPagoProgramado { get; set; }

        public DateTime? FechaPago { get; set; }

        public int? IdCredito { get; set; }

    }
}
