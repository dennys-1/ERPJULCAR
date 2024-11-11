// Models/ComprasDetalle.cs
using System.ComponentModel.DataAnnotations;

namespace ERPJULCAR.Models
{
public class ComprasDetalle
{
    [Key]
    public int IdDetalle { get; set; }
    public int IdCompra { get; set; } // Clave foránea
    public int IdArticulo { get; set; } // Clave foránea
    public decimal Cantidad { get; set; }
    public string UnidadMedida { get; set; }
    public decimal PrecioUnitario { get; set; }
    public decimal MontoGravado { get; set; }
    public decimal IGV { get; set; }
    public decimal Total { get; set; }
}}
