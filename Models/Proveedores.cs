// Models/Proveedor.cs
using System.ComponentModel.DataAnnotations;

namespace ERPJULCAR.Models
{
public class Proveedores
{
    [Key]
    public int IdProveedor { get; set; }
    public string NombreProveedor { get; set; }
    public string RUC { get; set; }
    public string Direccion { get; set; }
    public string Telefono { get; set; }
    public string Email { get; set; }
    public DateTime FechaRegistro { get; set; }
}
}