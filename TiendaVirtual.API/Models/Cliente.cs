using System;
using System.Collections.Generic;

namespace TiendaVirtual.API.Models;

public partial class Cliente
{
    public int Id { get; set; }

    public string? Direccion { get; set; }

    public string? Telefono { get; set; }

    public int? BarrioId { get; set; }

    public int? Edad { get; set; }

    public string? Genero { get; set; }

    public int? UsuarioId { get; set; }

    public virtual Barrio? Barrio { get; set; }

    public virtual Clientenatural? Clientenatural { get; set; }

    public virtual Juridico? Juridico { get; set; }

    public virtual ICollection<Pedido> Pedidos { get; set; } = new List<Pedido>();

    public virtual Usuario? Usuario { get; set; }
}
