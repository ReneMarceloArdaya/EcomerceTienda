namespace TiendaVirtual.API.Models.DTOs
{
    public class CreditoDTO
    {
        public int Id { get; set; }

        public decimal Monto { get; set; }

        public decimal InteresMensual { get; set; }

        public DateTime FechaDesembolso { get; set; }

        public int? IdPedido { get; set; }


    }
}
