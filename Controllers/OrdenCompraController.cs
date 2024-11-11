using Microsoft.AspNetCore.Mvc;
using ERPJULCAR.Models;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ERPJULCAR.Data;

namespace ERPJULCAR.Controllers
{
    public class OrdenCompraController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OrdenCompraController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Logistica/OrdenCompra
        public async Task<IActionResult> Index(string searchString)
{
    var ordenes = from o in _context.ordenescompra.Include(o => o.proveedor)
                  select o;

    if (!string.IsNullOrEmpty(searchString))
    {
        ordenes = ordenes.Where(o => o.Serie.Contains(searchString) || o.proveedor.NombreProveedor.Contains(searchString));
    }

    return View("~/Views/Logistica/OrdenCompra/Index.cshtml", await ordenes.ToListAsync());
}  // GET: Logistica/OrdenCompra/Create
       public IActionResult Create(string searchTerm)
{

    ViewBag.Proveedores = _context.proveedores
        .Select(p => new 
        {
            IdProveedor = p.IdProveedor,
            NombreProveedor = p.NombreProveedor ?? "N/A"  
        })
        .ToList();

   
    var articulos = string.IsNullOrEmpty(searchTerm)
        ? _context.Articulos.Select(a => new 
            {
                a.IdArticulo,
                Descripcion = a.Descripcion ?? "Sin Descripción",  // Manejo de NULL en Descripcion
                a.PrecioCompra
            }).ToList()
        : _context.Articulos.Where(a => a.Descripcion.Contains(searchTerm))
            .Select(a => new 
            {
                a.IdArticulo,
                Descripcion = a.Descripcion ?? "Sin Descripción",
                a.PrecioCompra
            }).ToList();

    ViewBag.Articulos = articulos;

    // Retornar la vista "Create" con un nuevo objeto OrdenesCompra
    return View("~/Views/Logistica/OrdenCompra/Create.cshtml", new OrdenesCompra());
}

        // Método para buscar productos por término de búsqueda (usado en el modal)
        [HttpGet]
        public IActionResult BuscarProductos(string term)
        {
            var productos = _context.Articulos
                .Where(p => p.Descripcion.Contains(term))
                .Select(p => new
                {
                    p.IdArticulo,
                    p.Descripcion,
                    p.PrecioCompra
                })
                .ToList();

            return Json(productos);
        }

        // POST: Logistica/OrdenCompra/Create
        [HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> Create(OrdenesCompra ordenCompra)
{
    if (ModelState.IsValid)
    {
        foreach (var detalle in ordenCompra.Detalles)
        {
            detalle.MontoGravado = detalle.Cantidad * detalle.PrecioUnitario;
            detalle.IGV = detalle.MontoGravado * 0.18m;
            detalle.Total = detalle.MontoGravado + detalle.IGV;
        }

        _context.Add(ordenCompra);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    // Manejar valores NULL en Proveedores para evitar excepciones al cargar la vista
    ViewBag.Proveedores = await _context.proveedores
        .Select(p => new 
        {
            IdProveedor = p.IdProveedor,
            NombreProveedor = p.NombreProveedor ?? "N/A" // Manejo de NULL en NombreProveedor
        })
        .ToListAsync();

    // Manejar valores NULL en Articulos
    ViewBag.Articulos = await _context.Articulos
        .Select(a => new 
        {
            a.IdArticulo,
            Descripcion = a.Descripcion ?? "Sin Descripción", // Manejo de NULL en Descripcion
            a.PrecioCompra
        })
        .ToListAsync();

    return View("~/Views/Logistica/OrdenCompra/Create.cshtml", ordenCompra);
}
        // GET: Logistica/OrdenCompra/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ordenCompra = await _context.ordenescompra.FindAsync(id);
            if (ordenCompra == null)
            {
                return NotFound();
            }
            return View("~/Views/Logistica/OrdenCompra/Edit.cshtml", ordenCompra);
        }

        // POST: Logistica/OrdenCompra/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, OrdenesCompra ordenCompra)
        {
            if (id != ordenCompra.IdOrdenCompra)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ordenCompra);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrdenCompraExists(ordenCompra.IdOrdenCompra))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View("~/Views/Logistica/OrdenCompra/Edit.cshtml", ordenCompra);
        }

        // GET: Logistica/OrdenCompra/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ordenCompra = await _context.ordenescompra
                .Include(o => o.proveedor)
                .FirstOrDefaultAsync(m => m.IdOrdenCompra == id);
            if (ordenCompra == null)
            {
                return NotFound();
            }

            return View("~/Views/Logistica/OrdenCompra/Delete.cshtml", ordenCompra);
        }

        // POST: Logistica/OrdenCompra/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ordenCompra = await _context.ordenescompra.FindAsync(id);
            _context.ordenescompra.Remove(ordenCompra);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrdenCompraExists(int id)
        {
            return _context.ordenescompra.Any(e => e.IdOrdenCompra == id);
        }
    }
}
