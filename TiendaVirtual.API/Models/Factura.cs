using System;
using System.Collections.Generic;

namespace TiendaVirtual.API.Models;

public partial class Factura
{
    public int Id { get; set; }

    public decimal Monto { get; set; }

    public DateTime FechaEmision { get; set; }

    public string CodigoControl { get; set; } = null!;

    public string Qr { get; set; } = null!;

    public int? IdPedido { get; set; }

    public virtual Pedido? IdPedidoNavigation { get; set; }
}
