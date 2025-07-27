using System;
using System.Collections.Generic;

namespace TiendaVirtual.API.Models;

public partial class Carrito
{
    public int Id { get; set; }

    public int? UsuarioId { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public virtual ICollection<Carritoproducto> Carritoproductos { get; set; } = new List<Carritoproducto>();

    public virtual Usuario? Usuario { get; set; }
}
