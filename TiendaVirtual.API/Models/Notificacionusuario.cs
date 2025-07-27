using System;
using System.Collections.Generic;

namespace TiendaVirtual.API.Models;

public partial class Notificacionusuario
{
    public int Id { get; set; }

    public int? UsuarioId { get; set; }

    public string? Mensaje { get; set; }

    public DateTime? FechaEnvio { get; set; }

    public bool? Leido { get; set; }

    public virtual Usuario? Usuario { get; set; }
}
