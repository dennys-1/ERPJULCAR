// Data/ApplicationDbContext.cs
using Microsoft.EntityFrameworkCore;
using ERPJULCAR.Models;
namespace ERPJULCAR.Data
{
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Proveedores> proveedores { get; set; }
    public DbSet<ComprasCabecera> OrdenesCompraCabecera { get; set; }
    public DbSet<ComprasDetalle> OrdenesCompraDetalle { get; set; }
    public DbSet<Articulo> Articulos { get; set; }
    public DbSet<Almacen> Almacenes { get; set; }
    public DbSet<OrdenesCompra> ordenescompra { get; set; }
    public DbSet<OrdenesCompraDetalle> ordenescompradetalle { get; set; }
}
}