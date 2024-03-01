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
    public class KhachhangsController : Controller
    {
        private readonly CarSalesContext _context;

        public KhachhangsController(CarSalesContext context)
        {
            _context = context;
        }

        // GET: Khachhangs
        public async Task<IActionResult> Index()
        {
            var activeKhachhang = await _context.Khachhangs
           .Where(x => x.Active == true)
           .OrderByDescending(x => x.Idkh)
           .ToListAsync();

            var hiddenKhachhang = await _context.Khachhangs
                .Where(x => x.Active == false)
                .OrderByDescending(x => x.Idkh)
                .ToListAsync();

            ViewBag.ActiveKhachhang = activeKhachhang;
            ViewBag.HiddenKhachhang = hiddenKhachhang;
            var kh = _context.Khachhangs
                .OrderByDescending(i => i.Idkh)
                .FirstOrDefault();

            if (kh != null)
            {
                int idtc = kh.Idkh;
                if(kh.Idkh < 10)
                {
                    string idkh = "KH000" + ++idtc;
                    TempData["GoiYKH"] = idkh;
                }
                else if (idtc < 100 && idtc >= 10)
                {
                    string idkh = "KH00" + ++idtc;
                    TempData["GoiYKH"] = idkh;
                }
                else if(idtc <1000 && idtc >= 100)
                {
                    string idkh = "KH0" + ++idtc;
                    TempData["GoiYKH"] = idkh;
                }
                else
                {
                    string idkh = "KH" + ++idtc;
                    TempData["GoiYKH"] = idkh;
                }
                
            }

            return View();
        }

        // GET: Khachhangs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Khachhangs == null)
            {
                return NotFound();
            }

            var khachhang = await _context.Khachhangs
                .FirstOrDefaultAsync(m => m.Idkh == id);
            if (khachhang == null)
            {
                return NotFound();
            }

            return View(khachhang);
        }

        
        

        // POST: Khachhangs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Idkh,Makh,Tenkh,Diachikh,Dienthoaikh,Ngaysinh,Cccdkh,Emailkh,Quequankh,Masothuekh,Ghichu,Active")] Khachhang khachhang)
        {
            if (ModelState.IsValid)
            {
                khachhang.Active = true;
                _context.Add(khachhang);
                await _context.SaveChangesAsync();
                TempData["AlertSuccessMessage"] = khachhang.Makh;
                return RedirectToAction(nameof(Index));
            }
            return View(khachhang);
        }

        // GET: Khachhangs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Khachhangs == null)
            {
                return NotFound();
            }

            var khachhang = await _context.Khachhangs.FindAsync(id);
            if (khachhang == null)
            {
                return NotFound();
            }
            return View(khachhang);
        }

        // POST: Khachhangs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Idkh,Makh,Tenkh,Diachikh,Dienthoaikh,Ngaysinh,Cccdkh,Emailkh,Quequankh,Masothuekh,Ghichu,Active")] Khachhang khachhang)
        {
            if (id != khachhang.Idkh)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(khachhang);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KhachhangExists(khachhang.Idkh))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                TempData["AlertUpdateMessage"] = khachhang.Makh;
                return RedirectToAction(nameof(Index));
            }
            return View(khachhang);
        }


        public async Task<IActionResult> Hidden(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var account = await _context.Khachhangs
                .FirstOrDefaultAsync(m => m.Idkh == id);
            if (account == null)
            {
                return NotFound();
            }


            account.Active = false;

            await _context.SaveChangesAsync();
            TempData["AlertDeleteMessage"] = account.Makh;
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Return(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var account = await _context.Khachhangs
                .FirstOrDefaultAsync(m => m.Idkh == id);
            if (account == null)
            {
                return NotFound();
            }


            account.Active = true;

            await _context.SaveChangesAsync();
            TempData["AlertReturnMessage"] = account.Makh;
            return RedirectToAction(nameof(Index));
        }
        // GET: Khachhangs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Khachhangs == null)
            {
                return NotFound();
            }

            var khachhang = await _context.Khachhangs
                .FirstOrDefaultAsync(m => m.Idkh == id);
            if (khachhang == null)
            {
                return NotFound();
            }

            return View(khachhang);
        }

        // POST: Khachhangs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Khachhangs == null)
            {
                return Problem("Entity set 'CarSalesContext.Khachhangs'  is null.");
            }
            var khachhang = await _context.Khachhangs.FindAsync(id);
            if (khachhang != null)
            {
                _context.Khachhangs.Remove(khachhang);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KhachhangExists(int id)
        {
          return (_context.Khachhangs?.Any(e => e.Idkh == id)).GetValueOrDefault();
        }
    }
}
