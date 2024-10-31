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
    public class BoletosController : Controller
    {
        private readonly BusReservationContext _context;

        public BoletosController(BusReservationContext context)
        {
            _context = context;
        }

        // GET: Boletos
        public async Task<IActionResult> Index()
        {
            var busReservationContext = _context.Boleto.Include(b => b.Reserva);
            return View(await busReservationContext.ToListAsync());
        }

        // GET: Boletos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var boleto = await _context.Boleto
                .Include(b => b.Reserva)
                .FirstOrDefaultAsync(m => m.BoletoId == id);
            if (boleto == null)
            {
                return NotFound();
            }

            return View(boleto);
        }

        // GET: Boletos/Create
        public IActionResult Create()
        {
            ViewData["ReservaId"] = new SelectList(_context.Reserva, "ReservaId", "Asiento");
            return View();
        }

        // POST: Boletos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BoletoId,ReservaId,Detalles,CodigoQR,FechaEmision")] Boleto boleto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(boleto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ReservaId"] = new SelectList(_context.Reserva, "ReservaId", "Asiento", boleto.ReservaId);
            return View(boleto);
        }

        // GET: Boletos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var boleto = await _context.Boleto.FindAsync(id);
            if (boleto == null)
            {
                return NotFound();
            }
            ViewData["ReservaId"] = new SelectList(_context.Reserva, "ReservaId", "Asiento", boleto.ReservaId);
            return View(boleto);
        }

        // POST: Boletos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BoletoId,ReservaId,Detalles,CodigoQR,FechaEmision")] Boleto boleto)
        {
            if (id != boleto.BoletoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(boleto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BoletoExists(boleto.BoletoId))
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
            ViewData["ReservaId"] = new SelectList(_context.Reserva, "ReservaId", "Asiento", boleto.ReservaId);
            return View(boleto);
        }

        // GET: Boletos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var boleto = await _context.Boleto
                .Include(b => b.Reserva)
                .FirstOrDefaultAsync(m => m.BoletoId == id);
            if (boleto == null)
            {
                return NotFound();
            }

            return View(boleto);
        }

        // POST: Boletos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var boleto = await _context.Boleto.FindAsync(id);
            if (boleto != null)
            {
                _context.Boleto.Remove(boleto);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BoletoExists(int id)
        {
            return _context.Boleto.Any(e => e.BoletoId == id);
        }
    }
}
