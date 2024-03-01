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
    public class XesController : Controller
    {
        private readonly CarSalesContext _context;

        public XesController(CarSalesContext context)
        {
            _context = context;
        }

        // GET: Xes
        public async Task<IActionResult> Index()
        {
            var activeXe = await _context.Xes.Include(x => x.IdlxNavigation)
                .OrderByDescending(x=>x.Idxe)
                .Where(x=>x.Active==true)
                .ToListAsync();
            var hiddenXe = await _context.Xes.Include(x => x.IdlxNavigation)
                .OrderByDescending(x => x.Idxe)
                .Where(x => x.Active == false)
                .ToListAsync();
            ViewBag.ActiveXe = activeXe;
            ViewBag.HiddenXe = hiddenXe;
            TempData["Idlx"] = _context.Loaixes.Where(x => x.Active == true).ToList();
            return View();
        }

        // GET: Xes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Xes == null)
            {
                return NotFound();
            }

            var xe = await _context.Xes
                .Include(x => x.IdlxNavigation)
                .FirstOrDefaultAsync(m => m.Idxe == id);
            if (xe == null)
            {
                return NotFound();
            }

            return View(xe);
        }

        // GET: Xes/Create
        //public IActionResult Create()
        //{
        //    ViewData["Idlx"] = new SelectList(_context.Loaixes, "Idlx", "Idlx");
        //    return View();
        //}

        // POST: Xes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Idxe,Maxe,Tenxe,Idlx,Active")] Xe xe)
        {
            if (ModelState.IsValid)
            {
                xe.Active=true;
                _context.Add(xe);
                await _context.SaveChangesAsync();
                TempData["AlertSuccessMessage"] = xe.Maxe;
                return RedirectToAction(nameof(Index));
            }
            ViewData["Idlx"] = new SelectList(_context.Loaixes, "Idlx", "Idlx", xe.Idlx);
            return View(xe);
        }

        // GET: Xes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Xes == null)
            {
                return NotFound();
            }

            var xe = await _context.Xes.FindAsync(id);
            if (xe == null)
            {
                return NotFound();
            }
            TempData["Loaixe"] = _context.Loaixes.Where(x => x.Active == true).ToList();
            return View(xe);
        }

        // POST: Xes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Idxe,Maxe,Tenxe,Idlx,Active")] Xe xe)
        {
            if (id != xe.Idxe)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    
                    _context.Update(xe);
                    await _context.SaveChangesAsync();
                    TempData["AlertUpdateMessage"] = xe.Maxe;
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!XeExists(xe.Idxe))
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
            TempData["Loaixe"] = _context.Loaixes.Where(x => x.Active == true).ToList();
            return View(xe);
        }

        public async Task<IActionResult> Hidden(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var account = await _context.Xes
                .FirstOrDefaultAsync(m => m.Idxe == id);
            if (account == null)
            {
                return NotFound();
            }


            account.Active = false;

            await _context.SaveChangesAsync();
            TempData["AlertDeleteMessage"] = account.Maxe;
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Return(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var account = await _context.Xes
                .FirstOrDefaultAsync(m => m.Idxe == id);
            if (account == null)
            {
                return NotFound();
            }


            account.Active = true;

            await _context.SaveChangesAsync();
            TempData["AlertReturnMessage"] = account.Maxe;
            return RedirectToAction(nameof(Index));
        }

        // GET: Xes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Xes == null)
            {
                return NotFound();
            }

            var xe = await _context.Xes
                .Include(x => x.IdlxNavigation)
                .FirstOrDefaultAsync(m => m.Idxe == id);
            if (xe == null)
            {
                return NotFound();
            }

            return View(xe);
        }

        // POST: Xes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Xes == null)
            {
                return Problem("Entity set 'CarSalesContext.Xes'  is null.");
            }
            var xe = await _context.Xes.FindAsync(id);
            if (xe != null)
            {
                _context.Xes.Remove(xe);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool XeExists(int id)
        {
          return (_context.Xes?.Any(e => e.Idxe == id)).GetValueOrDefault();
        }
    }
}
