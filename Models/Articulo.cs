// Models/Articulo.cs
using System.ComponentModel.DataAnnotations;

namespace ERPJULCAR.Models
{
public class Articulo
{
    [Key]
    public int IdArticulo { get; set; }
    public string CodigoBarra { get; set; }
    public string Descripcion { get; set; }
    public string UnidadMedida { get; set; }
    public decimal PrecioCompra { get; set; }
    public decimal PrecioVenta { get; set; }
}
}