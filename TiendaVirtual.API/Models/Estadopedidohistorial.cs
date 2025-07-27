using System;
using System.Collections.Generic;

namespace TiendaVirtual.API.Models;

public partial class Estadopedidohistorial
{
    public int Id { get; set; }

    public int? IdPedido { get; set; }

    public string? EstadoAnterior { get; set; }

    public string? EstadoNuevo { get; set; }

    public DateTime? FechaCambio { get; set; }

    public virtual Pedido? IdPedidoNavigation { get; set; }
}
