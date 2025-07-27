using System;
using System.Collections.Generic;

namespace TiendaVirtual.API.Models;

public partial class Zona
{
    public int Id { get; set; }

    public string NombreZona { get; set; } = null!;

    public virtual ICollection<Barrio> Barrios { get; set; } = new List<Barrio>();
}
