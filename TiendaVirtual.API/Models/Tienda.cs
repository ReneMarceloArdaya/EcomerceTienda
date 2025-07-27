using System;
using System.Collections.Generic;

namespace TiendaVirtual.API.Models;

public partial class Tienda
{
    public int Id { get; set; }

    public string? NombreTienda { get; set; }

    public string? Direccion { get; set; }

    public string? Ciudad { get; set; }

    public decimal? Latitud { get; set; }

    public decimal? Longitud { get; set; }

    public virtual ICollection<Productotienda> Productotienda { get; set; } = new List<Productotienda>();
}
