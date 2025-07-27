using System;
using System.Collections.Generic;

namespace TiendaVirtual.API.Models;

public partial class Barrio
{
    public int Id { get; set; }

    public string? NombreBarrio { get; set; }

    public int? ZonaId { get; set; }

    public virtual ICollection<Cliente> Clientes { get; set; } = new List<Cliente>();

    public virtual Zona? Zona { get; set; }
}
