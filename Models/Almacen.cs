using System.ComponentModel.DataAnnotations;

namespace ERPJULCAR.Models
{
public class Almacen
{
    [Key]
    public int Id { get; set; }
    public string Nombre { get; set; }
    public string Ubicacion { get; set; }
}
}