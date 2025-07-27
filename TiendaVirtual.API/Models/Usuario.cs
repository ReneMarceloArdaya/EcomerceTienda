using System;
using System.Collections.Generic;

namespace TiendaVirtual.API.Models;

public partial class Usuario
{
    public int Id { get; set; }

    public string NombreUsuario { get; set; } = null!;

    public string Correo { get; set; } = null!;

    public string Contrasena { get; set; } = null!;

    public string TipoUsuario { get; set; } = null!;

    public virtual ICollection<Carrito> Carritos { get; set; } = new List<Carrito>();

    public virtual ICollection<Cliente> Clientes { get; set; } = new List<Cliente>();

    public virtual ICollection<Direccionenvio> Direccionenvios { get; set; } = new List<Direccionenvio>();

    public virtual ICollection<Notificacionusuario> Notificacionusuarios { get; set; } = new List<Notificacionusuario>();
}
