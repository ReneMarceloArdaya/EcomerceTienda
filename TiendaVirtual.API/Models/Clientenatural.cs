using System;
using System.Collections.Generic;

namespace TiendaVirtual.API.Models;

public partial class Clientenatural
{
    public int Id { get; set; }

    public string NombreCompleto { get; set; } = null!;

    public string? ApellidoPaterno { get; set; }

    public string? ApellidoMaterno { get; set; }

    public int ClienteId { get; set; }

    public virtual Cliente Cliente { get; set; } = null!;
}
