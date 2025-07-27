using System;
using System.Collections.Generic;

namespace TiendaVirtual.API.Models;

public partial class Credito
{
    public int Id { get; set; }

    public decimal Monto { get; set; }

    public decimal InteresMensual { get; set; }

    public DateTime FechaDesembolso { get; set; }

    public int? IdPedido { get; set; }

    public virtual ICollection<Cuota> Cuota { get; set; } = new List<Cuota>();

    public virtual Pedido? IdPedidoNavigation { get; set; }
}
