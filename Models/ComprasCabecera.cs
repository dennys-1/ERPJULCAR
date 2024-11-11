// Models/ComprasCabecera.cs
using System.ComponentModel.DataAnnotations;

namespace ERPJULCAR.Models
{
public class ComprasCabecera
{
    [Key]
    public int IdCompra { get; set; }
    public int IdProveedor { get; set; } // Clave foránea
    public DateTime FechaCompra { get; set; }
    public string AlmacenDestino { get; set; }
    public string Estado { get; set; }
    public string TipoPago { get; set; }
    public int? DiasCredito { get; set; } // Nullable para crédito
}
}