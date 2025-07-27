using System;
using System.Collections.Generic;

namespace TiendaVirtual.API.Models;

public partial class Proveedor
{
    public int Id { get; set; }

    public string NombreProveedor { get; set; } = null!;

    public string? Direccion { get; set; }

    public string? Telefono { get; set; }

    public virtual ICollection<Proveedorproducto> Proveedorproductos { get; set; } = new List<Proveedorproducto>();
}
