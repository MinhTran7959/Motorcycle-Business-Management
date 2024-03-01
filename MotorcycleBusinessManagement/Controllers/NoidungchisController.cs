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
    public class NoidungchisController : Controller
    {
        private readonly CarSalesContext _context;

        public NoidungchisController(CarSalesContext context)
        {
            _context = context;
        }

        // GET: Noidungchis
        public async Task<IActionResult> Index()
        {
            var carSalesContext = _context.Noidungchis.Include(n => n.IdctNavigation).Include(n => n.IdpcNavigation);
            return View(await carSalesContext.ToListAsync());
        }

        // GET: Noidungchis/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Noidungchis == null)
            {
                return NotFound();
            }

            var noidungchi = await _context.Noidungchis
                .Include(n => n.IdctNavigation)
                .Include(n => n.IdpcNavigation)
                .FirstOrDefaultAsync(m => m.Idndc == id);
            if (noidungchi == null)
            {
                return NotFound();
            }

            return View(noidungchi);
        }

        // GET: Noidungchis/Create
        public IActionResult Create()
        {
            ViewData["Idct"] = new SelectList(_context.Chitietxes, "Idct", "Idct");
            ViewData["Idpc"] = new SelectList(_context.Phieuchitiennhapxes, "Idpc", "Idpc");
            return View();
        }

        // POST: Noidungchis/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Idndc,Idpc,Idct,Sotienchi,Ghichu,Active")] Noidungchi noidungchi)
        {
            if (ModelState.IsValid)
            {
                _context.Add(noidungchi);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Idct"] = new SelectList(_context.Chitietxes, "Idct", "Idct", noidungchi.Idct);
            ViewData["Idpc"] = new SelectList(_context.Phieuchitiennhapxes, "Idpc", "Idpc", noidungchi.Idpc);
            return View(noidungchi);
        }

        // GET: Noidungchis/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Noidungchis == null)
            {
                return NotFound();
            }

            var noidungchi = await _context.Noidungchis.FindAsync(id);
            if (noidungchi == null)
            {
                return NotFound();
            }
            ViewData["Idct"] = new SelectList(_context.Chitietxes, "Idct", "Idct", noidungchi.Idct);
            ViewData["Idpc"] = new SelectList(_context.Phieuchitiennhapxes, "Idpc", "Idpc", noidungchi.Idpc);
            return View(noidungchi);
        }

        // POST: Noidungchis/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Idndc,Idpc,Idct,Sotienchi,Ghichu,Active")] Noidungchi noidungchi)
        {
            if (id != noidungchi.Idndc)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(noidungchi);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NoidungchiExists(noidungchi.Idndc))
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
            ViewData["Idct"] = new SelectList(_context.Chitietxes, "Idct", "Idct", noidungchi.Idct);
            ViewData["Idpc"] = new SelectList(_context.Phieuchitiennhapxes, "Idpc", "Idpc", noidungchi.Idpc);
            return View(noidungchi);
        }

        // GET: Noidungchis/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Noidungchis == null)
            {
                return NotFound();
            }

            var noidungchi = await _context.Noidungchis
                .Include(n => n.IdctNavigation)
                .Include(n => n.IdpcNavigation)
                .FirstOrDefaultAsync(m => m.Idndc == id);
            if (noidungchi == null)
            {
                return NotFound();
            }

            return View(noidungchi);
        }

        // POST: Noidungchis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Noidungchis == null)
            {
                return Problem("Entity set 'CarSalesContext.Noidungchis'  is null.");
            }
            var noidungchi = await _context.Noidungchis.FindAsync(id);
            if (noidungchi != null)
            {
                _context.Noidungchis.Remove(noidungchi);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NoidungchiExists(int id)
        {
          return (_context.Noidungchis?.Any(e => e.Idndc == id)).GetValueOrDefault();
        }
    }
}
