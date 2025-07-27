using System;
using System.Collections.Generic;

namespace TiendaVirtual.API.Models;

public partial class Juridico
{
    public string Id { get; set; } = null!;

    public string? RazonSocial { get; set; }

    public string? RepresentanteLegal { get; set; }

    public int ClienteId { get; set; }

    public virtual Cliente Cliente { get; set; } = null!;
}
