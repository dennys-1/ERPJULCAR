using System.ComponentModel.DataAnnotations;

namespace ERPJULCAR.Models
{
    public class Usuario
    {
        [Key]
         public int IdUsuario { get; set; }
    public string NombreUsuario { get; set; }
    public string Contraseña { get; set; } // Almacenar contraseñas hasheadas
    public string Rol { get; set; }
    public DateTime FechaCreacion { get; set; }
    }
}