using System;
using System.Collections.Generic;

namespace TiendaVirtual.API.Models;

public partial class Direccionenvio
{
    public int Id { get; set; }

    public int? UsuarioId { get; set; }

    public string? Direccion { get; set; }

    public string? Ciudad { get; set; }

    public string? Departamento { get; set; }

    public decimal? Latitud { get; set; }

    public decimal? Longitud { get; set; }

    public virtual ICollection<Pedido> Pedidos { get; set; } = new List<Pedido>();

    public virtual Usuario? Usuario { get; set; }
}
