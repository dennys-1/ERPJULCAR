// Models/OrdenesCompra.cs
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERPJULCAR.Models
{
public class OrdenesCompra
    {
        
        [Key]
        public int IdOrdenCompra { get; set; }
        public string Serie { get; set; }
        public DateTime FechaOrden { get; set; }

        public int IdProveedor { get; set; } // Clave foránea

        public string Estado { get; set; }

        // Propiedad de navegación
        [ForeignKey("IdProveedor")]
        public Proveedores proveedor { get; set; }  // Cambiado a "Proveedor" para alinearse
        public ICollection<OrdenesCompraDetalle> Detalles { get; set; }
    }
}
