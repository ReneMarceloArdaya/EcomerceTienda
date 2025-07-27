using System;
using System.Collections.Generic;

namespace TiendaVirtual.API.Models;

public partial class Proveedorproducto
{
    public int IdProveedor { get; set; }

    public int IdProducto { get; set; }

    public string NombreEspecifico { get; set; } = null!;

    public virtual Producto IdProductoNavigation { get; set; } = null!;

    public virtual Proveedor IdProveedorNavigation { get; set; } = null!;
}
