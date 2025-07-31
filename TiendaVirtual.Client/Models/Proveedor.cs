using System.ComponentModel.DataAnnotations;

namespace TiendaVirtual.Client.Models
{
    public class Proveedor
    {
        public int Id { get; set; }

        
        public string? NombreProveedor { get; set; }

   
        public string? Direccion { get; set; }

    
        public string? Telefono { get; set; }
    }
}
