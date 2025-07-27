namespace TiendaVirtual.API.Models.DTOs
{
    public class DetallePedidoDTO
    {
        public int IdPedido { get; set; }

        public int IdProducto { get; set; }

        public float Cantidad { get; set; }

        public decimal Precio { get; set; }

    }
}
