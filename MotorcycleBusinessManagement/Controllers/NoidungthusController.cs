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
    public class NoidungthusController : Controller
    {
        private readonly CarSalesContext _context;

        public NoidungthusController(CarSalesContext context)
        {
            _context = context;
        }

        // GET: Noidungthus
        public async Task<IActionResult> Index()
        {
            var carSalesContext = _context.Noidungthus.Include(n => n.IdctNavigation).Include(n => n.IdptNavigation);
            return View(await carSalesContext.ToListAsync());
        }

        // GET: Noidungthus/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Noidungthus == null)
            {
                return NotFound();
            }

            var noidungthu = await _context.Noidungthus
                .Include(n => n.IdctNavigation)
                .Include(n => n.IdptNavigation)
                .FirstOrDefaultAsync(m => m.Idndt == id);
            if (noidungthu == null)
            {
                return NotFound();
            }

            return View(noidungthu);
        }

        // GET: Noidungthus/Create
        public IActionResult Create()
        {
            ViewData["Idct"] = new SelectList(_context.Chitietxes, "Idct", "Idct");
            ViewData["Idpt"] = new SelectList(_context.Phieuthutienbanxes, "Idpt", "Idpt");
            return View();
        }

        // POST: Noidungthus/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Idndt,Idpt,Idct,Sotienthu,Biensoxe,Ghichu,Active")] Noidungthu noidungthu)
        {
            if (ModelState.IsValid)
            {
                _context.Add(noidungthu);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Idct"] = new SelectList(_context.Chitietxes, "Idct", "Idct", noidungthu.Idct);
            ViewData["Idpt"] = new SelectList(_context.Phieuthutienbanxes, "Idpt", "Idpt", noidungthu.Idpt);
            return View(noidungthu);
        }

        // GET: Noidungthus/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Noidungthus == null)
            {
                return NotFound();
            }

            var noidungthu = await _context.Noidungthus.FindAsync(id);
            if (noidungthu == null)
            {
                return NotFound();
            }
            ViewData["Idct"] = new SelectList(_context.Chitietxes, "Idct", "Idct", noidungthu.Idct);
            ViewData["Idpt"] = new SelectList(_context.Phieuthutienbanxes, "Idpt", "Idpt", noidungthu.Idpt);
            return View(noidungthu);
        }

        // POST: Noidungthus/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Idndt,Idpt,Idct,Sotienthu,Biensoxe,Ghichu,Active")] Noidungthu noidungthu)
        {
            if (id != noidungthu.Idndt)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(noidungthu);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NoidungthuExists(noidungthu.Idndt))
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
            ViewData["Idct"] = new SelectList(_context.Chitietxes, "Idct", "Idct", noidungthu.Idct);
            ViewData["Idpt"] = new SelectList(_context.Phieuthutienbanxes, "Idpt", "Idpt", noidungthu.Idpt);
            return View(noidungthu);
        }

        // GET: Noidungthus/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Noidungthus == null)
            {
                return NotFound();
            }

            var noidungthu = await _context.Noidungthus
                .Include(n => n.IdctNavigation)
                .Include(n => n.IdptNavigation)
                .FirstOrDefaultAsync(m => m.Idndt == id);
            if (noidungthu == null)
            {
                return NotFound();
            }

            return View(noidungthu);
        }

        // POST: Noidungthus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Noidungthus == null)
            {
                return Problem("Entity set 'CarSalesContext.Noidungthus'  is null.");
            }
            var noidungthu = await _context.Noidungthus.FindAsync(id);
            if (noidungthu != null)
            {
                _context.Noidungthus.Remove(noidungthu);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NoidungthuExists(int id)
        {
          return (_context.Noidungthus?.Any(e => e.Idndt == id)).GetValueOrDefault();
        }
    }
}
