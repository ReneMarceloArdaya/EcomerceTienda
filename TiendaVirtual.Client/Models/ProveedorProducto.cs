namespace TiendaVirtual.Client.Models
{
    public class ProveedorProducto
    {
        public int idProveedor { get; set; }

        public int idProducto { get; set; }

        public string nombreEspecifico { get; set; } = null!;

       
        public Proveedor? idProveedorNavigation { get; set; }


    }
}
