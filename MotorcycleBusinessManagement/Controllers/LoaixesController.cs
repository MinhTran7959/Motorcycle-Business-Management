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
    public class LoaixesController : Controller
    {
        private readonly CarSalesContext _context;

        public LoaixesController(CarSalesContext context)
        {
            _context = context;
        }

        // GET: Loaixes
        public async Task<IActionResult> Index()
        {
            var activeLoaixe = await _context.Loaixes.Where(x=>x.Active==true).OrderByDescending(x=>x.Idlx).ToListAsync();
            var hiddenLoaixe = await _context.Loaixes.Where(x => x.Active == false).OrderByDescending(x => x.Idlx).ToListAsync();
            ViewBag.activeLoaiXe = activeLoaixe;
            ViewBag.hiddenLoaiXe = hiddenLoaixe;
            var lx = _context.Loaixes
                .OrderByDescending(i => i.Idlx)
                .FirstOrDefault();

            if (lx != null)
            {
                int idtd = lx.Idlx;
                if (lx.Idlx < 10)
                {
                    string Idlx = "LXe000" + ++idtd;
                    TempData["GoiYLx"] = Idlx;
                }
                else if (lx.Idlx < 100 && lx.Idlx >= 10)
                {
                    string Idlx = "LXe00" + ++idtd;
                    TempData["GoiYLx"] = Idlx;
                }
                else if (lx.Idlx < 1000 && lx.Idlx >= 100)
                {
                    string Idlx = "LXe0" + ++idtd;
                    TempData["GoiYLx"] = Idlx;
                }
                else
                {
                    string Idlx = "LXe" + ++idtd;
                    TempData["GoiYLx"] = Idlx;
                }

            }
            return View();
        }

        // GET: Loaixes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Loaixes == null)
            {
                return NotFound();
            }

            var loaixe = await _context.Loaixes
                .FirstOrDefaultAsync(m => m.Idlx == id);
            if (loaixe == null)
            {
                return NotFound();
            }

            return View(loaixe);
        }

        // GET: Loaixes/Create
        //public IActionResult Create()
        //{
        //    return View();
        //}

        // POST: Loaixes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Idlx,Malx,Tenlx,Active")] Loaixe loaixe)
        {
            if (ModelState.IsValid)
            {
                loaixe.Active = true;
                _context.Add(loaixe);
                await _context.SaveChangesAsync();
                TempData["AlertSuccessMessage"] = loaixe.Malx;
                return RedirectToAction(nameof(Index));
            }
            return View(loaixe);
        }

        // GET: Loaixes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Loaixes == null)
            {
                return NotFound();
            }

            var loaixe = await _context.Loaixes.FindAsync(id);
            if (loaixe == null)
            {
                return NotFound();
            }
            return View(loaixe);
        }

        // POST: Loaixes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Idlx,Malx,Tenlx,Active")] Loaixe loaixe)
        {
            if (id != loaixe.Idlx)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(loaixe);
                    TempData["AlertUpdateMessage"] = loaixe.Malx;
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LoaixeExists(loaixe.Idlx))
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
            return View(loaixe);
        }

        public async Task<IActionResult> Hidden(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var account = await _context.Loaixes
                .FirstOrDefaultAsync(m => m.Idlx == id);
            if (account == null)
            {
                return NotFound();
            }


            account.Active = false;

            await _context.SaveChangesAsync();
            TempData["AlertDeleteMessage"] = account.Malx;
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Return(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var account = await _context.Loaixes
                .FirstOrDefaultAsync(m => m.Idlx == id);
            if (account == null)
            {
                return NotFound();
            }


            account.Active = true;

            await _context.SaveChangesAsync();
            TempData["AlertReturnMessage"] = account.Malx;
            return RedirectToAction(nameof(Index));
        }

        // GET: Loaixes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Loaixes == null)
            {
                return NotFound();
            }

            var loaixe = await _context.Loaixes
                .FirstOrDefaultAsync(m => m.Idlx == id);
            if (loaixe == null)
            {
                return NotFound();
            }

            return View(loaixe);
        }

        // POST: Loaixes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Loaixes == null)
            {
                return Problem("Entity set 'CarSalesContext.Loaixes'  is null.");
            }
            var loaixe = await _context.Loaixes.FindAsync(id);
            if (loaixe != null)
            {
                _context.Loaixes.Remove(loaixe);
            }
            TempData["AlertSuccessMessage"] = loaixe?.Malx;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LoaixeExists(int id)
        {
          return (_context.Loaixes?.Any(e => e.Idlx == id)).GetValueOrDefault();
        }
    }
}
