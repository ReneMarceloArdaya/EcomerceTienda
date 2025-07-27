namespace TiendaVirtual.API.Models.DTOs
{
    public class PedidoDTO
    {
        public int Id { get; set; }

        public DateTime FechaPedido { get; set; }

        public string? EstadoPedido { get; set; }

        public int? IdCliente { get; set; }

        public int? DireccionEnvioId { get; set; }

    }
}
