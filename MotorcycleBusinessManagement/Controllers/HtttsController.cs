using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MotorcycleBusinessManagement.Models;
using static System.Net.WebRequestMethods;

namespace MotorcycleBusinessManagement.Controllers
{
    public class HtttsController : Controller
    {
        private readonly CarSalesContext _context;

        public HtttsController(CarSalesContext context)
        {
            _context = context;
        }

        // GET: Httts
        public async Task<IActionResult> Index()
        {
            var activeHttt = await _context.Httts
             .Where(x => x.Active == true)
             .OrderByDescending(x => x.Idhttt)
             .ToListAsync();

            var hiddenHttt = await _context.Httts
                .Where(x => x.Active == false)
                .OrderByDescending(x => x.Idhttt)
                .ToListAsync();

            ViewBag.ActiveHttt = activeHttt;
            ViewBag.HiddenHttt = hiddenHttt;
            var httt = _context.Httts
                .OrderByDescending(i => i.Idhttt)
                .FirstOrDefault();
            
            if (httt != null)
            {
                int idtd = httt.Idhttt;
                if (idtd < 10)
                {
                    string idhttt = "HTTT000" + ++idtd;
                    TempData["GoiYHttt"] = idhttt;
                }
                else if (idtd < 100 && idtd >= 10)
                {
                    string idhttt = "HTTT00" + ++idtd;
                    TempData["GoiYHttt"] = idhttt;
                }
                else if (idtd < 1000 && idtd >= 100)
                {
                    string idhttt = "HTTT0" + ++idtd;
                    TempData["GoiYHttt"] = idhttt;
                }
                else
                {
                    string idhttt = "HTTT" + ++idtd;
                    TempData["GoiYHttt"] = idhttt;
                }

            }
            return View();
        }

        // GET: Httts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Httts == null)
            {
                return NotFound();
            }

            var httt = await _context.Httts
                .FirstOrDefaultAsync(m => m.Idhttt == id);
            if (httt == null)
            {
                return NotFound();
            }

            return View(httt);
        }

        // GET: Httts/Create
        //public IActionResult Create()
        //{
        //    return View();
        //}

        // POST: Httts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Idhttt,Mahttt,Tenhttt,Active")] Httt httt)
        {
            if (ModelState.IsValid)
            {
                httt.Active = true;
                _context.Add(httt);
                await _context.SaveChangesAsync();
                TempData["AlertSuccessMessage"] = httt.Mahttt;
                return RedirectToAction(nameof(Index));
            }
            return View(httt);
        }

        // GET: Httts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Httts == null)
            {
                return NotFound();
            }

            var httt = await _context.Httts.FindAsync(id);
            if (httt == null)
            {
                return NotFound();
            }
            return View(httt);
        }

        // POST: Httts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Idhttt,Mahttt,Tenhttt,Active")] Httt httt)
        {
            if (id != httt.Idhttt)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(httt);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HtttExists(httt.Idhttt))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                TempData["AlertUpdateMessage"] = httt.Mahttt;
                return RedirectToAction(nameof(Index));
            }
            return View(httt);
        }

        public async Task<IActionResult> Hidden(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var account = await _context.Httts
                .FirstOrDefaultAsync(m => m.Idhttt == id);
            if (account == null)
            {
                return NotFound();
            }


            account.Active = false;

            await _context.SaveChangesAsync();
            TempData["AlertDeleteMessage"] = account.Mahttt;
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Return(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var account = await _context.Httts
                .FirstOrDefaultAsync(m => m.Idhttt == id);
            if (account == null)
            {
                return NotFound();
            }


            account.Active = true;

            await _context.SaveChangesAsync();
            TempData["AlertReturnMessage"] = account.Mahttt;
            return RedirectToAction(nameof(Index));
        }

        // GET: Httts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Httts == null)
            {
                return NotFound();
            }

            var httt = await _context.Httts
                .FirstOrDefaultAsync(m => m.Idhttt == id);
            if (httt == null)
            {
                return NotFound();
            }

            return View(httt);
        }

        // POST: Httts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Httts == null)
            {
                return Problem("Entity set 'CarSalesContext.Httts'  is null.");
            }
            var httt = await _context.Httts.FindAsync(id);
            if (httt != null)
            {
                _context.Httts.Remove(httt);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HtttExists(int id)
        {
          return (_context.Httts?.Any(e => e.Idhttt == id)).GetValueOrDefault();
        }
    }
}
