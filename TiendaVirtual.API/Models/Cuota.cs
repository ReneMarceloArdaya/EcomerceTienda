using System;
using System.Collections.Generic;

namespace TiendaVirtual.API.Models;

public partial class Cuota
{
    public int Id { get; set; }

    public decimal Monto { get; set; }

    public decimal Interes { get; set; }

    public DateTime FechaPagoProgramado { get; set; }

    public DateTime? FechaPago { get; set; }

    public int? IdCredito { get; set; }

    public virtual Credito? IdCreditoNavigation { get; set; }
}
