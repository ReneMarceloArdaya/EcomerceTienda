using System;
using System.Collections.Generic;

namespace TiendaVirtual.API.Models;

public partial class Producto
{
    public int Id { get; set; }

    public string NombreProducto { get; set; } = null!;

    public decimal PrecioVenta { get; set; }

    public int? IdCategoria { get; set; }

    public string? Imagen { get; set; }

    public virtual ICollection<Carritoproducto> Carritoproductos { get; set; } = new List<Carritoproducto>();

    public virtual ICollection<Detallepedido> Detallepedidos { get; set; } = new List<Detallepedido>();

    public virtual Categoria? IdCategoriaNavigation { get; set; }

    public virtual ICollection<Productotienda> Productotienda { get; set; } = new List<Productotienda>();

    public virtual ICollection<Proveedorproducto> Proveedorproductos { get; set; } = new List<Proveedorproducto>();
}
