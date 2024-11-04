using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BusReservation.Data;
using BusReservation.Models;

namespace BusReservation.Controllers
{
    public class ReservasController : Controller
    {
        private readonly BusReservationContext _context;

        public ReservasController(BusReservationContext context)
        {
            _context = context;
        }

        // GET: Reservas
        public async Task<IActionResult> Index()
        {
            var busReservationContext = _context.Reserva.Include(r => r.Cliente).Include(r => r.Ruta);
            return View(await busReservationContext.ToListAsync());
        }

        // GET: Reservas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reserva = await _context.Reserva
                .Include(r => r.Cliente)
                .Include(r => r.Ruta)
                .FirstOrDefaultAsync(m => m.ReservaId == id);
            if (reserva == null)
            {
                return NotFound();
            }

            return View(reserva);
        }

        // GET: Reservas/Create
        public IActionResult Create()
        {
            ViewData["ClienteId"] = new SelectList(_context.Cliente, "ClienteId", "Apellido");
            ViewData["RutaId"] = new SelectList(_context.Set<Ruta>(), "RutaId", "Destino");

            var asientosDisponibles = new List<SelectListItem>
            {
                new SelectListItem { Text = "A1", Value = "A1" },
                new SelectListItem { Text = "B1", Value = "B1" },
                new SelectListItem { Text = "A2", Value = "A2" },
                new SelectListItem { Text = "B2", Value = "B2" },
                new SelectListItem { Text = "A3", Value = "A3" },
                new SelectListItem { Text = "B3", Value = "B3" },
                new SelectListItem { Text = "A4", Value = "A4" },
                new SelectListItem { Text = "B4", Value = "B4" },
                new SelectListItem { Text = "A5", Value = "A5" },
                new SelectListItem { Text = "B5", Value = "B5" }
            };
            
            ViewBag.AsientosDisponibles = asientosDisponibles;
            return View();
        }

        // POST: Reservas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ReservaId,ClienteId,RutaId,FechaReserva,Asiento,EstadoReserva,MetodoPago,Precio")] Reserva reserva)
        {
            if (string.IsNullOrEmpty(reserva.Asiento))
            {
                ModelState.AddModelError("Asiento", "El asiento es obligatorio.");
            }
            if (ModelState.IsValid)
            {
                _context.Add(reserva);
                await _context.SaveChangesAsync();

                // Create a Boleto object when a Reserva is created
                var boleto = new Boleto
                {
                    ReservaId = reserva.ReservaId,
                    Detalles = "Reserva de Bus", 
                    CodigoQR = "QR code here", // Generate or set QR code
                    FechaEmision = DateTime.Now
                };

                _context.Add(boleto);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            ViewData["ClienteId"] = new SelectList(_context.Cliente, "ClienteId", "Apellido", reserva.ClienteId);
            ViewData["RutaId"] = new SelectList(_context.Set<Ruta>(), "RutaId", "Destino", reserva.RutaId);
            return View(reserva);
        }

        // GET: Reservas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reserva = await _context.Reserva.FindAsync(id);
            if (reserva == null)
            {
                return NotFound();
            }
            ViewData["ClienteId"] = new SelectList(_context.Cliente, "ClienteId", "Apellido", reserva.ClienteId);
            ViewData["RutaId"] = new SelectList(_context.Set<Ruta>(), "RutaId", "Destino", reserva.RutaId);
            
            var asientosDisponibles = new List<SelectListItem>
            {
                new SelectListItem { Text = "A1", Value = "A1" },
                new SelectListItem { Text = "B1", Value = "B1" },
                new SelectListItem { Text = "A2", Value = "A2" },
                new SelectListItem { Text = "B2", Value = "B2" },
                new SelectListItem { Text = "A3", Value = "A3" },
                new SelectListItem { Text = "B3", Value = "B3" },
                new SelectListItem { Text = "A4", Value = "A4" },
                new SelectListItem { Text = "B4", Value = "B4" },
                new SelectListItem { Text = "A5", Value = "A5" },
                new SelectListItem { Text = "B5", Value = "B5" }
            };
            ViewBag.AsientosDisponibles = asientosDisponibles;
            return View(reserva);
        }

        // POST: Reservas/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ReservaId,ClienteId,RutaId,FechaReserva,Asiento,EstadoReserva,MetodoPago,Precio")] Reserva reserva)
        {
            if (id != reserva.ReservaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reserva);
                    await _context.SaveChangesAsync();

                    // Update the corresponding Boleto object
                    var boleto = await _context.Boleto.FirstOrDefaultAsync(b => b.ReservaId == reserva.ReservaId);
                    if (boleto != null)
                    {
                        boleto.Detalles = "Updated details here"; // Update appropriate details
                        boleto.CodigoQR = "Updated QR code here"; // Update or regenerate QR code
                        boleto.FechaEmision = DateTime.Now;

                        _context.Update(boleto);
                        await _context.SaveChangesAsync();
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReservaExists(reserva.ReservaId))
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
            ViewData["ClienteId"] = new SelectList(_context.Cliente, "ClienteId", "Apellido", reserva.ClienteId);
            ViewData["RutaId"] = new SelectList(_context.Set<Ruta>(), "RutaId", "Destino", reserva.RutaId);
            return View(reserva);
        }

        // GET: Reservas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reserva = await _context.Reserva
                .Include(r => r.Cliente)
                .Include(r => r.Ruta)
                .FirstOrDefaultAsync(m => m.ReservaId == id);
            if (reserva == null)
            {
                return NotFound();
            }

            return View(reserva);
        }

        // POST: Reservas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reserva = await _context.Reserva.FindAsync(id);
            if (reserva != null)
            {
                var boleto = await _context.Boleto.FirstOrDefaultAsync(b => b.ReservaId == id);
                if (boleto != null)
                {
                    _context.Boleto.Remove(boleto);
                }

                _context.Reserva.Remove(reserva);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool ReservaExists(int id)
        {
            return _context.Reserva.Any(e => e.ReservaId == id);
        }
    }
}
