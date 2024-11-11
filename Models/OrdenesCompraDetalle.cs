

using System.ComponentModel.DataAnnotations;

namespace ERPJULCAR.Models
{
  public class OrdenesCompraDetalle
    {
        [Key]
        public int IdDetalle { get; set; }
        public int IdOrdenCompra { get; set; } // Clave foránea
        public int IdArticulo { get; set; } // Clave foránea
        public decimal Cantidad { get; set; }
        public string UnidadMedida { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal MontoGravado { get; set; }
        public decimal IGV { get; set; }
        public decimal Total { get; set; }

        // Propiedad de navegación
        public OrdenesCompra OrdenCompra { get; set; }
        public Articulo Articulo { get; set; }
    }

}