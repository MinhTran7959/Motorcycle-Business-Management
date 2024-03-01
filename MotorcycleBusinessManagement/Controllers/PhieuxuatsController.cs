using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Bibliography;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using MotorcycleBusinessManagement.Models;
using Rotativa.AspNetCore;

namespace MotorcycleBusinessManagement.Controllers
{
    public class PhieuxuatsController : Controller
    {
        private readonly CarSalesContext _context;

        public PhieuxuatsController(CarSalesContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Indexold(string searchString, DateTime fromDate, DateTime toDate, int? selectkh, bool? trangthai)
        {
            var phieuxuat = await _context.Phieuxuats.Include(p => p.IdkhNavigation).Include(p => p.IdnvNavigation).Where(x => x.Active == true && x.Idnv != null && x.Idkh != null && x.Tongtienxuat != 0 && x.Tongtienxuat != null).OrderByDescending(x => x.Idpxk).Take(10).ToListAsync();
            var xeDaBan = await _context.Chitietxes.Include(c => c.IdmxNavigation).Include(c => c.IdpnkNavigation).Include(c => c.IdpxkNavigation).Include(c => c.IdxeNavigation).OrderByDescending(x => x.Idct).Where(x => x.Soluong == 0 && x.Idpxk!=null && x.Idpnk!=null && x.Idmx!=null && x.Idxe != null && x.Active == true).Take(10).ToListAsync();
            TempData["Khachhang"] = _context.Khachhangs.Where(x => x.Active == true).ToList();
            
            double? tongTien = 0;

            foreach (var tienphieuxuat in phieuxuat)
            {
                tongTien += tienphieuxuat.Tongtienxuat;
            }
            if (!string.IsNullOrEmpty(searchString))
            {
                phieuxuat = await _context.Phieuxuats.Include(p => p.IdkhNavigation).Include(p => p.IdnvNavigation)
                    .Where(x => x.Active == true && x.Sopxk.Contains(searchString) || x.IdkhNavigation.Tenkh.Contains(searchString)
                        || x.IdnvNavigation.Tennv.Contains(searchString) && x.Idnv != null && x.Idkh != null && x.Tongtienxuat != 0 && x.Tongtienxuat != null)
                    .OrderByDescending(x => x.Idpxk) // Nhóm theo Idpnk
                    .ToListAsync();
                xeDaBan = await _context.Chitietxes
                    .Include(c => c.IdmxNavigation)
                    .Include(c => c.IdpnkNavigation)
                    .Include(c => c.IdpxkNavigation)
                    .Include(c => c.IdxeNavigation)
                    .OrderByDescending(x => x.Idct)
                    .Where(x => x.Active == true && x.Idpnk != null
                        && x.IdpnkNavigation.Mapnk.Contains(searchString)
                        || x.IdmxNavigation.Tenmx.Contains(searchString)
                        || x.IdxeNavigation.Tenxe.Contains(searchString)
                        || x.Biensolucban.Contains(searchString))
                    .ToListAsync();
                //if (phieuxuat.Any() == false)
                //{
                //    TempData["AlertpxkMessage"] = "";
                //}
                //if (xeDaBan.Any() == false)
                //{
                //    TempData["AlertxedabanMessage"] = "";
                //}
            }

            var fdate = fromDate.ToString("ddMMyyyy");
            var tdate = toDate.ToString("ddMMyyyy");
            if (fdate != "01010001" && tdate != "01010001" && Int32.Parse(fdate) <= Int32.Parse(tdate))
            {
                phieuxuat = await _context.Phieuxuats.Include(p => p.IdkhNavigation).Include(p => p.IdnvNavigation)
                    .OrderByDescending(x => x.Ngayxuat)
                    .Where(x => x.Active == true 
                        && (x.Ngayxuat >= fromDate) && (x.Ngayxuat <= toDate) && x.Idnv != null && x.Idkh != null && x.Tongtienxuat != 0 && x.Tongtienxuat != null)
                    .ToListAsync();
                
                //if (phieuxuat.Any() == false)
                //{
                //    TempData["AlertpxkMessage"] = "";
                //}
            }
            
            if(trangthai == true && selectkh != null)
            {
                phieuxuat = await _context.Phieuxuats.Include(p => p.IdkhNavigation).Include(p => p.IdnvNavigation)
                .Where(x => x.Active == true && x.Idkh == selectkh && x.Dathu == true && x.Idpt != null && x.Idnv != null && x.Idkh != null && x.Tongtienxuat != 0 && x.Tongtienxuat != null)
                .OrderByDescending(x => x.Idpxk) // Nhóm theo Idpnk
                .ToListAsync();
                tongTien = 0;
                foreach (var tienphieuxuat in phieuxuat)
                {
                    tongTien += tienphieuxuat.Tongtienxuat;
                }
            }
            else if (trangthai == false && selectkh != null)
            {
                phieuxuat = await _context.Phieuxuats.Include(p => p.IdkhNavigation).Include(p => p.IdnvNavigation)
                .Where(x => x.Active == true && x.Idkh == selectkh && x.Dathu == false && x.Idpt == null && x.Idnv!=null&&x.Idkh!=null && x.Tongtienxuat != 0 && x.Tongtienxuat !=null)
                .OrderByDescending(x => x.Idpxk)
                .ToListAsync();
                tongTien = 0;
                foreach (var tienphieuxuat in phieuxuat)
                {
                    tongTien += tienphieuxuat.Tongtienxuat;
                }
            }
            else if (trangthai == null && selectkh != null)
            {
                phieuxuat = await _context.Phieuxuats.Include(p => p.IdkhNavigation).Include(p => p.IdnvNavigation)
                .Where(x => x.Active == true && x.Idkh == selectkh && x.Idnv != null && x.Idkh != null && x.Tongtienxuat != 0 && x.Tongtienxuat != null)
                .OrderByDescending(x => x.Idpxk)
                .ToListAsync();
                tongTien = 0;
                foreach (var tienphieuxuat in phieuxuat)
                {
                    tongTien += tienphieuxuat.Tongtienxuat;
                }
            }
            
            ViewBag.phieuBanXe = phieuxuat;
            ViewBag.xeDaBan = xeDaBan;
            ViewBag.tongTienXuat = tongTien;
            return View();
        }

        public async Task<IActionResult> Index(string searchString, DateTime fromDate, DateTime toDate, int? selectkh, bool? trangthai)
        {
            var phieuxuat = await _context.Phieuxuats.Include(p => p.IdkhNavigation).Include(p => p.IdnvNavigation).Where(x => x.Active == true && x.Idnv != null && x.Idkh != null && x.Tongtienxuat != 0 && x.Tongtienxuat != null).OrderByDescending(x => x.Idpxk).Take(10).ToListAsync();
            TempData["Khachhang"] = _context.Khachhangs.Where(x => x.Active == true).ToList();

            double? tongTien = 0;

            foreach (var tienphieuxuat in phieuxuat)
            {
                tongTien += tienphieuxuat.Tongtienxuat;
            }
            if (!string.IsNullOrEmpty(searchString))
            {
                phieuxuat = await _context.Phieuxuats.Include(p => p.IdkhNavigation).Include(p => p.IdnvNavigation)
                    .Where(x => x.Active == true && x.Sopxk.Contains(searchString) || x.IdkhNavigation.Tenkh.Contains(searchString)
                        || x.IdnvNavigation.Tennv.Contains(searchString) && x.Idnv != null && x.Idkh != null && x.Tongtienxuat != 0 && x.Tongtienxuat != null)
                    .OrderByDescending(x => x.Idpxk) // Nhóm theo Idpnk
                    .ToListAsync();
            }

            var fdate = fromDate.ToString("ddMMyyyy");
            var tdate = toDate.ToString("ddMMyyyy");
            if (fdate != "01010001" && tdate != "01010001" && Int32.Parse(fdate) <= Int32.Parse(tdate))
            {
                phieuxuat = await _context.Phieuxuats.Include(p => p.IdkhNavigation).Include(p => p.IdnvNavigation)
                    .OrderByDescending(x => x.Ngayxuat)
                    .Where(x => x.Active == true
                        && (x.Ngayxuat >= fromDate) && (x.Ngayxuat <= toDate) && x.Idnv != null && x.Idkh != null && x.Tongtienxuat != 0 && x.Tongtienxuat != null)
                    .ToListAsync();

                //if (phieuxuat.Any() == false)
                //{
                //    TempData["AlertpxkMessage"] = "";
                //}
            }

            if (trangthai == true && selectkh != null)
            {
                phieuxuat = await _context.Phieuxuats.Include(p => p.IdkhNavigation).Include(p => p.IdnvNavigation)
                .Where(x => x.Active == true && x.Idkh == selectkh && x.Dathu == true && x.Idpt != null && x.Idnv != null && x.Idkh != null && x.Tongtienxuat != 0 && x.Tongtienxuat != null)
                .OrderByDescending(x => x.Idpxk) // Nhóm theo Idpnk
                .ToListAsync();
                tongTien = 0;
                foreach (var tienphieuxuat in phieuxuat)
                {
                    tongTien += tienphieuxuat.Tongtienxuat;
                }
            }
            else if (trangthai == false && selectkh != null)
            {
                phieuxuat = await _context.Phieuxuats.Include(p => p.IdkhNavigation).Include(p => p.IdnvNavigation)
                .Where(x => x.Active == true && x.Idkh == selectkh && x.Dathu == false && x.Idpt == null && x.Idnv != null && x.Idkh != null && x.Tongtienxuat != 0 && x.Tongtienxuat != null)
                .OrderByDescending(x => x.Idpxk)
                .ToListAsync();
                tongTien = 0;
                foreach (var tienphieuxuat in phieuxuat)
                {
                    tongTien += tienphieuxuat.Tongtienxuat;
                }
            }
            else if (trangthai == null && selectkh != null)
            {
                phieuxuat = await _context.Phieuxuats.Include(p => p.IdkhNavigation).Include(p => p.IdnvNavigation)
                .Where(x => x.Active == true && x.Idkh == selectkh && x.Idnv != null && x.Idkh != null && x.Tongtienxuat != 0 && x.Tongtienxuat != null)
                .OrderByDescending(x => x.Idpxk)
                .ToListAsync();
                tongTien = 0;
                foreach (var tienphieuxuat in phieuxuat)
                {
                    tongTien += tienphieuxuat.Tongtienxuat;
                }
            }

            ViewBag.phieuBanXe = phieuxuat;
            ViewBag.tongTienXuat = tongTien;
            return View();
        }

        public async Task<IActionResult> IndexXeDaBan(string searchString, DateTime fromDate, DateTime toDate, int? selectxe, int? selectmauxe)
        {
            var xeDaBan = await _context.Chitietxes
                .Include(c => c.IdmxNavigation)
                .Include(c => c.IdpnkNavigation)
                .Include(c => c.IdpxkNavigation)
                .Include(c => c.IdpxkNavigation.IdkhNavigation)
                .Include(c => c.IdxeNavigation)
                .OrderByDescending(x => x.Idct)
                .Where(x => x.Soluong == 0 && x.Idpxk != null 
                && x.Idpnk != null && x.Idmx != null && x.Idxe != null 
                && x.Active == true).Take(200).ToListAsync();
            
            if (!string.IsNullOrEmpty(searchString))
            {
                xeDaBan = await _context.Chitietxes
                    .Include(c => c.IdmxNavigation)
                    .Include(c => c.IdpnkNavigation)
                    .Include(c => c.IdpxkNavigation)
                    .Include(c => c.IdpxkNavigation.IdkhNavigation)
                    .Include(c => c.IdxeNavigation)
                    .OrderByDescending(x => x.Idct)
                    .Where(x => x.Active == true && x.Soluong == 0 && x.Idpxk != null && x.Idpnk != null
                        && x.IdpnkNavigation.Mapnk.Contains(searchString)
                        ||x.IdpxkNavigation.IdkhNavigation.Tenkh.Contains(searchString)
                        || x.IdpxkNavigation.IdkhNavigation.Diachikh.Contains(searchString)
                        || x.IdmxNavigation.Tenmx.Contains(searchString)
                        || x.IdxeNavigation.Tenxe.Contains(searchString)
                        || x.Sokhung.Contains(searchString)
                        || x.Somay.Contains(searchString)
                        || x.Biensolucban.Contains(searchString))
                    .ToListAsync();
            }
            var fdate = fromDate.ToString("ddMMyyyy");
            var tdate = toDate.ToString("ddMMyyyy");
            if (fdate != "01010001" && tdate != "01010001" && Int32.Parse(fdate) <= Int32.Parse(tdate))
            {
                xeDaBan = await _context.Chitietxes
                    .Include(c => c.IdmxNavigation)
                    .Include(c => c.IdpnkNavigation)
                    .Include(c => c.IdpnkNavigation.IdnccNavigation)
                    .Include(c => c.IdpnkNavigation.IdnvNavigation)
                    .Include(c => c.IdpxkNavigation)
                    .Include(c => c.IdpxkNavigation.IdkhNavigation)
                    .Include(c => c.IdpxkNavigation.IdnvNavigation)
                    .Include(c => c.IdxeNavigation)
                    .OrderByDescending(x => x.IdpxkNavigation.Ngayxuat)
                    .Where(x => x.IdpxkNavigation.Active == true && x.Soluong == 0 && x.Idpxk != null && x.Idpnk != null
                        && (x.IdpxkNavigation.Ngayxuat >= fromDate) && (x.IdpxkNavigation.Ngayxuat <= toDate) && x.IdpxkNavigation.Idpt != null && x.IdpxkNavigation.Idkh != null && x.IdpxkNavigation.Idnv != null)
                    .GroupBy(x => x.Idpnk) // Nhóm theo Idpnk
                    .Select(g => g.First())
                    .ToListAsync();
            }
            TempData["Xe"] = _context.Xes.Where(x => x.Active == true).ToList();
            TempData["MauXe"] = _context.Mauxes.Where(x => x.Active == true).ToList();
            if (selectxe != null && selectmauxe != null)
            {
                xeDaBan = await _context.Chitietxes
                    .Include(c => c.IdmxNavigation)
                    .Include(c => c.IdpnkNavigation)
                    .Include(c => c.IdpnkNavigation.IdnccNavigation)
                    .Include(c => c.IdpnkNavigation.IdnvNavigation)
                    .Include(c => c.IdpxkNavigation)
                    .Include(c => c.IdpxkNavigation.IdkhNavigation)
                    .Include(c => c.IdpxkNavigation.IdnvNavigation)
                    .Include(c => c.IdxeNavigation)
                    .OrderByDescending(x => x.IdpxkNavigation.Ngayxuat)
                    .Where(x => x.IdpxkNavigation.Active == true && x.Soluong == 0 && x.Idpxk != null && x.Idpnk != null
                        && x.IdxeNavigation.Idxe == selectxe && x.IdmxNavigation.Idmx == selectmauxe && x.IdpxkNavigation.Idpt != null && x.IdpxkNavigation.Idkh != null && x.IdpxkNavigation.Idnv != null)
                    .GroupBy(x => x.Idpnk) // Nhóm theo Idpnk
                    .Select(g => g.First())
                    .ToListAsync();
            }
            else if (selectxe != null && selectmauxe == null)
            {

                xeDaBan = await _context.Chitietxes
                    .Include(c => c.IdmxNavigation)
                    .Include(c => c.IdpnkNavigation)
                    .Include(c => c.IdpnkNavigation.IdnccNavigation)
                    .Include(c => c.IdpnkNavigation.IdnvNavigation)
                    .Include(c => c.IdpxkNavigation.IdkhNavigation)
                    .Include(c => c.IdpxkNavigation.IdnvNavigation)
                    .Include(c => c.IdpxkNavigation)
                    .Include(c => c.IdxeNavigation)
                    .OrderByDescending(x => x.IdpxkNavigation.Ngayxuat)
                    .Where(x => x.IdpxkNavigation.Active == true && x.Soluong == 0 && x.Idpxk != null && x.Idpnk != null
                        && x.IdxeNavigation.Idxe == selectxe && x.IdpxkNavigation.Idpt != null && x.IdpxkNavigation.Idkh != null && x.IdpxkNavigation.Idnv != null)
                    .GroupBy(x => x.Idpnk) // Nhóm theo Idpnk
                    .Select(g => g.First())
                    .ToListAsync();
            }
            else if (selectxe == null && selectmauxe != null)
            {
                xeDaBan = await _context.Chitietxes
                    .Include(c => c.IdmxNavigation)
                    .Include(c => c.IdpnkNavigation)
                    .Include(c => c.IdpnkNavigation.IdnccNavigation)
                    .Include(c => c.IdpnkNavigation.IdnvNavigation)
                    .Include(c => c.IdpxkNavigation)
                    .Include(c => c.IdpxkNavigation.IdkhNavigation)
                    .Include(c => c.IdpxkNavigation.IdnvNavigation)
                    .Include(c => c.IdxeNavigation)
                    .OrderByDescending(x => x.IdpxkNavigation.Ngayxuat)
                    .Where(x => x.IdpxkNavigation.Active == true && x.Soluong == 0 && x.Idpxk != null && x.Idpnk != null
                        && x.IdmxNavigation.Idmx == selectmauxe && x.IdpxkNavigation.Idpt != null && x.IdpxkNavigation.Idkh != null && x.IdpxkNavigation.Idnv != null)
                    .GroupBy(x => x.Idpnk) // Nhóm theo Idpnk
                    .Select(g => g.First())
                    .ToListAsync();
            }
            ViewBag.xeDaBan = xeDaBan;
            return View();
        }
        // GET: Phieuxuats
        //public async Task<IActionResult> Index()
        //{
        //    ClearPhieuXuatTam();
        //    var phieuxuat = await _context.Phieuxuats.Include(p => p.IdkhNavigation).Include(p => p.IdnvNavigation).Where(x=>x.Active==true).OrderByDescending(x => x.Idpxk).ToListAsync();
        //    var xeDaBan = await _context.Chitietxes.Include(c => c.IdmxNavigation).Include(c => c.IdpnkNavigation).Include(c => c.IdpxkNavigation).Include(c => c.IdxeNavigation).OrderByDescending(x => x.Idct).Where(x=>x.Soluong==0 && x.Active==true).ToListAsync();
        //    ViewBag.phieuBanXe = phieuxuat;
        //    ViewBag.xeDaBan = xeDaBan;
        //    return View();
        //}


        //[HttpGet]
        //public IActionResult Create()
        //{
        //    string Sophieutudong = TaoSoPhieu();
        //    ViewData["idtd"] = Sophieutudong;

        //    TempData["Khachhang"] = _context.Khachhangs.Where(x => x.Active == true).ToList();
        //    TempData["Nhanvien"] = _context.Nhanviens.Where(x => x.Active == true).ToList();
        //    var xeDeBan = _context.Chitietxes.Include(c => c.IdmxNavigation).Include(c => c.IdpnkNavigation).Include(c => c.IdpxkNavigation).Include(c => c.IdxeNavigation).OrderByDescending(x => x.Idct).Where(x => x.Soluong == 1 && x.Active == true && x.Idpnk != null).ToList();
        //    ViewBag.xeDeBan = xeDeBan;
        //    return View();
        //}

        //[HttpPost]
        //public IActionResult Create(Phieuxuat px)
        //{
        //    try
        //    {
        //        // Lấy dữ liệu ngày hiện tại
        //        px.Ngayxuat = DateTime.Now;
        //        px.Active = true;
        //        px.Dathu = false;
        //        // Xóa các dòng với Active == false
        //        px.Chitietxes.RemoveAll(n => n.Active == false || n.Soluong == 0);
        //        _context.Add(px);
        //        _context.SaveChanges();
        //        List<PhieuXuatTam> phieuXuatTam = _context.PhieuXuatTams.Where(x => x.Idct != 0).ToList();
        //        foreach (PhieuXuatTam tam in phieuXuatTam)
        //        {
        //            Chitietxe chitietxe = _context.Chitietxes.Find((int)tam.Idct);
        //            chitietxe.Soluong = 0;
        //            chitietxe.Idpxk = px.Idpxk;
        //            _context.Chitietxes.Update(chitietxe);
        //            _context.PhieuXuatTams.Remove(tam);
        //            _context.SaveChanges();
        //        }
        //        TempData["AlertSuccessMessage"] = px.Sopxk;
        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
        //public void ClearPhieuXuatTam()
        //{
        //    List<PhieuXuatTam> a = _context.PhieuXuatTams.ToList();
        //    _context.RemoveRange(a);
        //    _context.SaveChanges();
        //}
        //[HttpPost("/Phieuxuats/GetIDCT")]
        //public void GetIDCT(int idct)
        //{
        //    PhieuXuatTam phieuXuatTam = _context.PhieuXuatTams.FirstOrDefault(x => x.Idct == idct);
        //    if (phieuXuatTam != null)
        //    {
        //        _context.PhieuXuatTams.Remove(phieuXuatTam);
        //    }
        //    else
        //    {
        //        PhieuXuatTam pxt = new PhieuXuatTam();
        //        pxt.Idct = idct;
        //        _context.PhieuXuatTams.Add(pxt);
        //    }
        //    _context.SaveChanges();
        //}

        [HttpGet]
        public IActionResult Create()
        {
            string Sophieutudong = TaoSoPhieu();
            ViewData["idtd"] = Sophieutudong;
            TempData["Xe"] = _context.Chitietxes.Where(x => x.Active == true && x.Idpnk != null
                        && x.Idxe != null && x.Idmx != null && x.Idpxk == null && x.Soluong == 1)
                        .Include(d=>d.IdpnkNavigation)
                        .Include(a => a.IdxeNavigation).Include(b => b.IdmxNavigation)
                        .Include(c => c.IdpnkNavigation).Take(200).ToList();
            TempData["Khachhang"] = _context.Khachhangs.Where(x => x.Active == true).OrderByDescending(x=>x.Idkh).ToList();
            TempData["Nhanvien"] = _context.Nhanviens.Where(x => x.Active == true).ToList();
            Phieuxuat pxk = new Phieuxuat();
            pxk.Chitietxes.Add(new Chitietxe() { Idct = 1 });

            return View(pxk);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Phieuxuat px)
        {
            //if (ModelState.IsValid)
            //{
            try { 
                if (px.Chitietxes.Count() == 0)
                {
                    try
                    {
                        px.Chitietxes.RemoveAll(n => n.Active == false || n.Giaban == null || n.Idct == null || n.Giaban == double.NaN || n.Sokhung == null || n.Somay == null);
                        px.Ngayxuat = DateTime.Now;
                        px.Active = true;
                        px.Dathu = false;
                        _context.Add(px);
                        // Lặp qua danh sách Chitietxes
                        foreach (var updatedChitietxe in px.Chitietxes)
                        {
                            // Kiểm tra xem Idct đã tồn tại hay chưa
                            Chitietxe existingChitietxe = px.Chitietxes
                                .FirstOrDefault(c => c.Idct == updatedChitietxe.Idct);

                            if (existingChitietxe != null)
                            {
                                // Cập nhật thông tin của Chitietxe
                                existingChitietxe.Idpnk = updatedChitietxe.Idpnk;
                                existingChitietxe.Idmx = updatedChitietxe.Idmx;
                                existingChitietxe.Idxe = updatedChitietxe.Idxe;
                                existingChitietxe.Doixe = updatedChitietxe.Doixe;
                                existingChitietxe.Sokhung = updatedChitietxe.Sokhung;
                                existingChitietxe.Somay = updatedChitietxe.Somay;
                                existingChitietxe.Gianhap = updatedChitietxe.Gianhap;
                                existingChitietxe.Trangthai = updatedChitietxe.Trangthai;
                                existingChitietxe.Biensolucmua = updatedChitietxe.Biensolucmua;
                                existingChitietxe.Biensolucban = updatedChitietxe.Biensolucban;
                                existingChitietxe.Thoigianbaohanh = updatedChitietxe.Thoigianbaohanh;
                                existingChitietxe.Soluong = 0;
                                existingChitietxe.Giaban = updatedChitietxe.Giaban;
                                existingChitietxe.Active = updatedChitietxe.Active;

                                _context.Chitietxes.Update(existingChitietxe);
                            }
                            else
                            {
                                // Thêm mới Chitietxe
                                px.Chitietxes.Add(updatedChitietxe);
                            }
                        }

                        // Xóa các dòng với Active == false

                        // Lưu các thay đổi
                        _context.SaveChanges();

                        TempData["AlertErrorMessage"] = px.Sopxk;
                        return RedirectToAction("Index");
                    }
                    catch (Exception ex)
                    {
                        // Xử lý lỗi
                        return View();
                    }
                }
                else
                {
                    try
                    {
                        px.Chitietxes.RemoveAll(n => n.Active == false || n.Giaban == null || n.Idct == null || n.Giaban == double.NaN || n.Sokhung == null || n.Somay == null);
                        px.Ngayxuat = DateTime.Now;
                        px.Active = true;
                        px.Dathu = false;
                        _context.Add(px);
                        // Lặp qua danh sách Chitietxes
                        foreach (var updatedChitietxe in px.Chitietxes)
                        {
                            // Kiểm tra xem Idct đã tồn tại hay chưa
                            Chitietxe existingChitietxe = px.Chitietxes
                                .FirstOrDefault(c => c.Idct == updatedChitietxe.Idct);

                            if (existingChitietxe != null)
                            {
                                // Cập nhật thông tin của Chitietxe
                                existingChitietxe.Idpnk = updatedChitietxe.Idpnk;
                                existingChitietxe.Idmx = updatedChitietxe.Idmx;
                                existingChitietxe.Idxe = updatedChitietxe.Idxe;
                                existingChitietxe.Doixe = updatedChitietxe.Doixe;
                                existingChitietxe.Sokhung = updatedChitietxe.Sokhung;
                                existingChitietxe.Somay = updatedChitietxe.Somay;
                                existingChitietxe.Gianhap = updatedChitietxe.Gianhap;
                                existingChitietxe.Trangthai = updatedChitietxe.Trangthai;
                                existingChitietxe.Biensolucmua = updatedChitietxe.Biensolucmua;
                                existingChitietxe.Biensolucban = updatedChitietxe.Biensolucban;
                                existingChitietxe.Thoigianbaohanh = updatedChitietxe.Thoigianbaohanh;
                                existingChitietxe.Soluong = 0;
                                existingChitietxe.Giaban = updatedChitietxe.Giaban;
                                existingChitietxe.Active = updatedChitietxe.Active;

                                _context.Chitietxes.Update(existingChitietxe);
                            }
                            else
                            {
                                // Thêm mới Chitietxe
                                px.Chitietxes.Add(updatedChitietxe);
                            }
                        }

                        // Xóa các dòng với Active == false

                        // Lưu các thay đổi
                        _context.SaveChanges();

                        TempData["AlertSuccessMessage"] = px.Sopxk;
                        return RedirectToAction("Index");
                    }
                    catch (Exception ex)
                    {
                        // Xử lý lỗi
                        return View();
                    }
                }

                //}

                //return View();
            }
            catch
            {
                TempData["AlertErrorMessage"] = "";
                return RedirectToAction("Index");
            }
        }

        private string TaoSoPhieu()
        {
            // Lấy ngày tháng năm hiện tại
            string ngayThangNam = DateTime.Now.ToString("ddMMyy");

            // Kiểm tra xem có phiếu cùng ngày không
            var phieuCu = _context.Phieuxuats.OrderByDescending(x => x.Sopxk).FirstOrDefault(p => p.Sopxk.StartsWith($"XK{ngayThangNam}"));

            if (phieuCu != null)
            {
                // Tách lấy số phiếu hiện tại
                string soPhieuCu = phieuCu.Sopxk.Substring(8);
                int soPhieuMoi = int.Parse(soPhieuCu) + 1;
                return $"XK{ngayThangNam}{soPhieuMoi:D4}";
            }
            else
            {
                // Nếu không có phiếu cùng ngày, bắt đầu từ 1
                return $"XK{ngayThangNam}0001";
            }
        }
        [HttpGet]
        public IActionResult CreateKH()
        {
            var kh = _context.Khachhangs
                .OrderByDescending(i => i.Idkh)
                .FirstOrDefault();

            if (kh != null)
            {
                int idtc = kh.Idkh;
                if (kh.Idkh < 10)
                {
                    string idkh = "KH000" + ++idtc;
                    TempData["GoiYKH"] = idkh;
                }
                else if (idtc < 100 && idtc >= 10)
                {
                    string idkh = "KH00" + ++idtc;
                    TempData["GoiYKH"] = idkh;
                }
                else if (idtc < 1000 && idtc >= 100)
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateKH([Bind("Idkh,Makh,Tenkh,Diachikh,Dienthoaikh,Ngaysinh,Cccdkh,Emailkh,Quequankh,Masothuekh,Ghichu,Active")] Khachhang khachhang)
        {
            if (ModelState.IsValid)
            {
                khachhang.Active = true;
                _context.Add(khachhang);
                await _context.SaveChangesAsync();
                TempData["AlertSuccessKHMessage"] = khachhang.Makh;
                return RedirectToAction(nameof(Create));
            }
            return View(khachhang);
        }
        [HttpGet]
        public IActionResult Details(int id)
        {
            try
            {
                TempData["Tenxe"] = _context.Xes.Where(x => x.Active == true).ToList();
                TempData["Tenmx"] = _context.Mauxes.Where(x => x.Active == true).ToList();
                TempData["Pnk"] = _context.Phieunhapkhos.Where(x => x.Active == true).ToList();
                TempData["Khachhang"] = _context.Khachhangs.Where(x => x.Active == true).ToList();
                TempData["Nhanvien"] = _context.Nhanviens.Where(x => x.Active == true).ToList();
                Phieuxuat? px = _context.Phieuxuats
                    .Include(e => e.Chitietxes)
                    .Where(a => a.Idpxk == id).FirstOrDefault();

                return View(px);
            }
            catch
            {
                return View();
            }

        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            try
            {
                TempData["Tenxe"] = _context.Xes.Where(x => x.Active == true).ToList();
                TempData["Tenmx"] = _context.Mauxes.Where(x => x.Active == true).ToList();
                TempData["Pnk"] = _context.Phieunhapkhos.Where(x => x.Active == true).ToList();
                TempData["Khachhang"] = _context.Khachhangs.Where(x => x.Active == true).ToList();
                TempData["Nhanvien"] = _context.Nhanviens.Where(x => x.Active == true).ToList();
                Phieuxuat? px = _context.Phieuxuats
                    .Include(e => e.Chitietxes)
                    .Where(a => a.Idpxk == id).FirstOrDefault();

                return View(px);
            }
            catch
            {
                return View();
            }

        }

        [HttpPost]
        public IActionResult Delete(Phieuxuat px)
        {
            try { 
                Phieuxuat? existingApplicant = _context.Phieuxuats
                    .Include(e => e.Chitietxes)
                    .FirstOrDefault(a => a.Idpxk == px.Idpxk);
                existingApplicant.Active = false;

                var ctxActive = _context.Chitietxes.Where(x => x.Idpxk == px.Idpxk).ToList();
                foreach (var item in ctxActive)
                {
                    item.Idpxk = null;
                    item.Soluong = 1;
                }
                _context.SaveChanges();
                TempData["AlertDeleteMessage"] = px.Sopxk;
                return RedirectToAction("Index");
            }
            catch
            {
                TempData["AlertErrorMessage"] = "";
                return RedirectToAction("Index");
            }
        }


        [HttpGet]
        public IActionResult Edit(int? id)
        {
            try
            {
                TempData["Xe"] = _context.Chitietxes.Where(x => x.Active == true && x.Idpnk != null
                        && x.Idxe != null && x.Idmx != null && x.Idpxk == null || x.Idpxk == id)
                        .Include(a => a.IdxeNavigation).Include(b => b.IdmxNavigation)
                        .Include(c => c.IdpnkNavigation).ToList();
                TempData["Khachhang"] = _context.Khachhangs.Where(x => x.Active == true).ToList();
                TempData["Nhanvien"] = _context.Nhanviens.Where(x => x.Active == true).ToList();
                Phieuxuat? px = _context.Phieuxuats
                    .Include(e => e.Chitietxes)
                    .Where(a => a.Idpxk == id).FirstOrDefault();

                return View(px);
            }
            catch
            {
                return View();
            }
        }


        [HttpPost]
        public IActionResult Edit(int id, Phieuxuat px)
        {
            try { 
                if (id != px.Idpxk)
                {
                    return BadRequest(); // Xử lý trường hợp id không khớp
                }

                // Kiểm tra xem ứng viên có tồn tại trong cơ sở dữ liệu hay không
                Phieuxuat? existingApplicant = _context.Phieuxuats
                    .Include(e => e.Chitietxes)
                    .FirstOrDefault(a => a.Idpxk == px.Idpxk);

                if (existingApplicant == null)
                {
                    return NotFound(); // Xử lý trường hợp không tìm thấy ứng viên
                }

                px.Chitietxes.RemoveAll(n => n.Active == false || n.Giaban == null || n.Idct == null);
                List<Chitietxe> chitietxes1 = new List<Chitietxe>();
                foreach (Chitietxe chitietxe in px.Chitietxes)
                {
                    Chitietxe ct = new Chitietxe();
                    ct.Idpnk = chitietxe.Idpnk;
                    ct.Idmx = chitietxe.Idmx;
                    ct.Idxe = chitietxe.Idxe;
                    ct.Doixe = chitietxe.Doixe;
                    ct.Gianhap = chitietxe.Gianhap;
                    ct.Trangthai = chitietxe.Trangthai;
                    ct.Biensolucmua = chitietxe.Biensolucmua;
                    ct.Biensolucban = chitietxe.Biensolucban;
                    ct.Thoigianbaohanh = chitietxe.Thoigianbaohanh;
                    ct.Sokhung = chitietxe.Sokhung;
                    ct.Somay = chitietxe.Somay;
                    ct.Idpxk = chitietxe.Idpxk;
                    ct.Active = true;
                    ct.Soluong = 0;
                    ct.Giaban = chitietxe.Giaban;
                    chitietxes1.Add(ct);
                }
                // Cập nhật thông tin của ứng viên
                existingApplicant.Sopxk = px.Sopxk; // Thay thế bằng các trường thông tin khác cần cập nhật
                existingApplicant.Ngayxuat = px.Ngayxuat;
                existingApplicant.Sohd = px.Sohd;
                existingApplicant.Ngayhd = px.Ngayhd;
                existingApplicant.Idkh = px.Idkh;
                existingApplicant.Idnv = px.Idnv;
                existingApplicant.Tongtienxuat = px.Tongtienxuat;
                existingApplicant.Active = true;

                // Xóa tất cả các trải nghiệm cũ
                List<Chitietxe> chitietxes = _context.Chitietxes.Where(x => x.Idpxk == px.Idpxk).ToList();
                _context.Chitietxes.RemoveRange(chitietxes);
                _context.SaveChanges();
                _context.Chitietxes.AddRange(chitietxes1);
            
            
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                TempData["AlertErrorMessage"] = "";
                return RedirectToAction("Index");
            }
        }

        public IActionResult DownPDF(int? id)
        {
            try { 
                if (id == null || _context.Phieuxuats == null)
                {
                    return NotFound();
                }


                var pxk = _context.Phieuxuats
                    .Include(p => p.IdkhNavigation)
                    .Include(p => p.IdnvNavigation)
                    .Where(x => x.Active == true && x.Idpxk == id).FirstOrDefault();

                var chitietxe = _context.Chitietxes
                    .Include(c => c.IdmxNavigation)
                    .Include(c => c.IdpnkNavigation)
                    .Include(c => c.IdpxkNavigation)
                    .Include(c => c.IdxeNavigation)
                    .Where(x => x.Active == true && x.Idpxk == id && x.Idmx != null && x.Idxe != null && x.Idpnk != null).ToList();
                ViewBag.ctx = chitietxe;

                if (pxk == null)
                {
                    return NotFound();
                }

                return new ViewAsPdf("DownPDF", pxk)
                {
                    FileName = $"Phieuxuat {pxk.Sopxk}.pdf",
                    PageOrientation = Rotativa.AspNetCore.Options.Orientation.Portrait,
                    PageSize = Rotativa.AspNetCore.Options.Size.A4,
                    ViewData = new ViewDataDictionary(new EmptyModelMetadataProvider(), new ModelStateDictionary())
                    {
                        Model = pxk,
                        ["ctx"] = chitietxe
                    }

                };
            }
            catch
            {
                TempData["AlertErrorMessage"] = "";
                return RedirectToAction("Index");
            }
        }

        public async Task<IActionResult> ExportExcel()
        {
            try { 
            double? tongGianhap = 0;
            double? tongGiaban = 0;
            double? tienLoi = 0;
            var xedaban = await _context.Chitietxes
                .Include(c => c.IdmxNavigation)
                .Include(c => c.IdpnkNavigation)
                .Include(c => c.IdpnkNavigation.IdnccNavigation)
                .Include(c => c.IdpnkNavigation.IdnvNavigation)
                .Include(c => c.IdpxkNavigation)
                .Include(c => c.IdpxkNavigation.IdkhNavigation)
                .Include(c => c.IdpxkNavigation.IdnvNavigation)
                .Include(c => c.IdxeNavigation)
                .Include(c => c.IdxeNavigation.IdlxNavigation)
                .Where(i => i.Soluong == 0 && i.Active==true && i.Idpnk!=null &&i.Idpxk!=null &&i.Idmx!=null&&i.Idxe!=null)
                .OrderByDescending(x => x.Idct)
                .ToListAsync();

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Chitietxes");
                var currentRow = 3;


                
                worksheet.Cell(2, 1).Value = "Danh sách các xe đã bán";

                //worksheet.Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                //worksheet.Cell(currentRow, 1).Value = "Danh sách các xe đã bán";

                worksheet.Cell(currentRow, 2).Value = "Số phiếu nhập";
                worksheet.Cell(currentRow, 3).Value = "Ngày nhập";
                worksheet.Cell(currentRow, 4).Value = "Số phiếu bán";
                worksheet.Cell(currentRow, 5).Value = "Ngày bán";
                worksheet.Cell(currentRow, 6).Value = "Tên xe";
                worksheet.Cell(currentRow, 7).Value = "Màu xe";
                worksheet.Cell(currentRow, 8).Value = "Đời xe";
                worksheet.Cell(currentRow, 9).Value = "Số khung";
                worksheet.Cell(currentRow, 10).Value = "Số máy";
                worksheet.Cell(currentRow, 11).Value = "Biển số lúc mua";
                worksheet.Cell(currentRow, 12).Value = "Biển số lúc bán";
                worksheet.Cell(currentRow, 13).Value = "Trạng thái";
                worksheet.Cell(currentRow, 14).Value = "Tên nhà cung cấp";
                worksheet.Cell(currentRow, 15).Value = "Địa chỉ nhà cung cấp";
                worksheet.Cell(currentRow, 16).Value = "Tên nhân viên nhập";
                worksheet.Cell(currentRow, 17).Value = "Tên khách hàng mua";
                worksheet.Cell(currentRow, 18).Value = "Địa chỉ khách hàng mua";
                worksheet.Cell(currentRow, 19).Value = "Số điện thoại khách hàng mua";
                worksheet.Cell(currentRow, 20).Value = "Tên nhân viên bán";
                worksheet.Cell(currentRow, 21).Value = "Thời gian bảo hành";
                worksheet.Cell(currentRow, 22).Value = "Giá nhập";
                worksheet.Cell(currentRow, 23).Value = "Giá bán";

                
                foreach (var slt in xedaban)
                {
                    currentRow++;
                    worksheet.Cell(currentRow, 2).Value = slt.IdpnkNavigation.Mapnk;
                    worksheet.Cell(currentRow, 3).Value = slt.IdpnkNavigation.Ngaynhap;
                    worksheet.Cell(currentRow, 3).Style.DateFormat.Format = "dd/MM/yyyy";
                    worksheet.Cell(currentRow, 4).Value = slt.IdpxkNavigation.Sopxk;
                    worksheet.Cell(currentRow, 5).Value = slt.IdpxkNavigation.Ngayxuat;
                    worksheet.Cell(currentRow, 5).Style.DateFormat.Format = "dd/MM/yyyy";
                    worksheet.Cell(currentRow, 6).Value = slt.IdxeNavigation.Tenxe;
                    worksheet.Cell(currentRow, 7).Value = slt.IdmxNavigation.Tenmx;
                    worksheet.Cell(currentRow, 8).Value = slt.Doixe;
                    worksheet.Cell(currentRow, 9).Value = slt.Sokhung;
                    worksheet.Cell(currentRow, 10).Value = slt.Somay;
                    worksheet.Cell(currentRow, 11).Value = slt.Biensolucmua;
                    worksheet.Cell(currentRow, 12).Value = slt.Biensolucban;
                    worksheet.Cell(currentRow, 13).Value = slt.Trangthai;
                    worksheet.Cell(currentRow, 14).Value = slt.IdpnkNavigation.IdnccNavigation.Tenncc;
                    worksheet.Cell(currentRow, 15).Value = slt.IdpnkNavigation.IdnccNavigation.Diachincc;
                    worksheet.Cell(currentRow, 16).Value = slt.IdpnkNavigation.IdnvNavigation.Tennv;
                    worksheet.Cell(currentRow, 17).Value = slt.IdpxkNavigation.IdkhNavigation.Tenkh;
                    worksheet.Cell(currentRow, 18).Value = slt.IdpxkNavigation.IdkhNavigation.Diachikh;
                    worksheet.Cell(currentRow, 19).Value = slt.IdpxkNavigation.IdkhNavigation.Dienthoaikh;
                    worksheet.Cell(currentRow, 20).Value = slt.IdpxkNavigation.IdnvNavigation.Tennv;
                    worksheet.Cell(currentRow, 21).Value = slt.Thoigianbaohanh;
                    worksheet.Cell(currentRow, 22).Value = slt.Gianhap;
                    worksheet.Cell(currentRow, 23).Value = slt.Giaban;

                    tongGianhap += slt.Gianhap;
                    tongGiaban += slt.Giaban;
                    
                }
                currentRow += 3;
                worksheet.Cell(currentRow, 2).Value = "Tổng giá nhập";
                worksheet.Cell(currentRow, 3).Value = tongGianhap;
                currentRow++;
                worksheet.Cell(currentRow, 2).Value = "Tổng giá bán";
                worksheet.Cell(currentRow, 3).Value = tongGiaban;
                currentRow++;
                tienLoi += tongGiaban - tongGianhap;
                worksheet.Cell(currentRow, 2).Value = "Tiền lời";
                worksheet.Cell(currentRow, 3).Value = tienLoi;
                using (var stream = new MemoryStream())
                {
                    
                    worksheet.Columns().AdjustToContents();
                    worksheet.RangeUsed().Style.Border.OutsideBorder = XLBorderStyleValues.Thick;
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Danhsachxedaban.xlsx");
                }
            }
            }
            catch
            {
                TempData["AlertErrorMessage"] = "";
                return RedirectToAction("Index");
            }
        }
        public async Task<IActionResult> ExportExcelPX()
        {
            try { 
                double? tongGiaban = 0;
                double? tongTienDaThu = 0;

                var phieuxuat = await _context.Phieuxuats
                    .Include(p => p.IdkhNavigation)
                    .Include(p => p.IdnvNavigation)
                    .Where(x => x.Active == true)
                    .OrderByDescending(x => x.Idpxk)
                    .ToListAsync();
            
                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Phieuxuats");
                    var currentRow = 3;



                    worksheet.Cell(2, 1).Value = "Danh sách phiếu bán xe";

                    //worksheet.Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    //worksheet.Cell(currentRow, 1).Value = "Danh sách các xe đã bán";

                
                    worksheet.Cell(currentRow, 2).Value = "Số phiếu bán";
                    worksheet.Cell(currentRow, 3).Value = "Ngày bán";
                    worksheet.Cell(currentRow, 4).Value = "Số hóa đơn";
                    worksheet.Cell(currentRow, 5).Value = "Ngày hóa đơn";
                    worksheet.Cell(currentRow, 6).Value = "Nhân viên bán";
                    worksheet.Cell(currentRow, 7).Value = "Khách hàng";
                    worksheet.Cell(currentRow, 8).Value = "Tổng tiền phiếu";
                    worksheet.Cell(currentRow, 9).Value = "Trạng thái";


                    foreach (var slt in phieuxuat)
                    {
                        currentRow++;
                        worksheet.Cell(currentRow, 2).Value = slt.Sopxk;
                        worksheet.Cell(currentRow, 3).Value = slt.Ngayxuat;
                        worksheet.Cell(currentRow, 3).Style.DateFormat.Format = "dd/MM/yyyy";
                        worksheet.Cell(currentRow, 4).Value = slt.Sohd;
                        worksheet.Cell(currentRow, 5).Value = slt.Ngayhd;
                        worksheet.Cell(currentRow, 5).Style.DateFormat.Format = "dd/MM/yyyy";
                        worksheet.Cell(currentRow, 6).Value = slt.IdnvNavigation.Tennv;
                        worksheet.Cell(currentRow, 7).Value = slt.IdkhNavigation.Tenkh;
                        worksheet.Cell(currentRow, 8).Value = slt.Tongtienxuat;
                        if (slt.Dathu == true)
                        {
                            worksheet.Cell(currentRow, 9).Value = "Đã thu tiền";
                            tongTienDaThu += slt.Tongtienxuat;
                        }
                        else
                        {
                            worksheet.Cell(currentRow, 9).Value = "Chưa thu tiền";
                        }
                    
                        tongGiaban += slt.Tongtienxuat;

                    }
                    currentRow += 3;
                    worksheet.Cell(currentRow, 2).Value = "Tổng giá bán";
                    worksheet.Cell(currentRow, 3).Value = tongGiaban;
                    currentRow += 2;
                    worksheet.Cell(currentRow, 2).Value = "Tổng tiền đã thu";
                    worksheet.Cell(currentRow, 3).Value = tongTienDaThu;
                    using (var stream = new MemoryStream())
                    {
                    
                        worksheet.Columns().AdjustToContents();
                        worksheet.RangeUsed().Style.Border.OutsideBorder = XLBorderStyleValues.Thick;
                        workbook.SaveAs(stream);
                        var content = stream.ToArray();
                        return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Danhsachphieubanxe.xlsx");
                    }
                }
            }
            catch
            {
                TempData["AlertErrorMessage"] = "";
                return RedirectToAction("Index");
            }
        }
    }
}
