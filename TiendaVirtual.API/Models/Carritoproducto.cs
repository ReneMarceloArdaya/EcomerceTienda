using System;
using System.Collections.Generic;

namespace TiendaVirtual.API.Models;

public partial class Carritoproducto
{
    public int Id { get; set; }

    public int? CarritoId { get; set; }

    public int? ProductoId { get; set; }

    public int? Cantidad { get; set; }

    public decimal? PrecioUnitario { get; set; }

    public virtual Carrito? Carrito { get; set; }

    public virtual Producto? Producto { get; set; }
}
