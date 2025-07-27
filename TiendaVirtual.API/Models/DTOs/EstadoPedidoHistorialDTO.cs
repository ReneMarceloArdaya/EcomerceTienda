namespace TiendaVirtual.API.Models.DTOs
{
    public class EstadoPedidoHistorialDTO
    {
        public int Id { get; set; }

        public int? IdPedido { get; set; }

        public string? EstadoAnterior { get; set; }

        public string? EstadoNuevo { get; set; }

        public DateTime? FechaCambio { get; set; }

    }
}
