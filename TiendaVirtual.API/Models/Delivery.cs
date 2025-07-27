using System;
using System.Collections.Generic;

namespace TiendaVirtual.API.Models;

public partial class Delivery
{
    public int Id { get; set; }

    public int? PedidoId { get; set; }

    public string Nombre { get; set; } = null!;

    public string Telefono { get; set; } = null!;

    public string? TelefonoDelyvery { get; set; }

    public virtual Pedido? Pedido { get; set; }
}
