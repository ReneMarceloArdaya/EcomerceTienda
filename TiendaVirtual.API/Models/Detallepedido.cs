using System;
using System.Collections.Generic;

namespace TiendaVirtual.API.Models;

public partial class Detallepedido
{
    public int IdPedido { get; set; }

    public int IdProducto { get; set; }

    public float Cantidad { get; set; }

    public decimal Precio { get; set; }

    public virtual Pedido IdPedidoNavigation { get; set; } = null!;

    public virtual Producto IdProductoNavigation { get; set; } = null!;
}
