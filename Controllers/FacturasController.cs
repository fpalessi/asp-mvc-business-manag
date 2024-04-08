using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Rotativa.AspNetCore;
using SandraConfecciones.Models;

namespace SandraConfecciones.Controllers
{

    public class FacturasController : Controller
    {
        private readonly SandraContext _context;

        public FacturasController(SandraContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "Empleado, Admin")]
        public async Task<IActionResult> Index()
        {
            var sandraContext = _context.Facturas
                .Include(f => f.Cliente)
                .OrderBy(f => f.Fecha);
            return View(await sandraContext.ToListAsync());
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Print(int id)
        {
            var factura = _context.Facturas
                .Include(f => f.Cliente)
                .FirstOrDefault(m => m.FacturaId == id);

            if (factura == null)
            {
                return NotFound();
            }

            // Devolver el archivo PDF para su descarga
            return new ViewAsPdf("Print", factura)
            {
                FileName = $"Factura-Sandra-Confecciones-{id}.pdf",
                PageSize = Rotativa.AspNetCore.Options.Size.A4
            };
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var factura = await _context.Facturas
                .Include(f => f.Cliente)
                .FirstOrDefaultAsync(m => m.FacturaId == id);
            if (factura == null)
            {
                return NotFound();
            }

            return View(factura);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "ClienteId", "Nombre");
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FacturaId,ClienteId,Fecha,Total,Descripcion")] Factura factura)
        {
            if (ModelState.IsValid)
            {
                _context.Add(factura);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "ClienteId", "ClienteId", factura.ClienteId);
            return View(factura);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var factura = await _context.Facturas.FindAsync(id);
            if (factura == null)
            {
                return NotFound();
            }

            // Obtener la lista de clientes ordenada por nombre
            var clientes = _context.Clientes.OrderBy(c => c.Nombre).ToList();

            // Construir una lista de SelectListItem para el dropdown list
            ViewData["ClienteId"] = new SelectList(clientes, "ClienteId", "Nombre", factura.ClienteId);

            //ViewData["ClienteId"] = new SelectList(_context.Clientes, "ClienteId", "ClienteId", factura.ClienteId);
            return View(factura);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FacturaId,ClienteId,Fecha,Total,Descripcion")] Factura factura)
        {
            if (id != factura.FacturaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(factura);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FacturaExists(factura.FacturaId))
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
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "ClienteId", "ClienteId", factura.ClienteId);
            return View(factura);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var factura = await _context.Facturas
                .Include(f => f.Cliente)
                .FirstOrDefaultAsync(m => m.FacturaId == id);
            if (factura == null)
            {
                return NotFound();
            }

            return View(factura);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var factura = await _context.Facturas.FindAsync(id);
            if (factura != null)
            {
                _context.Facturas.Remove(factura);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FacturaExists(int id)
        {
            return _context.Facturas.Any(e => e.FacturaId == id);
        }
    }
}
