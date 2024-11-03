using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BusReservation.Data;
using BusReservation.Models;

namespace BusReservation.Controllers
{
    public class RutasController : Controller
    {
        private readonly BusReservationContext _context;

        public RutasController(BusReservationContext context)
        {
            _context = context;
        }

        // GET: Rutas
        public async Task<IActionResult> Index()
        {
            return View(await _context.Ruta.ToListAsync());
        }

        // GET: Rutas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ruta = await _context.Ruta
                .FirstOrDefaultAsync(m => m.RutaId == id);
            if (ruta == null)
            {
                return NotFound();
            }

            return View(ruta);
        }

        // GET: Rutas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Rutas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RutaId,Origen,Destino,Duracion,FechaSalida,Hora")] Ruta ruta)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ruta);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(ruta);
        }

        // GET: Rutas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ruta = await _context.Ruta.FindAsync(id);
            if (ruta == null)
            {
                return NotFound();
            }
            return View(ruta);
        }

        // POST: Rutas/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RutaId,Origen,Destino,Duracion,FechaSalida,Hora")] Ruta ruta)
        {
            if (id != ruta.RutaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ruta);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RutaExists(ruta.RutaId))
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
            return View(ruta);
        }

        // GET: Rutas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ruta = await _context.Ruta
                .FirstOrDefaultAsync(m => m.RutaId == id);
            if (ruta == null)
            {
                return NotFound();
            }

            return View(ruta);
        }

        // POST: Rutas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ruta = await _context.Ruta.FindAsync(id);
            if (ruta != null)
            {
                _context.Ruta.Remove(ruta);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool RutaExists(int id)
        {
            return _context.Ruta.Any(e => e.RutaId == id);
        }
    }
}