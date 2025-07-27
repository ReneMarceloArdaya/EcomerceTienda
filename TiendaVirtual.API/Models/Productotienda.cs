using System;
using System.Collections.Generic;

namespace TiendaVirtual.API.Models;

public partial class Productotienda
{
    public int IdProducto { get; set; }

    public int IdTienda { get; set; }

    public int Stock { get; set; }

    public virtual Producto IdProductoNavigation { get; set; } = null!;

    public virtual Tienda IdTiendaNavigation { get; set; } = null!;
}
