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
    public class NhacungcapsController : Controller
    {
        private readonly CarSalesContext _context;

        public NhacungcapsController(CarSalesContext context)
        {
            _context = context;
        }

        // GET: Nhacungcaps
        public async Task<IActionResult> Index()
        {
            var activeNhaCungCap = await _context.Nhacungcaps.Where(x=>x.Active == true)
                .OrderByDescending(x=>x.Idncc).ToListAsync();
            var hiddenNhaCungCap = await _context.Nhacungcaps.Where(x => x.Active == false)
                .OrderByDescending(x => x.Idncc).ToListAsync();
            ViewBag.ActiveNhaCungCap = activeNhaCungCap;
            ViewBag.HiddenNhaCungCap = hiddenNhaCungCap;
            var ncc = _context.Nhacungcaps
                .OrderByDescending(i => i.Idncc)
                .FirstOrDefault();

            if (ncc != null)
            {
                int idtd = ncc.Idncc;
                if (ncc.Idncc < 10)
                {
                    string Idncc = "NCC000" + ++idtd;
                    TempData["GoiYNcc"] = Idncc;
                }
                else if (ncc.Idncc < 100 && ncc.Idncc >= 10)
                {
                    string Idncc = "NCC00" + ++idtd;
                    TempData["GoiYNcc"] = Idncc;
                }
                else if (ncc.Idncc < 1000 && ncc.Idncc >= 100)
                {
                    string Idncc = "NCC0" + ++idtd;
                    TempData["GoiYNcc"] = Idncc;
                }
                else
                {
                    string Idncc = "NCC" + ++idtd;
                    TempData["GoiYNcc"] = Idncc;
                }

            }
            return View();
        }
        private string TaoSoPhieu()
        {
            // Lấy ngày tháng năm hiện tại
            string ngayThangNam = DateTime.Now.ToString("ddMMyy");

            // Kiểm tra xem có phiếu cùng ngày không
            var MaCu = _context.Nhacungcaps.OrderByDescending(x => x.Mancc).FirstOrDefault(p => p.Mancc.StartsWith($"NCC{ngayThangNam}"));

            if (MaCu != null)
            {
                // Tách lấy số phiếu hiện tại
                string soPhieuCu = MaCu.Mancc.Substring(8);
                int soPhieuMoi = int.Parse(soPhieuCu) + 1;
                return $"NCC{ngayThangNam}{soPhieuMoi:D4}";
            }
            else
            {
                // Nếu không có phiếu cùng ngày, bắt đầu từ 1
                return $"NCC{ngayThangNam}0001";
            }
        }
        // GET: Nhacungcaps/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Nhacungcaps == null)
            {
                return NotFound();
            }

            var nhacungcap = await _context.Nhacungcaps
                .FirstOrDefaultAsync(m => m.Idncc == id);
            if (nhacungcap == null)
            {
                return NotFound();
            }

            return View(nhacungcap);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Idncc,Mancc,Tenncc,Diachincc,Dienthoaincc,Cccdncc,Email,Quequan,Masothuencc,Ghichu,Active")] Nhacungcap nhacungcap)
        {
            if (ModelState.IsValid)
            {
                nhacungcap.Active = true;
                _context.Add(nhacungcap);
                await _context.SaveChangesAsync();
                TempData["AlertSuccessMessage"] = nhacungcap.Mancc;
                return RedirectToAction(nameof(Index));
            }
            return View(nhacungcap);
        }

        // GET: Nhacungcaps/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Nhacungcaps == null)
            {
                return NotFound();
            }

            var nhacungcap = await _context.Nhacungcaps.FindAsync(id);
            if (nhacungcap == null)
            {
                return NotFound();
            }
            return View(nhacungcap);
        }

        // POST: Nhacungcaps/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Idncc,Mancc,Tenncc,Diachincc,Dienthoaincc,Cccdncc,Email,Quequan,Masothuencc,Ghichu,Active")] Nhacungcap nhacungcap)
        {
            if (id != nhacungcap.Idncc)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(nhacungcap);
                    await _context.SaveChangesAsync();
                    TempData["AlertUpdateMessage"] = nhacungcap.Mancc;
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NhacungcapExists(nhacungcap.Idncc))
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
            return View(nhacungcap);
        }

        public async Task<IActionResult> Hidden(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var account = await _context.Nhacungcaps
                .FirstOrDefaultAsync(m => m.Idncc == id);
            if (account == null)
            {
                return NotFound();
            }


            account.Active = false;

            await _context.SaveChangesAsync();
            TempData["AlertDeleteMessage"] = account.Mancc;
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Return(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var account = await _context.Nhacungcaps
                .FirstOrDefaultAsync(m => m.Idncc == id);
            if (account == null)
            {
                return NotFound();
            }


            account.Active = true;

            await _context.SaveChangesAsync();
            TempData["AlertReturnMessage"] = account.Mancc;
            return RedirectToAction(nameof(Index));
        }

        // GET: Nhacungcaps/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Nhacungcaps == null)
            {
                return NotFound();
            }

            var nhacungcap = await _context.Nhacungcaps
                .FirstOrDefaultAsync(m => m.Idncc == id);
            if (nhacungcap == null)
            {
                return NotFound();
            }

            return View(nhacungcap);
        }

        // POST: Nhacungcaps/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Nhacungcaps == null)
            {
                return Problem("Entity set 'CarSalesContext.Nhacungcaps'  is null.");
            }
            var nhacungcap = await _context.Nhacungcaps.FindAsync(id);
            if (nhacungcap != null)
            {
                _context.Nhacungcaps.Remove(nhacungcap);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NhacungcapExists(int id)
        {
          return (_context.Nhacungcaps?.Any(e => e.Idncc == id)).GetValueOrDefault();
        }
    }
}
