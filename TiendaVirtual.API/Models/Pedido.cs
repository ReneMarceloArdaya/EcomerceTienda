using System;
using System.Collections.Generic;

namespace TiendaVirtual.API.Models;

public partial class Pedido
{
    public int Id { get; set; }

    public DateTime FechaPedido { get; set; }

    public string? EstadoPedido { get; set; }

    public int? IdCliente { get; set; }

    public int? DireccionEnvioId { get; set; }

    public virtual ICollection<Credito> Creditos { get; set; } = new List<Credito>();

    public virtual ICollection<Delivery> Deliveries { get; set; } = new List<Delivery>();

    public virtual ICollection<Detallepedido> Detallepedidos { get; set; } = new List<Detallepedido>();

    public virtual Direccionenvio? DireccionEnvio { get; set; }

    public virtual ICollection<Estadopedidohistorial> Estadopedidohistorials { get; set; } = new List<Estadopedidohistorial>();

    public virtual ICollection<Factura> Facturas { get; set; } = new List<Factura>();

    public virtual Cliente? IdClienteNavigation { get; set; }

    public virtual ICollection<Pago> Pagos { get; set; } = new List<Pago>();
}
