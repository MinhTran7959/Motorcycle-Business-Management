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
    public class MauxesController : Controller
    {
        private readonly CarSalesContext _context;

        public MauxesController(CarSalesContext context)
        {
            _context = context;
        }

        // GET: Mauxes
        public async Task<IActionResult> Index()
        {
            var activeMauxe = await _context.Mauxes.Where(x => x.Active == true).OrderByDescending(x => x.Idmx).ToListAsync();
            var hiddenMauxe = await _context.Mauxes.Where(x => x.Active == false).OrderByDescending(x => x.Idmx).ToListAsync();
            var Maucuaxe = await _context.Ndxemauxes.Where(x=>x.Active==true&&x.Idmx!=null&&x.Idxe!=null).OrderByDescending(x => x.Idxemau).ToListAsync();
            ViewBag.activeMauXe = activeMauxe;
            ViewBag.hiddenMauXe = hiddenMauxe;
            ViewBag.Maucuaxe = Maucuaxe;
            TempData["Tenxe"] = _context.Xes.Where(x => x.Active == true).ToList();
            TempData["Mauxe"] = _context.Mauxes.Where(x => x.Active == true).ToList();
            
            return View();
        }

        // GET: Mauxes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Mauxes == null)
            {
                return NotFound();
            }

            var mauxe = await _context.Mauxes
                .FirstOrDefaultAsync(m => m.Idmx == id);
            if (mauxe == null)
            {
                return NotFound();
            }

            return View(mauxe);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateMauCuaXe([Bind("Idxemau,Idmx,Idxe,Active")] Ndxemauxe ndxemauxe)
        {
            if (ModelState.IsValid)
            {
                ndxemauxe.Active = true;
                _context.Add(ndxemauxe);
                await _context.SaveChangesAsync();
                TempData["AlertSuccessMessage"] = ndxemauxe.IdxeNavigation?.Maxe +" "+ ndxemauxe.IdmxNavigation?.Mamx ;
                return RedirectToAction(nameof(Index));
            }
            ViewData["Idmx"] = new SelectList(_context.Mauxes, "Idmx", "Idmx", ndxemauxe.Idmx);
            ViewData["Idxe"] = new SelectList(_context.Xes, "Idxe", "Idxe", ndxemauxe.Idxe);
            return View(ndxemauxe);
        }
        public async Task<IActionResult> EditMauCuaXe(int? id)
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
            TempData["Tenxe"] = _context.Xes.Where(x => x.Active == true).ToList();
            TempData["Mauxe"] = _context.Mauxes.Where(x => x.Active == true).ToList();
            return View(ndxemauxe);
        }

        // POST: Ndxemauxes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditMauCuaXe(int id, [Bind("Idxemau,Idmx,Idxe,Active")] Ndxemauxe ndxemauxe)
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
                    TempData["AlertUpdateMessage"] = ndxemauxe.IdxeNavigation?.Maxe + " " + ndxemauxe.IdmxNavigation?.Mamx;
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (ndxemauxe == null)
                    {
                        return NotFound();
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["Idmx"] = new SelectList(_context.Mauxes, "Idmx", "Idmx", ndxemauxe.Idmx);
            ViewData["Idxe"] = new SelectList(_context.Xes, "Idxe", "Idxe", ndxemauxe.Idxe);
            return View(ndxemauxe);
        }
        public async Task<IActionResult> HiddenMauCuaXe(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var account = await _context.Ndxemauxes
                .FirstOrDefaultAsync(m => m.Idxemau == id);
            if (account == null)
            {
                return NotFound();
            }


            account.Active = false;

            await _context.SaveChangesAsync();
            TempData["AlertDeleteMessage"] = "";
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateMauXe([Bind("Idmx,Mamx,Tenmx,Active")] Mauxe mauxe)
        {
            if (ModelState.IsValid)
            {
                mauxe.Active= true;
                _context.Add(mauxe);
                await _context.SaveChangesAsync();
                TempData["AlertSuccessMessage"] = mauxe.Mamx;
                return RedirectToAction(nameof(Index));
            }
            return View(mauxe);
        }

        // GET: Mauxes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Mauxes == null)
            {
                return NotFound();
            }

            var mauxe = await _context.Mauxes.FindAsync(id);
            if (mauxe == null)
            {
                return NotFound();
            }
            return View(mauxe);
        }

        // POST: Mauxes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Idmx,Mamx,Tenmx,Active")] Mauxe mauxe)
        {
            if (id != mauxe.Idmx)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mauxe);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MauxeExists(mauxe.Idmx))
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
            return View(mauxe);
        }

        public async Task<IActionResult> Hidden(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var account = await _context.Mauxes
                .FirstOrDefaultAsync(m => m.Idmx == id);
            if (account == null)
            {
                return NotFound();
            }


            account.Active = false;

            await _context.SaveChangesAsync();
            TempData["AlertDeleteMessage"] = account.Mamx;
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Return(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var account = await _context.Mauxes
                .FirstOrDefaultAsync(m => m.Idmx == id);
            if (account == null)
            {
                return NotFound();
            }


            account.Active = true;

            await _context.SaveChangesAsync();
            TempData["AlertReturnMessage"] = account.Mamx;
            return RedirectToAction(nameof(Index));
        }
        // GET: Mauxes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Mauxes == null)
            {
                return NotFound();
            }

            var mauxe = await _context.Mauxes
                .FirstOrDefaultAsync(m => m.Idmx == id);
            if (mauxe == null)
            {
                return NotFound();
            }

            return View(mauxe);
        }

        // POST: Mauxes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Mauxes == null)
            {
                return Problem("Entity set 'CarSalesContext.Mauxes'  is null.");
            }
            var mauxe = await _context.Mauxes.FindAsync(id);
            if (mauxe != null)
            {
                _context.Mauxes.Remove(mauxe);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MauxeExists(int id)
        {
          return (_context.Mauxes?.Any(e => e.Idmx == id)).GetValueOrDefault();
        }
    }
}
