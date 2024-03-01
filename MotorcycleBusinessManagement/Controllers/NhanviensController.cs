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
    public class NhanviensController : Controller
    {
        private readonly CarSalesContext _context;
        private readonly IWebHostEnvironment _webHost;

        public NhanviensController(CarSalesContext context, IWebHostEnvironment webHost)
        {
            _context = context;
            _webHost = webHost;
        }

        // GET: Nhanviens
        public async Task<IActionResult> Index()
        {
            var activeNhanVien = await _context.Nhanviens.OrderByDescending(x => x.Idnv).Where(x=>x.Active == true).ToListAsync();
            var hiddenNhanVien = await _context.Nhanviens.OrderByDescending(x => x.Idnv).Where(x => x.Active == false).ToListAsync();
            ViewBag.ActiveNhanVien = activeNhanVien;
            ViewBag.HiddenNhanVien = hiddenNhanVien;
            var nv = _context.Nhanviens
                .OrderByDescending(i => i.Idnv)
                .FirstOrDefault();
            if (nv != null)
            {
                int idtd = nv.Idnv;
                if (nv.Idnv < 10)
                {
                    string Idnv = "NV000" + ++idtd;
                    TempData["GoiYNv"] = Idnv;
                }
                else if (nv.Idnv < 100 && nv.Idnv >= 10)
                {
                    string Idnv = "NV00" + ++idtd;
                    TempData["GoiYNv"] = Idnv;
                }
                else if (nv.Idnv < 1000 && nv.Idnv >= 100)
                {
                    string Idnv = "NV0" + ++idtd;
                    TempData["GoiYNv"] = Idnv;
                }
                else
                {
                    string Idnv = "NV" + ++idtd;
                    TempData["GoiYNv"] = Idnv;
                }

            }
            return View();
        }

        // GET: Nhanviens/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Nhanviens == null)
            {
                return NotFound();
            }

            var nhanvien = await _context.Nhanviens
                .FirstOrDefaultAsync(m => m.Idnv == id);
            if (nhanvien == null)
            {
                return NotFound();
            }

            return View(nhanvien);
        }

        
        // POST: Nhanviens/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Nhanvien nhanvien, IFormFile ProfilePhoto)
        {
            try
            {
                //if (ModelState.IsValid)
                //{
                //}
                //TempData["AlertErrorMessage"] = nhanvien.Manv;
                //return RedirectToAction(nameof(Index));
                    nhanvien.Active = true;
                    string uniqueFileName = GetUploadedFileName(nhanvien);
                    nhanvien.Hinhanh = uniqueFileName;
                    _context.Add(nhanvien);
                    _context.SaveChanges();
                    TempData["AlertSuccessMessage"] = nhanvien.Manv;
                    return RedirectToAction(nameof(Index));
                
            }
            catch
            {
                TempData["AlertErrorMessage"] = nhanvien.Manv;
                return RedirectToAction(nameof(Index));
            }
        }

        private string GetUploadedFileName(Nhanvien nhanvien)
        {
            string? uniqueFileName = null;

            if (nhanvien.ProfilePhoto != null)
            {
                string uploadsFolder = Path.Combine(_webHost.WebRootPath, "imgNV");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + nhanvien.ProfilePhoto.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    nhanvien.ProfilePhoto.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }
        // GET: Nhanviens/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Nhanviens == null)
            {
                return NotFound();
            }

            var nhanvien = await _context.Nhanviens.FindAsync(id);
            if (nhanvien == null)
            {
                return NotFound();
            }
            return View(nhanvien);
        }

        // POST: Nhanviens/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Nhanvien nhanvien, IFormFile ProfilePhoto)
        {
            if (id != nhanvien.Idnv)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    string uniqueFileName = GetUploadedFileName(nhanvien);
                    nhanvien.Hinhanh = uniqueFileName;
                    _context.Update(nhanvien);
                    await _context.SaveChangesAsync();
                    TempData["AlertUpdateMessage"] = nhanvien.Manv;
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NhanvienExists(nhanvien.Idnv))
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
            return View(nhanvien);
        }


        public async Task<IActionResult> Hidden(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var account = await _context.Nhanviens
                .FirstOrDefaultAsync(m => m.Idnv == id);
            if (account == null)
            {
                return NotFound();
            }
            account.Active = false;

            await _context.SaveChangesAsync();
            TempData["AlertDeleteMessage"] = account.Manv;
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Return(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var account = await _context.Nhanviens
                .FirstOrDefaultAsync(m => m.Idnv == id);
            if (account == null)
            {
                return NotFound();
            }


            account.Active = true;

            await _context.SaveChangesAsync();
            TempData["AlertReturnMessage"] = account.Manv;
            return RedirectToAction(nameof(Index));
        }
        // GET: Nhanviens/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Nhanviens == null)
            {
                return NotFound();
            }

            var nhanvien = await _context.Nhanviens
                .FirstOrDefaultAsync(m => m.Idnv == id);
            if (nhanvien == null)
            {
                return NotFound();
            }

            return View(nhanvien);
        }

        // POST: Nhanviens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Nhanviens == null)
            {
                return Problem("Entity set 'CarSalesContext.Nhanviens'  is null.");
            }
            var nhanvien = await _context.Nhanviens.FindAsync(id);
            if (nhanvien != null)
            {
                _context.Nhanviens.Remove(nhanvien);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NhanvienExists(int id)
        {
          return (_context.Nhanviens?.Any(e => e.Idnv == id)).GetValueOrDefault();
        }
    }
}
