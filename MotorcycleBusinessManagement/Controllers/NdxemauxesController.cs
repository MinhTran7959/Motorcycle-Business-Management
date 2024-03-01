using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MotorcycleBusinessManagement.Models;

namespace MotorcycleBusinessManagement.Controllers
{
    public class NdxemauxesController : Controller
    {
        private readonly CarSalesContext _context;

        public NdxemauxesController(CarSalesContext context)
        {
            _context = context;
        }

        // GET: Ndxemauxes
        public async Task<IActionResult> Index()
        {
            var carSalesContext = _context.Ndxemauxes.Include(n => n.IdmxNavigation).Include(n => n.IdxeNavigation);
            return View(await carSalesContext.ToListAsync());
        }

        // GET: Ndxemauxes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Ndxemauxes == null)
            {
                return NotFound();
            }

            var ndxemauxe = await _context.Ndxemauxes
                .Include(n => n.IdmxNavigation)
                .Include(n => n.IdxeNavigation)
                .FirstOrDefaultAsync(m => m.Idxemau == id);
            if (ndxemauxe == null)
            {
                return NotFound();
            }

            return View(ndxemauxe);
        }

        // GET: Ndxemauxes/Create
        public IActionResult Create()
        {
            ViewData["Idmx"] = new SelectList(_context.Mauxes, "Idmx", "Idmx");
            ViewData["Idxe"] = new SelectList(_context.Xes, "Idxe", "Idxe");
            return View();
        }

        // POST: Ndxemauxes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Idxemau,Idmx,Idxe,Active")] Ndxemauxe ndxemauxe)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ndxemauxe);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Idmx"] = new SelectList(_context.Mauxes, "Idmx", "Idmx", ndxemauxe.Idmx);
            ViewData["Idxe"] = new SelectList(_context.Xes, "Idxe", "Idxe", ndxemauxe.Idxe);
            return View(ndxemauxe);
        }

        // GET: Ndxemauxes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Ndxemauxes == null)
            {
                return NotFound();
            }

            var ndxemauxe = await _context.Ndxemauxes.FindAsync(id);
            if (ndxemauxe == null)
            {
                return NotFound();
            }
            ViewData["Idmx"] = new SelectList(_context.Mauxes, "Idmx", "Idmx", ndxemauxe.Idmx);
            ViewData["Idxe"] = new SelectList(_context.Xes, "Idxe", "Idxe", ndxemauxe.Idxe);
            return View(ndxemauxe);
        }

        // POST: Ndxemauxes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Idxemau,Idmx,Idxe,Active")] Ndxemauxe ndxemauxe)
        {
            if (id != ndxemauxe.Idxemau)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ndxemauxe);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NdxemauxeExists(ndxemauxe.Idxemau))
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
            ViewData["Idmx"] = new SelectList(_context.Mauxes, "Idmx", "Idmx", ndxemauxe.Idmx);
            ViewData["Idxe"] = new SelectList(_context.Xes, "Idxe", "Idxe", ndxemauxe.Idxe);
            return View(ndxemauxe);
        }

        // GET: Ndxemauxes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Ndxemauxes == null)
            {
                return NotFound();
            }

            var ndxemauxe = await _context.Ndxemauxes
                .Include(n => n.IdmxNavigation)
                .Include(n => n.IdxeNavigation)
                .FirstOrDefaultAsync(m => m.Idxemau == id);
            if (ndxemauxe == null)
            {
                return NotFound();
            }

            return View(ndxemauxe);
        }

        // POST: Ndxemauxes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Ndxemauxes == null)
            {
                return Problem("Entity set 'CarSalesContext.Ndxemauxes'  is null.");
            }
            var ndxemauxe = await _context.Ndxemauxes.FindAsync(id);
            if (ndxemauxe != null)
            {
                _context.Ndxemauxes.Remove(ndxemauxe);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NdxemauxeExists(int id)
        {
          return (_context.Ndxemauxes?.Any(e => e.Idxemau == id)).GetValueOrDefault();
        }
    }
}
