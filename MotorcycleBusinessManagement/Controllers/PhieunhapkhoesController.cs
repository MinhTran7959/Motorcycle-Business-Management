using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using MotorcycleBusinessManagement.Models;
using Rotativa.AspNetCore;


namespace MotorcycleBusinessManagement.Controllers
{
    public class PhieunhapkhoesController : Controller
    {
        private readonly CarSalesContext _context;

        public PhieunhapkhoesController(CarSalesContext context)
        {
            _context = context;
        }

        // GET: Phieunhapkhoes
        public async Task<IActionResult> Indexold(string searchString, DateTime fromDate, DateTime toDate, int? selectncc, bool? trangthai)
        {
            var phieunhapkho = await _context.Chitietxes
                .Include(c => c.IdmxNavigation)
                .Include(c => c.IdpnkNavigation)
                .Include(c => c.IdpnkNavigation.IdnccNavigation)
                .Include(c => c.IdpnkNavigation.IdnvNavigation)
                .Include(c => c.IdpxkNavigation)
                .Include(c => c.IdxeNavigation)
                .OrderByDescending(x => x.Idpnk)
                .Where(x => x.Active == true && x.Idpnk != null && x.Trangthai == "Mới")
                .GroupBy(x => x.Idpnk) // Nhóm theo Idpnk
                .Select(g => g.First())
                .ToListAsync();
            var phieunhapkhoxecu = await _context.Chitietxes
                .Include(c => c.IdmxNavigation)
                .Include(c => c.IdpnkNavigation)
                .Include(c => c.IdpnkNavigation.IdnccNavigation)
                .Include(c => c.IdpnkNavigation.IdnvNavigation)
                .Include(c => c.IdpxkNavigation)
                .Include(c => c.IdxeNavigation)
                .OrderByDescending(x => x.Idpnk)
                .Where(x => x.Active == true && x.Idpnk != null && x.Trangthai == "Cũ")
                .GroupBy(x => x.Idpnk) // Nhóm theo Idpnk
                .Select(g => g.First())
                .ToListAsync();
            var xeDaNhap = await _context.Chitietxes
                .Include(c => c.IdmxNavigation)
                .Include(c => c.IdpnkNavigation)
                .Include(c => c.IdpxkNavigation)
                .Include(c => c.IdxeNavigation)
                .OrderByDescending(x=>x.Idct)
                .Where(x => (x.Active == true&& x.Idpnk != null))
                .ToListAsync();
            double? tongTien = 0;

            foreach (var tienphieunhap in phieunhapkho)
            {
                tongTien += tienphieunhap.IdpnkNavigation.Tongtiennhap;
            }
            double? tongTienNhapXeCu = 0;

            foreach (var tienphieunhapxecu in phieunhapkhoxecu)
            {
                tongTienNhapXeCu += tienphieunhapxecu.IdpnkNavigation.Tongtiennhap;
            }
            TempData["Nhacungcap"] = _context.Nhacungcaps.Where(x => x.Active == true).ToList();
            if (!string.IsNullOrEmpty(searchString))
            {
                phieunhapkho = await _context.Chitietxes
                    .Include(c => c.IdmxNavigation)
                    .Include(c => c.IdpnkNavigation)
                    .Include(c => c.IdpnkNavigation.IdnccNavigation)
                    .Include(c => c.IdpnkNavigation.IdnvNavigation)
                    .Include(c => c.IdpxkNavigation)
                    .Include(c => c.IdxeNavigation)
                    .OrderByDescending(x => x.Idpnk)
                    .Where(x => x.Active == true && x.Idpnk != null && x.Trangthai == "Mới"
                        && x.IdpnkNavigation.Mapnk.Contains(searchString)|| x.IdpnkNavigation.IdnccNavigation.Tenncc.Contains(searchString) 
                        || x.IdpnkNavigation.IdnvNavigation.Tennv.Contains(searchString))
                    .GroupBy(x => x.Idpnk) // Nhóm theo Idpnk
                    .Select(g => g.First())
                    .ToListAsync();
                phieunhapkhoxecu = await _context.Chitietxes
                    .Include(c => c.IdmxNavigation)
                    .Include(c => c.IdpnkNavigation)
                    .Include(c => c.IdpnkNavigation.IdnccNavigation)
                    .Include(c => c.IdpnkNavigation.IdnvNavigation)
                    .Include(c => c.IdpxkNavigation)
                    .Include(c => c.IdxeNavigation)
                    .OrderByDescending(x => x.Idpnk)
                    .Where(x => x.Active == true && x.Idpnk != null && x.Trangthai == "Cũ"
                        && x.IdpnkNavigation.Mapnk.Contains(searchString) || x.IdpnkNavigation.IdnccNavigation.Tenncc.Contains(searchString)
                        || x.IdpnkNavigation.IdnvNavigation.Tennv.Contains(searchString))
                    .GroupBy(x => x.Idpnk) // Nhóm theo Idpnk
                    .Select(g => g.First())
                    .ToListAsync();
                xeDaNhap = await _context.Chitietxes
                    .Include(c => c.IdmxNavigation)
                    .Include(c => c.IdpnkNavigation)
                    .Include(c => c.IdpxkNavigation)
                    .Include(c => c.IdxeNavigation)
                    .OrderByDescending(x => x.Idct)
                    .Where(x => x.Active == true && x.Idpnk != null 
                        && x.IdpnkNavigation.Mapnk.Contains(searchString) 
                        || x.IdmxNavigation.Tenmx.Contains(searchString) 
                        || x.IdxeNavigation.Tenxe.Contains(searchString) 
                        || x.Biensolucmua.Contains(searchString))
                    .ToListAsync();
                //if (phieunhapkho.Any() == false)
                //{
                //    TempData["AlertpnkMessage"] = "";
                //}
                //if (phieunhapkhoxecu.Any() == false)
                //{
                //    TempData["AlertpnkcMessagetk"] = "";
                //}
                //if (xeDaNhap.Any() == false)
                //{
                //    TempData["AlertxdnMessage"] = "";
                //}
            }

            var fdate = fromDate.ToString("ddMMyyyy");
            var tdate = toDate.ToString("ddMMyyyy");
            if (fdate != "01010001" && tdate != "01010001" && Int32.Parse(fdate) <= Int32.Parse(tdate))
            {
                phieunhapkho = await _context.Chitietxes
                    .Include(c => c.IdmxNavigation)
                    .Include(c => c.IdpnkNavigation)
                    .Include(c => c.IdpnkNavigation.IdnccNavigation)
                    .Include(c => c.IdpnkNavigation.IdnvNavigation)
                    .Include(c => c.IdpxkNavigation)
                    .Include(c => c.IdxeNavigation)
                    .OrderByDescending(x => x.IdpnkNavigation.Ngaynhap)
                    .Where(x => x.Active == true && x.Idpnk != null && x.Trangthai == "Mới"
                        && (x.IdpnkNavigation.Ngaynhap >= fromDate) && (x.IdpnkNavigation.Ngaynhap <= toDate))
                    .GroupBy(x => x.Idpnk) // Nhóm theo Idpnk
                    .Select(g => g.First())
                    .ToListAsync();
                phieunhapkhoxecu = await _context.Chitietxes
                    .Include(c => c.IdmxNavigation)
                    .Include(c => c.IdpnkNavigation)
                    .Include(c => c.IdpnkNavigation.IdnccNavigation)
                    .Include(c => c.IdpnkNavigation.IdnvNavigation)
                    .Include(c => c.IdpxkNavigation)
                    .Include(c => c.IdxeNavigation)
                    .OrderByDescending(x => x.IdpnkNavigation.Ngaynhap)
                    .Where(x => x.Active == true && x.Idpnk != null && x.Trangthai == "Cũ"
                        && (x.IdpnkNavigation.Ngaynhap >= fromDate) && (x.IdpnkNavigation.Ngaynhap <= toDate))
                    .GroupBy(x => x.Idpnk) // Nhóm theo Idpnk
                    .Select(g => g.First())
                    .ToListAsync();
                //if (phieunhapkho.Any() == false)
                //{
                //    TempData["AlertpnkMessage"] = "";
                //}
                //if (phieunhapkhoxecu.Any() == false)
                //{
                //    TempData["AlertpnkcMessagetk"] = "";
                //}
            }
            if (trangthai == true && selectncc != null)
            {
                phieunhapkho = await _context.Chitietxes
                    .Include(c => c.IdmxNavigation)
                    .Include(c => c.IdpnkNavigation)
                    .Include(c => c.IdpnkNavigation.IdnccNavigation)
                    .Include(c => c.IdpnkNavigation.IdnvNavigation)
                    .Include(c => c.IdpxkNavigation)
                    .Include(c => c.IdxeNavigation)
                    .Where(x => x.IdpnkNavigation.Active == true && x.Idpnk != null && x.Trangthai == "Mới"
                        && x.IdpnkNavigation.Idncc == selectncc && x.IdpnkNavigation.Dachi == true && x.IdpnkNavigation.Idpc !=null && x.IdpnkNavigation.Idncc!=null && x.IdpnkNavigation.Idnv!=null && x.IdpnkNavigation.Tongtiennhap!=0 && x.IdpnkNavigation.Tongtiennhap!=null)
                    .GroupBy(x => x.Idpnk) // Nhóm theo Idpnk
                    .Select(g => g.First())
                    .ToListAsync();
                phieunhapkhoxecu = await _context.Chitietxes
                    .Include(c => c.IdmxNavigation)
                    .Include(c => c.IdpnkNavigation)
                    .Include(c => c.IdpnkNavigation.IdnccNavigation)
                    .Include(c => c.IdpnkNavigation.IdnvNavigation)
                    .Include(c => c.IdpxkNavigation)
                    .Include(c => c.IdxeNavigation)
                    .OrderByDescending(x => x.IdpnkNavigation.Ngaynhap)
                    .Where(x => x.IdpnkNavigation.Active == true && x.Idpnk != null && x.Trangthai == "Cũ"
                        && x.IdpnkNavigation.Idncc == selectncc && x.IdpnkNavigation.Dachi == true && x.IdpnkNavigation.Idpc != null && x.IdpnkNavigation.Idncc != null && x.IdpnkNavigation.Idnv != null && x.IdpnkNavigation.Tongtiennhap != 0 && x.IdpnkNavigation.Tongtiennhap != null)
                    .GroupBy(x => x.Idpnk) // Nhóm theo Idpnk
                    .Select(g => g.First())
                    .ToListAsync();
                tongTien = 0;

                foreach (var tienphieunhap in phieunhapkho)
                {
                    tongTien += tienphieunhap.IdpnkNavigation.Tongtiennhap;
                }
                tongTienNhapXeCu = 0;

                foreach (var tienphieunhapxecu in phieunhapkhoxecu)
                {
                    tongTienNhapXeCu += tienphieunhapxecu.IdpnkNavigation.Tongtiennhap;
                }
            }
            else if (trangthai == false && selectncc != null)
            {
                phieunhapkho = await _context.Chitietxes
                    .Include(c => c.IdmxNavigation)
                    .Include(c => c.IdpnkNavigation)
                    .Include(c => c.IdpnkNavigation.IdnccNavigation)
                    .Include(c => c.IdpnkNavigation.IdnvNavigation)
                    .Include(c => c.IdpxkNavigation)
                    .Include(c => c.IdxeNavigation)
                    .Where(x => x.IdpnkNavigation.Active == true && x.Idpnk != null && x.Trangthai == "Mới"
                        && x.IdpnkNavigation.Idncc == selectncc && x.IdpnkNavigation.Dachi == false && x.IdpnkNavigation.Idpc != null && x.IdpnkNavigation.Idncc != null && x.IdpnkNavigation.Idnv != null && x.IdpnkNavigation.Tongtiennhap != 0 && x.IdpnkNavigation.Tongtiennhap != null)
                    .GroupBy(x => x.Idpnk) // Nhóm theo Idpnk
                    .Select(g => g.First())
                    .ToListAsync();
                phieunhapkhoxecu = await _context.Chitietxes
                    .Include(c => c.IdmxNavigation)
                    .Include(c => c.IdpnkNavigation)
                    .Include(c => c.IdpnkNavigation.IdnccNavigation)
                    .Include(c => c.IdpnkNavigation.IdnvNavigation)
                    .Include(c => c.IdpxkNavigation)
                    .Include(c => c.IdxeNavigation)
                    .OrderByDescending(x => x.IdpnkNavigation.Ngaynhap)
                    .Where(x => x.IdpnkNavigation.Active == true && x.Idpnk != null && x.Trangthai == "Cũ"
                        && x.IdpnkNavigation.Idncc == selectncc && x.IdpnkNavigation.Dachi == false && x.IdpnkNavigation.Idpc != null && x.IdpnkNavigation.Idncc != null && x.IdpnkNavigation.Idnv != null && x.IdpnkNavigation.Tongtiennhap != 0 && x.IdpnkNavigation.Tongtiennhap != null)
                    .GroupBy(x => x.Idpnk) // Nhóm theo Idpnk
                    .Select(g => g.First())
                    .ToListAsync();
                tongTien = 0;

                foreach (var tienphieunhap in phieunhapkho)
                {
                    tongTien += tienphieunhap.IdpnkNavigation.Tongtiennhap;
                }
                tongTienNhapXeCu = 0;

                foreach (var tienphieunhapxecu in phieunhapkhoxecu)
                {
                    tongTienNhapXeCu += tienphieunhapxecu.IdpnkNavigation.Tongtiennhap;
                }
            }
            else if (trangthai == null && selectncc != null)
            {
                phieunhapkho = await _context.Chitietxes
                    .Include(c => c.IdmxNavigation)
                    .Include(c => c.IdpnkNavigation)
                    .Include(c => c.IdpnkNavigation.IdnccNavigation)
                    .Include(c => c.IdpnkNavigation.IdnvNavigation)
                    .Include(c => c.IdpxkNavigation)
                    .Include(c => c.IdxeNavigation)
                    .Where(x => x.IdpnkNavigation.Active == true && x.Idpnk != null && x.Trangthai == "Mới"
                        && x.IdpnkNavigation.Idncc == selectncc && x.IdpnkNavigation.Idpc != null && x.IdpnkNavigation.Idncc != null && x.IdpnkNavigation.Idnv != null && x.IdpnkNavigation.Tongtiennhap != 0 && x.IdpnkNavigation.Tongtiennhap != null)
                    .GroupBy(x => x.Idpnk) // Nhóm theo Idpnk
                    .Select(g => g.First())
                    .ToListAsync();
                phieunhapkhoxecu = await _context.Chitietxes
                    .Include(c => c.IdmxNavigation)
                    .Include(c => c.IdpnkNavigation)
                    .Include(c => c.IdpnkNavigation.IdnccNavigation)
                    .Include(c => c.IdpnkNavigation.IdnvNavigation)
                    .Include(c => c.IdpxkNavigation)
                    .Include(c => c.IdxeNavigation)
                    .OrderByDescending(x => x.IdpnkNavigation.Ngaynhap)
                    .Where(x => x.IdpnkNavigation.Active == true && x.Idpnk != null && x.Trangthai == "Cũ"
                        && x.IdpnkNavigation.Idncc == selectncc && x.IdpnkNavigation.Idpc != null && x.IdpnkNavigation.Idncc != null && x.IdpnkNavigation.Idnv != null && x.IdpnkNavigation.Tongtiennhap != 0 && x.IdpnkNavigation.Tongtiennhap != null)
                    .GroupBy(x => x.Idpnk) // Nhóm theo Idpnk
                    .Select(g => g.First())
                    .ToListAsync();
                tongTien = 0;

                foreach (var tienphieunhap in phieunhapkho)
                {
                    tongTien += tienphieunhap.IdpnkNavigation.Tongtiennhap;
                }
                tongTienNhapXeCu = 0;

                foreach (var tienphieunhapxecu in phieunhapkhoxecu)
                {
                    tongTienNhapXeCu += tienphieunhapxecu.IdpnkNavigation.Tongtiennhap;
                }
            }
            ViewBag.phieuNhapKho=phieunhapkho;
            ViewBag.phieuNhapKhoXeCu = phieunhapkhoxecu;
            ViewBag.xeDaNhap=xeDaNhap;
            ViewBag.tongTien = tongTien;
            ViewBag.tongTienNhapXeCu = tongTienNhapXeCu;
            return View();
        }


        public async Task<IActionResult> Index(string searchString, DateTime fromDate, DateTime toDate, int? selectncc, bool? trangthai)
        {
            var phieunhapkho = await _context.Chitietxes
                .Include(c => c.IdmxNavigation)
                .Include(c => c.IdpnkNavigation)
                .Include(c => c.IdpnkNavigation.IdnccNavigation)
                .Include(c => c.IdpnkNavigation.IdnvNavigation)
                .Include(c => c.IdpxkNavigation)
                .Include(c => c.IdxeNavigation)
                .OrderByDescending(x => x.Idpnk)
                .Where(x => x.Active == true && x.Idpnk != null && x.Trangthai == "Mới")
                .GroupBy(x => x.Idpnk) // Nhóm theo Idpnk
                .Select(g => g.First())
                .ToListAsync();
            
            double? tongTien = 0;

            foreach (var tienphieunhap in phieunhapkho)
            {
                tongTien += tienphieunhap.IdpnkNavigation.Tongtiennhap;
            }
            
            TempData["Nhacungcap"] = _context.Nhacungcaps.Where(x => x.Active == true).ToList();
            if (!string.IsNullOrEmpty(searchString))
            {
                phieunhapkho = await _context.Chitietxes
                    .Include(c => c.IdmxNavigation)
                    .Include(c => c.IdpnkNavigation)
                    .Include(c => c.IdpnkNavigation.IdnccNavigation)
                    .Include(c => c.IdpnkNavigation.IdnvNavigation)
                    .Include(c => c.IdpxkNavigation)
                    .Include(c => c.IdxeNavigation)
                    .OrderByDescending(x => x.Idpnk)
                    .Where(x => x.Active == true && x.Idpnk != null && x.Trangthai == "Mới"
                        && x.IdpnkNavigation.Mapnk.Contains(searchString) || x.IdpnkNavigation.IdnccNavigation.Tenncc.Contains(searchString)
                        || x.IdpnkNavigation.IdnvNavigation.Tennv.Contains(searchString))
                    .GroupBy(x => x.Idpnk) // Nhóm theo Idpnk
                    .Select(g => g.First())
                    .ToListAsync();
            }

            var fdate = fromDate.ToString("ddMMyyyy");
            var tdate = toDate.ToString("ddMMyyyy");
            if (fdate != "01010001" && tdate != "01010001" && Int32.Parse(fdate) <= Int32.Parse(tdate))
            {
                phieunhapkho = await _context.Chitietxes
                    .Include(c => c.IdmxNavigation)
                    .Include(c => c.IdpnkNavigation)
                    .Include(c => c.IdpnkNavigation.IdnccNavigation)
                    .Include(c => c.IdpnkNavigation.IdnvNavigation)
                    .Include(c => c.IdpxkNavigation)
                    .Include(c => c.IdxeNavigation)
                    .OrderByDescending(x => x.IdpnkNavigation.Ngaynhap)
                    .Where(x => x.Active == true && x.Idpnk != null && x.Trangthai == "Mới"
                        && (x.IdpnkNavigation.Ngaynhap >= fromDate) && (x.IdpnkNavigation.Ngaynhap <= toDate))
                    .GroupBy(x => x.Idpnk) // Nhóm theo Idpnk
                    .Select(g => g.First())
                    .ToListAsync();
            }
            if (trangthai == true && selectncc != null)
            {
                phieunhapkho = await _context.Chitietxes
                    .Include(c => c.IdmxNavigation)
                    .Include(c => c.IdpnkNavigation)
                    .Include(c => c.IdpnkNavigation.IdnccNavigation)
                    .Include(c => c.IdpnkNavigation.IdnvNavigation)
                    .Include(c => c.IdpxkNavigation)
                    .Include(c => c.IdxeNavigation)
                    .Where(x => x.IdpnkNavigation.Active == true && x.Idpnk != null && x.Trangthai == "Mới"
                        && x.IdpnkNavigation.Idncc == selectncc && x.IdpnkNavigation.Dachi == true && x.IdpnkNavigation.Idpc != null && x.IdpnkNavigation.Idncc != null && x.IdpnkNavigation.Idnv != null && x.IdpnkNavigation.Tongtiennhap != 0 && x.IdpnkNavigation.Tongtiennhap != null)
                    .GroupBy(x => x.Idpnk) // Nhóm theo Idpnk
                    .Select(g => g.First())
                    .ToListAsync();
                tongTien = 0;

                foreach (var tienphieunhap in phieunhapkho)
                {
                    tongTien += tienphieunhap.IdpnkNavigation.Tongtiennhap;
                }
            }
            else if (trangthai == false && selectncc != null)
            {
                phieunhapkho = await _context.Chitietxes
                    .Include(c => c.IdmxNavigation)
                    .Include(c => c.IdpnkNavigation)
                    .Include(c => c.IdpnkNavigation.IdnccNavigation)
                    .Include(c => c.IdpnkNavigation.IdnvNavigation)
                    .Include(c => c.IdpxkNavigation)
                    .Include(c => c.IdxeNavigation)
                    .Where(x => x.IdpnkNavigation.Active == true && x.Idpnk != null && x.Trangthai == "Mới"
                        && x.IdpnkNavigation.Idncc == selectncc && x.IdpnkNavigation.Dachi == false && x.IdpnkNavigation.Idpc != null && x.IdpnkNavigation.Idncc != null && x.IdpnkNavigation.Idnv != null && x.IdpnkNavigation.Tongtiennhap != 0 && x.IdpnkNavigation.Tongtiennhap != null)
                    .GroupBy(x => x.Idpnk) // Nhóm theo Idpnk
                    .Select(g => g.First())
                    .ToListAsync();
                tongTien = 0;

                foreach (var tienphieunhap in phieunhapkho)
                {
                    tongTien += tienphieunhap.IdpnkNavigation.Tongtiennhap;
                }
            }
            else if (trangthai == null && selectncc != null)
            {
                phieunhapkho = await _context.Chitietxes
                    .Include(c => c.IdmxNavigation)
                    .Include(c => c.IdpnkNavigation)
                    .Include(c => c.IdpnkNavigation.IdnccNavigation)
                    .Include(c => c.IdpnkNavigation.IdnvNavigation)
                    .Include(c => c.IdpxkNavigation)
                    .Include(c => c.IdxeNavigation)
                    .Where(x => x.IdpnkNavigation.Active == true && x.Idpnk != null && x.Trangthai == "Mới"
                        && x.IdpnkNavigation.Idncc == selectncc && x.IdpnkNavigation.Idpc != null && x.IdpnkNavigation.Idncc != null && x.IdpnkNavigation.Idnv != null && x.IdpnkNavigation.Tongtiennhap != 0 && x.IdpnkNavigation.Tongtiennhap != null)
                    .GroupBy(x => x.Idpnk) // Nhóm theo Idpnk
                    .Select(g => g.First())
                    .ToListAsync();
                
                tongTien = 0;

                foreach (var tienphieunhap in phieunhapkho)
                {
                    tongTien += tienphieunhap.IdpnkNavigation.Tongtiennhap;
                }
            }
            ViewBag.phieuNhapKho = phieunhapkho;
            ViewBag.tongTien = tongTien;
            return View();
        }
        public async Task<IActionResult> IndexXeCu(string searchString, DateTime fromDate, DateTime toDate, int? selectncc, bool? trangthai)
        {
            
            var phieunhapkhoxecu = await _context.Chitietxes
                .Include(c => c.IdmxNavigation)
                .Include(c => c.IdpnkNavigation)
                .Include(c => c.IdpnkNavigation.IdnccNavigation)
                .Include(c => c.IdpnkNavigation.IdnvNavigation)
                .Include(c => c.IdpxkNavigation)
                .Include(c => c.IdxeNavigation)
                .OrderByDescending(x => x.Idpnk)
                .Where(x => x.Active == true && x.Idpnk != null && x.Trangthai == "Cũ")
                .GroupBy(x => x.Idpnk) // Nhóm theo Idpnk
                .Select(g => g.First())
                .ToListAsync();
            
            
            double? tongTienNhapXeCu = 0;

            foreach (var tienphieunhapxecu in phieunhapkhoxecu)
            {
                tongTienNhapXeCu += tienphieunhapxecu.IdpnkNavigation.Tongtiennhap;
            }
            TempData["Nhacungcap"] = _context.Nhacungcaps.Where(x => x.Active == true).ToList();
            if (!string.IsNullOrEmpty(searchString))
            {
                
                phieunhapkhoxecu = await _context.Chitietxes
                    .Include(c => c.IdmxNavigation)
                    .Include(c => c.IdpnkNavigation)
                    .Include(c => c.IdpnkNavigation.IdnccNavigation)
                    .Include(c => c.IdpnkNavigation.IdnvNavigation)
                    .Include(c => c.IdpxkNavigation)
                    .Include(c => c.IdxeNavigation)
                    .OrderByDescending(x => x.Idpnk)
                    .Where(x => x.Active == true && x.Idpnk != null && x.Trangthai == "Cũ"
                        && x.IdpnkNavigation.Mapnk.Contains(searchString) || x.IdpnkNavigation.IdnccNavigation.Tenncc.Contains(searchString)
                        || x.IdpnkNavigation.IdnvNavigation.Tennv.Contains(searchString))
                    .GroupBy(x => x.Idpnk) // Nhóm theo Idpnk
                    .Select(g => g.First())
                    .ToListAsync();
                
            }

            var fdate = fromDate.ToString("ddMMyyyy");
            var tdate = toDate.ToString("ddMMyyyy");
            if (fdate != "01010001" && tdate != "01010001" && Int32.Parse(fdate) <= Int32.Parse(tdate))
            {
                
                phieunhapkhoxecu = await _context.Chitietxes
                    .Include(c => c.IdmxNavigation)
                    .Include(c => c.IdpnkNavigation)
                    .Include(c => c.IdpnkNavigation.IdnccNavigation)
                    .Include(c => c.IdpnkNavigation.IdnvNavigation)
                    .Include(c => c.IdpxkNavigation)
                    .Include(c => c.IdxeNavigation)
                    .OrderByDescending(x => x.IdpnkNavigation.Ngaynhap)
                    .Where(x => x.Active == true && x.Idpnk != null && x.Trangthai == "Cũ"
                        && (x.IdpnkNavigation.Ngaynhap >= fromDate) && (x.IdpnkNavigation.Ngaynhap <= toDate))
                    .GroupBy(x => x.Idpnk) // Nhóm theo Idpnk
                    .Select(g => g.First())
                    .ToListAsync();
                
            }
            if (trangthai == true && selectncc != null)
            {
                
                phieunhapkhoxecu = await _context.Chitietxes
                    .Include(c => c.IdmxNavigation)
                    .Include(c => c.IdpnkNavigation)
                    .Include(c => c.IdpnkNavigation.IdnccNavigation)
                    .Include(c => c.IdpnkNavigation.IdnvNavigation)
                    .Include(c => c.IdpxkNavigation)
                    .Include(c => c.IdxeNavigation)
                    .OrderByDescending(x => x.IdpnkNavigation.Ngaynhap)
                    .Where(x => x.IdpnkNavigation.Active == true && x.Idpnk != null && x.Trangthai == "Cũ"
                        && x.IdpnkNavigation.Idncc == selectncc && x.IdpnkNavigation.Dachi == true && x.IdpnkNavigation.Idpc != null && x.IdpnkNavigation.Idncc != null && x.IdpnkNavigation.Idnv != null && x.IdpnkNavigation.Tongtiennhap != 0 && x.IdpnkNavigation.Tongtiennhap != null)
                    .GroupBy(x => x.Idpnk) // Nhóm theo Idpnk
                    .Select(g => g.First())
                    .ToListAsync();
                
                tongTienNhapXeCu = 0;

                foreach (var tienphieunhapxecu in phieunhapkhoxecu)
                {
                    tongTienNhapXeCu += tienphieunhapxecu.IdpnkNavigation.Tongtiennhap;
                }
            }
            else if (trangthai == false && selectncc != null)
            {
                
                phieunhapkhoxecu = await _context.Chitietxes
                    .Include(c => c.IdmxNavigation)
                    .Include(c => c.IdpnkNavigation)
                    .Include(c => c.IdpnkNavigation.IdnccNavigation)
                    .Include(c => c.IdpnkNavigation.IdnvNavigation)
                    .Include(c => c.IdpxkNavigation)
                    .Include(c => c.IdxeNavigation)
                    .OrderByDescending(x => x.IdpnkNavigation.Ngaynhap)
                    .Where(x => x.IdpnkNavigation.Active == true && x.Idpnk != null && x.Trangthai == "Cũ"
                        && x.IdpnkNavigation.Idncc == selectncc && x.IdpnkNavigation.Dachi == false && x.IdpnkNavigation.Idpc != null && x.IdpnkNavigation.Idncc != null && x.IdpnkNavigation.Idnv != null && x.IdpnkNavigation.Tongtiennhap != 0 && x.IdpnkNavigation.Tongtiennhap != null)
                    .GroupBy(x => x.Idpnk) // Nhóm theo Idpnk
                    .Select(g => g.First())
                    .ToListAsync();
                
                tongTienNhapXeCu = 0;

                foreach (var tienphieunhapxecu in phieunhapkhoxecu)
                {
                    tongTienNhapXeCu += tienphieunhapxecu.IdpnkNavigation.Tongtiennhap;
                }
            }
            else if (trangthai == null && selectncc != null)
            {
                
                phieunhapkhoxecu = await _context.Chitietxes
                    .Include(c => c.IdmxNavigation)
                    .Include(c => c.IdpnkNavigation)
                    .Include(c => c.IdpnkNavigation.IdnccNavigation)
                    .Include(c => c.IdpnkNavigation.IdnvNavigation)
                    .Include(c => c.IdpxkNavigation)
                    .Include(c => c.IdxeNavigation)
                    .OrderByDescending(x => x.IdpnkNavigation.Ngaynhap)
                    .Where(x => x.IdpnkNavigation.Active == true && x.Idpnk != null && x.Trangthai == "Cũ"
                        && x.IdpnkNavigation.Idncc == selectncc && x.IdpnkNavigation.Idpc != null && x.IdpnkNavigation.Idncc != null && x.IdpnkNavigation.Idnv != null && x.IdpnkNavigation.Tongtiennhap != 0 && x.IdpnkNavigation.Tongtiennhap != null)
                    .GroupBy(x => x.Idpnk) // Nhóm theo Idpnk
                    .Select(g => g.First())
                    .ToListAsync();
                
                tongTienNhapXeCu = 0;

                foreach (var tienphieunhapxecu in phieunhapkhoxecu)
                {
                    tongTienNhapXeCu += tienphieunhapxecu.IdpnkNavigation.Tongtiennhap;
                }
            }
            ViewBag.phieuNhapKhoXeCu = phieunhapkhoxecu;
            ViewBag.tongTienNhapXeCu = tongTienNhapXeCu;
            return View();
        }
        public async Task<IActionResult> IndexXeDaNhap(string searchString, int? selectxe, int? selectmauxe)
        {
            
            var xeDaNhap = await _context.Chitietxes
                .Include(c => c.IdmxNavigation)
                .Include(c => c.IdpnkNavigation)
                .Include(c => c.IdpnkNavigation.IdnccNavigation)
                .Include(c => c.IdpxkNavigation)
                .Include(c => c.IdxeNavigation)
                .OrderByDescending(x => x.Idct)
                .Where(x => (x.Active == true && x.Idpnk != null))
                .ToListAsync();
            
            if (!string.IsNullOrEmpty(searchString))
            {
                xeDaNhap = await _context.Chitietxes
                    .Include(c => c.IdmxNavigation)
                    .Include(c => c.IdpnkNavigation)
                    .Include(c => c.IdpnkNavigation.IdnccNavigation)
                    .Include(c => c.IdpxkNavigation)
                    .Include(c => c.IdxeNavigation)
                    .OrderByDescending(x => x.Idct)
                    .Where(x => x.Active == true && x.Idpnk != null
                        && x.IdpnkNavigation.Mapnk.Contains(searchString)
                        || x.IdpnkNavigation.IdnccNavigation.Tenncc.Contains(searchString)
                        || x.IdmxNavigation.Tenmx.Contains(searchString)
                        || x.IdxeNavigation.Tenxe.Contains(searchString)
                        || x.Sokhung.Contains(searchString)
                        || x.Somay.Contains(searchString)
                        || x.Biensolucmua.Contains(searchString))
                    .ToListAsync();
            }
            TempData["Xe"] = _context.Xes.Where(x => x.Active == true).ToList();
            TempData["MauXe"] = _context.Mauxes.Where(x => x.Active == true).ToList();
            if (selectxe!=null && selectmauxe != null)
            {
                xeDaNhap = await _context.Chitietxes
                    .Include(c => c.IdmxNavigation)
                    .Include(c => c.IdpnkNavigation)
                    .Include(c => c.IdpnkNavigation.IdnccNavigation)
                    .Include(c => c.IdpnkNavigation.IdnvNavigation)
                    .Include(c => c.IdpxkNavigation)
                    .Include(c => c.IdxeNavigation)
                    .OrderByDescending(x => x.IdpnkNavigation.Ngaynhap)
                    .Where(x => x.IdpnkNavigation.Active == true && x.Idpnk != null
                        && x.IdxeNavigation.Idxe == selectxe && x.IdmxNavigation.Idmx == selectmauxe && x.IdpnkNavigation.Idpc != null && x.IdpnkNavigation.Idncc != null && x.IdpnkNavigation.Idnv != null)
                    .GroupBy(x => x.Idpnk) // Nhóm theo Idpnk
                    .Select(g => g.First())
                    .ToListAsync();
            }
            else if (selectxe != null && selectmauxe == null)
            {

                xeDaNhap = await _context.Chitietxes
                    .Include(c => c.IdmxNavigation)
                    .Include(c => c.IdpnkNavigation)
                    .Include(c => c.IdpnkNavigation.IdnccNavigation)
                    .Include(c => c.IdpnkNavigation.IdnvNavigation)
                    .Include(c => c.IdpxkNavigation)
                    .Include(c => c.IdxeNavigation)
                    .OrderByDescending(x => x.IdpnkNavigation.Ngaynhap)
                    .Where(x => x.IdpnkNavigation.Active == true && x.Idpnk != null
                        && x.IdxeNavigation.Idxe == selectxe && x.IdpnkNavigation.Idpc != null && x.IdpnkNavigation.Idncc != null && x.IdpnkNavigation.Idnv != null)
                    .GroupBy(x => x.Idpnk) // Nhóm theo Idpnk
                    .Select(g => g.First())
                    .ToListAsync();
            }
            else if (selectxe == null && selectmauxe != null)
            {
                xeDaNhap = await _context.Chitietxes
                    .Include(c => c.IdmxNavigation)
                    .Include(c => c.IdpnkNavigation)
                    .Include(c => c.IdpnkNavigation.IdnccNavigation)
                    .Include(c => c.IdpnkNavigation.IdnvNavigation)
                    .Include(c => c.IdpxkNavigation)
                    .Include(c => c.IdxeNavigation)
                    .OrderByDescending(x => x.IdpnkNavigation.Ngaynhap)
                    .Where(x => x.IdpnkNavigation.Active == true && x.Idpnk != null
                        && x.IdmxNavigation.Idmx == selectmauxe && x.IdpnkNavigation.Idpc != null && x.IdpnkNavigation.Idncc != null && x.IdpnkNavigation.Idnv != null)
                    .GroupBy(x => x.Idpnk) // Nhóm theo Idpnk
                    .Select(g => g.First())
                    .ToListAsync();
            }
            ViewBag.xeDaNhap = xeDaNhap;
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            string Sophieutudong = TaoSoPhieu();
            ViewData["idtd"] = Sophieutudong;
            TempData["Tenxe"] = _context.Xes.Where(x => x.Active == true).ToList();

            TempData["Nhacungcap"] = _context.Nhacungcaps.Where(x => x.Active == true).OrderByDescending(x=>x.Idncc).ToList();
            TempData["Nhanvien"] = _context.Nhanviens.Where(x => x.Active == true).ToList();
            Phieunhapkho phieunhapkho = new Phieunhapkho();
            phieunhapkho.Chitietxes.Add(new Chitietxe() { Idct = 1 });
            return View(phieunhapkho);
        }
        [HttpGet]
        public JsonResult GetColorsByXe(int Idxe)
        {
            var colors = _context.Mauxes
                .Where(c => _context.Ndxemauxes.Any(x => x.Idxe == Idxe && x.Idmx == c.Idmx))
                .ToList();

            var colorOptions = colors.Select(c => new { Idmx = c.Idmx, TenMau = c.Tenmx });

            return Json(colorOptions);
        }



        [HttpPost]
        public IActionResult Create(Phieunhapkho phieunhapkho)
        {
            try
            {
                phieunhapkho.Chitietxes.RemoveAll(n => (n.Active == false) || (n.Gianhap == 0) || (n.Sokhung == null) || (n.Somay == null));

                if (phieunhapkho.Chitietxes.Count() == 0)
                {
                    
                    TempData["AlertErrorMessage"] = "";
                    return RedirectToAction("Index");
                }
                else
                {
                    phieunhapkho.Chitietxes.RemoveAll(n => (n.Active == false) || (n.Gianhap == 0) || (n.Sokhung == null) || (n.Somay == null));
                    // Lấy dữ liệu ngày hiện tại
                    phieunhapkho.Ngaynhap = DateTime.Now;
                    phieunhapkho.Dachi = false;
                    phieunhapkho.Active = true;
                    _context.Add(phieunhapkho);
                    _context.SaveChanges();
                    TempData["AlertSuccessMessage"] = phieunhapkho.Mapnk;
                    return RedirectToAction("Index");
                }
            }
            catch
            {
                TempData["AlertErrorMessage"] = "";
                return RedirectToAction("Index");
            }
        }
        [HttpGet]
        public IActionResult CreateXeCu()
        {
            string Sophieutudong = TaoSoPhieu();
            ViewData["idtd"] = Sophieutudong;
            TempData["Tenxe"] = _context.Xes.Where(x => x.Active == true).ToList();

            TempData["Nhacungcap"] = _context.Nhacungcaps.Where(x => x.Active == true).OrderByDescending(x=>x.Idncc).ToList();
            TempData["Nhanvien"] = _context.Nhanviens.Where(x => x.Active == true).ToList();
            Phieunhapkho phieunhapkho = new Phieunhapkho();
            phieunhapkho.Chitietxes.Add(new Chitietxe() { Idct = 1 });
            return View(phieunhapkho);
        }
        [HttpPost]
        public IActionResult CreateXeCu(Phieunhapkho phieunhapkho)
        {
            try
            {
                phieunhapkho.Chitietxes.RemoveAll(n => (n.Active == false) || (n.Gianhap == 0) || (n.Sokhung == null) || (n.Somay == null));
                if (phieunhapkho.Chitietxes.Count() == 0)
                {
                    TempData["AlertErrorMessage"] = "";
                    return RedirectToAction("IndexXeCu");
                }
                else
                {
                    phieunhapkho.Chitietxes.RemoveAll(n => (n.Active == false) || (n.Gianhap == 0) || (n.Sokhung == null) || (n.Somay == null));
                    // Lấy dữ liệu ngày hiện tại
                    phieunhapkho.Ngaynhap = DateTime.Now;
                    phieunhapkho.Dachi = false;
                    phieunhapkho.Active = true;
                    _context.Add(phieunhapkho);
                    _context.SaveChanges();
                    TempData["AlertSuccessMessage"] = phieunhapkho.Mapnk;
                    return RedirectToAction("IndexXeCu");
                }
            }
            catch
            {
                TempData["AlertErrorMessage"] = "";
                return RedirectToAction("Index");
            }
        }
        [HttpGet]
        public IActionResult CreateNCC()
        {
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateNCC([Bind("Idncc,Mancc,Tenncc,Diachincc,Dienthoaincc,Cccdncc,Email,Quequan,Masothuencc,Ghichu,Active")] Nhacungcap nhacungcap)
        {
            try { 
                if (ModelState.IsValid)
                {
                    nhacungcap.Active = true;
                    _context.Add(nhacungcap);
                    await _context.SaveChangesAsync();
                    TempData["AlertSuccessNCCMessage"] = nhacungcap.Mancc;
                    return RedirectToAction(nameof(Create));
                }
                return View(nhacungcap);
            }
            catch
            {
                TempData["AlertErrorMessage"] = "";
                return RedirectToAction("Index");
            }
        }
        [HttpGet]
        public IActionResult CreateNCCXeCu()
        {
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateNCCXeCu([Bind("Idncc,Mancc,Tenncc,Diachincc,Dienthoaincc,Cccdncc,Email,Quequan,Masothuencc,Ghichu,Active")] Nhacungcap nhacungcap)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    nhacungcap.Active = true;
                    _context.Add(nhacungcap);
                    await _context.SaveChangesAsync();
                    TempData["AlertSuccessNCCXeCuMessage"] = nhacungcap.Mancc;
                    return RedirectToAction(nameof(CreateXeCu));
                }
                return View(nhacungcap);
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
            var phieuCu = _context.Phieunhapkhos.OrderByDescending(x => x.Mapnk).FirstOrDefault(p => p.Mapnk.StartsWith($"NK{ngayThangNam}"));

            if (phieuCu != null)
            {
                // Tách lấy số phiếu hiện tại
                string soPhieuCu = phieuCu.Mapnk.Substring(8);
                int soPhieuMoi = int.Parse(soPhieuCu) + 1;
                return $"NK{ngayThangNam}{soPhieuMoi:D4}";
            }
            else
            {
                // Nếu không có phiếu cùng ngày, bắt đầu từ 1
                return $"NK{ngayThangNam}0001";
            }
        }
        [HttpGet]
        public IActionResult Details(int id)
        {
            try
            {
                TempData["Tenxe"] = _context.Xes.Where(x => x.Active == true).ToList();
                TempData["Tenmx"] = _context.Mauxes.Where(x => x.Active == true).ToList();
                TempData["Nhacungcap"] = _context.Nhacungcaps.Where(x => x.Active == true).ToList();
                TempData["Nhanvien"] = _context.Nhanviens.Where(x => x.Active == true).ToList();
                Phieunhapkho? pnk = _context.Phieunhapkhos
                    .Include(e => e.Chitietxes)
                    .Where(a => a.Idpnk == id).FirstOrDefault();

                return View(pnk);
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
                TempData["Nhacungcap"] = _context.Nhacungcaps.Where(x => x.Active == true).ToList();
                TempData["Nhanvien"] = _context.Nhanviens.Where(x => x.Active == true).ToList();
                Phieunhapkho? pnk = _context.Phieunhapkhos
                    .Include(e => e.Chitietxes)
                    .Where(a => a.Idpnk == id).FirstOrDefault();

                return View(pnk);
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        public IActionResult Delete(Phieunhapkho pnk)
        {
            try
            {

            
            Phieunhapkho? existingApplicant = _context.Phieunhapkhos
                .Include(e => e.Chitietxes)
                .FirstOrDefault(a => a.Idpnk == pnk.Idpnk);
            existingApplicant.Active = false;

            var ctxActive = _context.Chitietxes.Where(x => x.Idpnk == pnk.Idpnk).ToList();
            
            foreach (var item in ctxActive)
            {
                item.Active = false;
                item.Idpnk = null;
                item.Idxe = null;
                item.Idmx = null;
                item.Idpxk = null;
            }
            _context.Chitietxes.RemoveRange(ctxActive);
            _context.Phieunhapkhos.Remove(existingApplicant);
            _context.SaveChanges();
            TempData["AlertDeleteMessage"] = pnk.Mapnk;
                

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
            if (id == null)
            {
                return NotFound(); // Xử lý trường hợp id là null
            }
            TempData["Tenxe"] = _context.Xes.Where(x => x.Active == true).ToList();
            TempData["Tenmx"] = _context.Mauxes.Include(x=>x.Ndxemauxes).Where(x => x.Active == true).ToList();
            TempData["Nhacungcap"] = _context.Nhacungcaps.Where(x => x.Active == true).ToList();
            TempData["Nhanvien"] = _context.Nhanviens.Where(x => x.Active == true).ToList();
            Phieunhapkho? pnk = _context.Phieunhapkhos
                .Include(e => e.Chitietxes)
                .FirstOrDefault(a => a.Idpnk == id);

            if (pnk == null)
            {
                return NotFound(); // Xử lý trường hợp không tìm thấy ứng viên
            }

            return View(pnk);
        }



        [HttpPost]
        public IActionResult Edit(int id, Phieunhapkho pnk)
        {
            try { 
                if (id != pnk.Idpnk)
                {
                    return BadRequest(); // Xử lý trường hợp id không khớp
                }

                // Kiểm tra xem ứng viên có tồn tại trong cơ sở dữ liệu hay không
            
                Phieunhapkho? existingApplicant = _context.Phieunhapkhos
                    .Include(e => e.Chitietxes)
                    .FirstOrDefault(a => a.Idpnk == pnk.Idpnk);

                if (existingApplicant == null)
                {
                    return NotFound(); // Xử lý trường hợp không tìm thấy ứng viên
                }

                // Cập nhật thông tin của ứng viên
                existingApplicant.Mapnk = pnk.Mapnk; // Thay thế bằng các trường thông tin khác cần cập nhật
                existingApplicant.Ngaynhap = pnk.Ngaynhap;
                existingApplicant.Sohd = pnk.Sohd;
                existingApplicant.Ngayhd = pnk.Ngayhd;
                existingApplicant.Idncc = pnk.Idncc;
                existingApplicant.Idnv = pnk.Idnv;
                existingApplicant.Tongtiennhap = pnk.Tongtiennhap;
                existingApplicant.Active = true;

            
            
                List<Chitietxe> chitietxes = _context.Chitietxes.Where(x => x.Idpnk == pnk.Idpnk).ToList();
                _context.Chitietxes.RemoveRange(chitietxes);
                _context.SaveChanges();
                List<Chitietxe> chitietxes1 = new List<Chitietxe>();
                foreach (Chitietxe chitietxe in pnk.Chitietxes)
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
                    ct.Soluong = chitietxe.Soluong;
                    ct.Giaban = chitietxe.Giaban;
                    chitietxes1.Add(ct);
                }
                existingApplicant.Chitietxes.RemoveAll(n => (n.Active == false) || (n.Gianhap == 0) || (n.Sokhung == null));
                _context.Chitietxes.AddRange(chitietxes1);
                _context.SaveChanges();
                TempData["AlertEditMessage"] = pnk.Mapnk;
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
            try
            {

            
                if (id == null || _context.Phieunhapkhos == null)
                {
                    return NotFound();
                }

            
                var pnk = _context.Phieunhapkhos
                    .Include(p => p.IdnccNavigation)
                    .Include(p => p.IdnvNavigation)
                    .Where(x => x.Active == true && x.Idpnk == id).FirstOrDefault();

                var chitietxe = _context.Chitietxes
                    .Include(c => c.IdmxNavigation)
                    .Include(c => c.IdpnkNavigation)
                    .Include(c => c.IdpxkNavigation)
                    .Include(c => c.IdxeNavigation)
                    .Where(x => x.Active == true && x.Idpnk == id && x.Idmx != null && x.Idxe != null).ToList();
                ViewBag.ctx = chitietxe;

                if (pnk == null)
                {
                    return NotFound();
                }

                return new ViewAsPdf("DownPDF", pnk)
                {
                    FileName = $"Phieunhapkho {pnk.Mapnk}.pdf",
                    PageOrientation = Rotativa.AspNetCore.Options.Orientation.Portrait,
                    PageSize = Rotativa.AspNetCore.Options.Size.A4,
                    ViewData = new ViewDataDictionary(new EmptyModelMetadataProvider(), new ModelStateDictionary())
                    {
                        Model = pnk,
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
        public async Task<IActionResult> ExportExcelPN()
        {
            try
            {

            
            double? tongGianhap = 0;
            double? tongGiaban = 0;
            double? tienloi = 0;
            var phieuNhapXeMoi = await _context.Chitietxes
                .Include(c => c.IdmxNavigation)
                .Include(c => c.IdpnkNavigation)
                .Include(c => c.IdpnkNavigation.IdnccNavigation)
                .Include(c => c.IdpnkNavigation.IdnvNavigation)
                .Include(c => c.IdpxkNavigation)
                .Include(c => c.IdpxkNavigation.IdkhNavigation)
                .Include(c => c.IdpxkNavigation.IdnvNavigation)
                .Include(c => c.IdxeNavigation)
                .Include(c => c.IdxeNavigation.IdlxNavigation)
                .Where(i => i.Active == true
                    && i.Idpnk != null
                    && i.Trangthai == "Mới"
                    && i.Idmx != null
                    && i.Idxe != null)
                .OrderByDescending(x => x.IdpnkNavigation.Mapnk)
                .ThenBy(x => x.IdxeNavigation.Tenxe)
                .ThenBy(x => x.IdmxNavigation.Tenmx)
                //.GroupBy(x => x.Idpnk)
                //.Select(g => g.First())
                .ToListAsync();

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Chitietxes");
                var currentRow = 3;
                var firstRow = 2;

                //worksheet.Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                worksheet.Cell(firstRow, 1).Value = "Danh sách các phiếu nhập kho xe mới";

                worksheet.Cell(currentRow, 2).Value = "Số phiếu nhập";
                worksheet.Cell(currentRow, 3).Value = "Ngày nhập";
                worksheet.Cell(currentRow, 4).Value = "Số phiếu bán";
                worksheet.Cell(currentRow, 5).Value = "Ngày bán";
                worksheet.Cell(currentRow, 6).Value = "Tên xe";
                worksheet.Cell(currentRow, 7).Value = "Màu xe";
                worksheet.Cell(currentRow, 8).Value = "Đời xe";
                worksheet.Cell(currentRow, 9).Value = "Số khung";
                worksheet.Cell(currentRow, 10).Value = "Số máy";
                worksheet.Cell(currentRow, 11).Value = "Tên nhà cung cấp";
                worksheet.Cell(currentRow, 12).Value = "Tên nhân viên nhập";
                worksheet.Cell(currentRow, 13).Value = "Tên nhà khách hàng mua";
                worksheet.Cell(currentRow, 14).Value = "Tên nhân viên bán";
                worksheet.Cell(currentRow, 15).Value = "Thời gian bảo hành";
                worksheet.Cell(currentRow, 16).Value = "Giá nhập";
                worksheet.Cell(currentRow, 17).Value = "Giá bán";


                foreach (var slt in phieuNhapXeMoi)
                {
                    currentRow++;
                    worksheet.Cell(currentRow, 2).Value = slt.IdpnkNavigation.Mapnk;
                    worksheet.Cell(currentRow, 3).Value = slt.IdpnkNavigation.Ngaynhap;
                    worksheet.Cell(currentRow, 3).Style.DateFormat.Format = "dd/MM/yyyy";

                    if (slt.IdpxkNavigation != null)
                    {
                        worksheet.Cell(currentRow, 4).Value = slt.IdpxkNavigation.Sopxk;
                        worksheet.Cell(currentRow, 5).Value = slt.IdpxkNavigation.Ngayxuat;
                        
                    }
                    else
                    {
                        worksheet.Cell(currentRow, 4).Value = "Chưa bán";
                        worksheet.Cell(currentRow, 5).Value = "Chưa bán";
                    }
                    worksheet.Cell(currentRow, 6).Value = slt.IdxeNavigation.Tenxe;
                    worksheet.Cell(currentRow, 7).Value = slt.IdmxNavigation.Tenmx;
                    worksheet.Cell(currentRow, 8).Value = slt.Doixe;
                    worksheet.Cell(currentRow, 9).Value = slt.Sokhung;
                    worksheet.Cell(currentRow, 10).Value = slt.Somay;
                    worksheet.Cell(currentRow, 11).Value = slt.IdpnkNavigation.IdnccNavigation.Tenncc;
                    worksheet.Cell(currentRow, 12).Value = slt.IdpnkNavigation.IdnvNavigation.Tennv;
                    if (slt.IdpxkNavigation != null && slt.IdpxkNavigation.IdkhNavigation != null)
                    {
                        worksheet.Cell(currentRow, 13).Value = slt.IdpxkNavigation.IdkhNavigation.Tenkh;
                    }
                    else
                    {
                        worksheet.Cell(currentRow, 13).Value = "Không có";
                    }

                    if (slt.IdpxkNavigation != null && slt.IdpxkNavigation.IdnvNavigation != null)
                    {
                        worksheet.Cell(currentRow, 14).Value = slt.IdpxkNavigation.IdnvNavigation.Tennv;
                    }
                    else
                    {
                        worksheet.Cell(currentRow, 14).Value = "Không có";
                    }

                    worksheet.Cell(currentRow, 15).Value = slt.Thoigianbaohanh;
                    worksheet.Cell(currentRow, 16).Value = slt.Gianhap;
                    worksheet.Cell(currentRow, 17).Value = slt.Giaban;

                    tongGianhap += slt.Gianhap;
                    tongGiaban += slt.Giaban;
                }
                currentRow+=3;
                worksheet.Cell(currentRow, 2).Value = "Tổng giá nhập";
                worksheet.Cell(currentRow, 3).Value = tongGianhap;
                currentRow++;
                worksheet.Cell(currentRow, 2).Value = "Tổng giá bán";
                worksheet.Cell(currentRow, 3).Value = tongGiaban;
                tienloi = tongGiaban - tongGianhap;
                currentRow++;
                worksheet.Cell(currentRow, 2).Value = "Tiền lời";
                worksheet.Cell(currentRow, 3).Value = tienloi;

                using (var stream = new MemoryStream())
                {
                    
                    var dateColumnHD = worksheet.Column(5);
                    dateColumnHD.Style.DateFormat.Format = "dd/MM/yyyy";
                    worksheet.Columns().AdjustToContents();
                    worksheet.RangeUsed().Style.Border.OutsideBorder = XLBorderStyleValues.Thick;
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Danhsachphieunhapkhoxemoi.xlsx");
                }
            }
            }
            catch
            {
                TempData["AlertErrorMessage"] = "";
                return RedirectToAction("Index");
            }
        }
        public async Task<IActionResult> ExportExcelPNOld()
        {
            try { 
            double? tongGianhap = 0;
            double? tongGiaban = 0;
            var phieuNhapXeCu = await _context.Chitietxes
                .Include(c => c.IdmxNavigation)
                .Include(c => c.IdpnkNavigation)
                .Include(c => c.IdpnkNavigation.IdnccNavigation)
                .Include(c => c.IdpnkNavigation.IdnvNavigation)
                .Include(c => c.IdpxkNavigation)
                .Include(c => c.IdpxkNavigation.IdkhNavigation)
                .Include(c => c.IdpxkNavigation.IdnvNavigation)
                .Include(c => c.IdxeNavigation)
                .Include(c => c.IdxeNavigation.IdlxNavigation)
                .Where(i => i.Active == true
                    && i.Idpnk != null
                    && i.Trangthai == "Cũ"
                    && i.Idmx != null
                    && i.Idxe != null)
                .OrderByDescending(x => x.IdpnkNavigation.Mapnk)
                .ThenBy(x => x.IdxeNavigation.Tenxe)
                .ThenBy(x => x.IdmxNavigation.Tenmx)
                //.GroupBy(x => x.Idpnk)
                //.Select(g => g.First())
                .ToListAsync();

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Chitietxes");
                var currentRow = 3;
                var firstRow = 2;

                //worksheet.Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                worksheet.Cell(firstRow, 1).Value = "Danh sách các phiếu nhập kho xe cũ";

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
                worksheet.Cell(currentRow, 15).Value = "Tên nhân viên nhập";
                worksheet.Cell(currentRow, 16).Value = "Tên nhà khách hàng mua";
                worksheet.Cell(currentRow, 17).Value = "Tên nhân viên bán";
                worksheet.Cell(currentRow, 18).Value = "Thời gian bảo hành";
                worksheet.Cell(currentRow, 19).Value = "Giá nhập";
                worksheet.Cell(currentRow, 20).Value = "Giá bán";


                foreach (var slt in phieuNhapXeCu)
                {
                    currentRow++;
                    worksheet.Cell(currentRow, 2).Value = slt.IdpnkNavigation.Mapnk;
                    worksheet.Cell(currentRow, 3).Value = slt.IdpnkNavigation.Ngaynhap;
                    worksheet.Cell(currentRow, 3).Style.DateFormat.Format = "dd/MM/yyyy";
                    if (slt.IdpxkNavigation != null)
                    {
                        worksheet.Cell(currentRow, 4).Value = slt.IdpxkNavigation.Sopxk;
                        worksheet.Cell(currentRow, 5).Value = slt.IdpxkNavigation.Ngayxuat;

                    }
                    else
                    {
                        worksheet.Cell(currentRow, 4).Value = "Chưa bán";
                        worksheet.Cell(currentRow, 5).Value = "Chưa bán";
                    }
                    worksheet.Cell(currentRow, 6).Value = slt.IdxeNavigation.Tenxe;
                    worksheet.Cell(currentRow, 7).Value = slt.IdmxNavigation.Tenmx;
                    worksheet.Cell(currentRow, 8).Value = slt.Doixe;
                    worksheet.Cell(currentRow, 9).Value = slt.Sokhung;
                    worksheet.Cell(currentRow, 10).Value = slt.Somay;
                    worksheet.Cell(currentRow, 11).Value = slt.Biensolucmua;
                    worksheet.Cell(currentRow, 12).Value = slt.Biensolucban;
                    worksheet.Cell(currentRow, 13).Value = slt.Trangthai;
                    worksheet.Cell(currentRow, 14).Value = slt.IdpnkNavigation.IdnccNavigation.Tenncc;
                    worksheet.Cell(currentRow, 15).Value = slt.IdpnkNavigation.IdnvNavigation.Tennv;
                    if (slt.IdpxkNavigation != null && slt.IdpxkNavigation.IdkhNavigation != null)
                    {
                        worksheet.Cell(currentRow, 16).Value = slt.IdpxkNavigation.IdkhNavigation.Tenkh;
                    }
                    else
                    {
                        worksheet.Cell(currentRow, 16).Value = "Không có";
                    }

                    if (slt.IdpxkNavigation != null && slt.IdpxkNavigation.IdnvNavigation != null)
                    {
                        worksheet.Cell(currentRow, 17).Value = slt.IdpxkNavigation.IdnvNavigation.Tennv;
                    }
                    else
                    {
                        worksheet.Cell(currentRow, 17).Value = "Không có";
                    }
                    worksheet.Cell(currentRow, 18).Value = slt.Thoigianbaohanh;
                    worksheet.Cell(currentRow, 19).Value = slt.Gianhap;
                    worksheet.Cell(currentRow, 20).Value = slt.Giaban;
                    
                    tongGianhap += slt.Gianhap;
                    tongGiaban += slt.Giaban;
                }
                currentRow+=3;
                worksheet.Cell(currentRow, 2).Value = "Tổng giá nhập";
                worksheet.Cell(currentRow, 3).Value = tongGianhap;
                currentRow++;
                worksheet.Cell(currentRow, 2).Value = "Tổng giá bán";
                worksheet.Cell(currentRow, 3).Value = tongGiaban;
                currentRow++;
                double? tienloi = tongGiaban - tongGianhap;
                worksheet.Cell(currentRow, 2).Value = "Tiền lời";
                worksheet.Cell(currentRow, 3).Value = tienloi;

                using (var stream = new MemoryStream())
                {
                    
                    var dateColumnHD = worksheet.Column(5);
                    dateColumnHD.Style.DateFormat.Format = "dd/MM/yyyy";
                    worksheet.Columns().AdjustToContents();
                    worksheet.RangeUsed().Style.Border.OutsideBorder = XLBorderStyleValues.Thick;
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Danhsachphieunhapkhoxecu.xlsx");
                }
            }
            }
            catch
            {
                TempData["AlertErrorMessage"] = "";
                return RedirectToAction("Index");
            }
        }
        public async Task<IActionResult> ExportExcelDSXdanhap()
        {
            try { 
            double? tongGianhap = 0;
            double? tongGiaban = 0;
            var danhsachxedanhap = await _context.Chitietxes
                .Include(c => c.IdmxNavigation)
                .Include(c => c.IdpnkNavigation)
                .Include(c => c.IdpnkNavigation.IdnccNavigation)
                .Include(c => c.IdpnkNavigation.IdnvNavigation)
                .Include(c => c.IdpxkNavigation)
                .Include(c => c.IdpxkNavigation.IdkhNavigation)
                .Include(c => c.IdpxkNavigation.IdnvNavigation)
                .Include(c => c.IdxeNavigation)
                .Include(c => c.IdxeNavigation.IdlxNavigation)
                .Where(i => i.Active == true
                    && i.Idpnk != null
                    && i.Idmx != null
                    && i.Idxe != null)
                .OrderByDescending(x => x.IdpnkNavigation.Mapnk)
                .ThenBy(x => x.IdxeNavigation.Tenxe)
                .ThenBy(x => x.IdmxNavigation.Tenmx)
                //.GroupBy(x => x.Idpnk)
                //.Select(g => g.First())
                .ToListAsync();

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Chitietxes");
                var currentRow = 3;
                var firstRow = 2;

                //worksheet.Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                worksheet.Cell(firstRow, 1).Value = "Danh sách xe đã nhập";

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
                worksheet.Cell(currentRow, 15).Value = "Tên nhân viên nhập";
                worksheet.Cell(currentRow, 16).Value = "Tên nhà khách hàng mua";
                worksheet.Cell(currentRow, 17).Value = "Tên nhân viên bán";
                worksheet.Cell(currentRow, 18).Value = "Thời gian bảo hành";
                worksheet.Cell(currentRow, 19).Value = "Giá nhập";
                worksheet.Cell(currentRow, 20).Value = "Giá bán";
                worksheet.Cell(currentRow, 21).Value = "Đã bán";

                foreach (var slt in danhsachxedanhap)
                {
                    currentRow++;
                    worksheet.Cell(currentRow, 2).Value = slt.IdpnkNavigation.Mapnk;
                    worksheet.Cell(currentRow, 3).Value = slt.IdpnkNavigation.Ngaynhap;
                    worksheet.Cell(currentRow, 3).Style.DateFormat.Format = "dd/MM/yyyy";
                    if (slt.IdpxkNavigation != null)
                    {
                        worksheet.Cell(currentRow, 4).Value = slt.IdpxkNavigation.Sopxk;
                        worksheet.Cell(currentRow, 5).Value = slt.IdpxkNavigation.Ngayxuat;

                    }
                    else
                    {
                        worksheet.Cell(currentRow, 4).Value = "Chưa bán";
                        worksheet.Cell(currentRow, 5).Value = "Chưa bán";
                    }
                    worksheet.Cell(currentRow, 6).Value = slt.IdxeNavigation.Tenxe;
                    worksheet.Cell(currentRow, 7).Value = slt.IdmxNavigation.Tenmx;
                    worksheet.Cell(currentRow, 8).Value = slt.Doixe;
                    worksheet.Cell(currentRow, 9).Value = slt.Sokhung;
                    worksheet.Cell(currentRow, 10).Value = slt.Somay;
                    worksheet.Cell(currentRow, 11).Value = slt.Biensolucmua;
                    worksheet.Cell(currentRow, 12).Value = slt.Biensolucban;
                    worksheet.Cell(currentRow, 13).Value = slt.Trangthai;
                    worksheet.Cell(currentRow, 14).Value = slt.IdpnkNavigation.IdnccNavigation.Tenncc;
                    worksheet.Cell(currentRow, 15).Value = slt.IdpnkNavigation.IdnvNavigation.Tennv;
                    if (slt.IdpxkNavigation != null && slt.IdpxkNavigation.IdkhNavigation != null)
                    {
                        worksheet.Cell(currentRow, 16).Value = slt.IdpxkNavigation.IdkhNavigation.Tenkh;
                    }
                    else
                    {
                        worksheet.Cell(currentRow, 16).Value = "Không có";
                    }

                    if (slt.IdpxkNavigation != null && slt.IdpxkNavigation.IdnvNavigation != null)
                    {
                        worksheet.Cell(currentRow, 17).Value = slt.IdpxkNavigation.IdnvNavigation.Tennv;
                    }
                    else
                    {
                        worksheet.Cell(currentRow, 17).Value = "Không có";
                    }
                    worksheet.Cell(currentRow, 18).Value = slt.Thoigianbaohanh;
                    worksheet.Cell(currentRow, 19).Value = slt.Gianhap;
                    worksheet.Cell(currentRow, 20).Value = slt.Giaban;
                    if (slt.Soluong != null && slt.Soluong == 1)
                    {
                        worksheet.Cell(currentRow, 21).Value = "Chưa bán";
                    }
                    else if (slt.Soluong != null && slt.Soluong == 0)
                    {
                        worksheet.Cell(currentRow, 21).Value = "Đã bán";
                        worksheet.Row(currentRow).Style.Fill.BackgroundColor = XLColor.Yellow;
                    }
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
                double? tienloi = tongGiaban - tongGianhap;
                worksheet.Cell(currentRow, 2).Value = "Tiền lời";
                worksheet.Cell(currentRow, 3).Value = tienloi;

                using (var stream = new MemoryStream())
                {
                    var dateColumnHD = worksheet.Column(5);
                    dateColumnHD.Style.DateFormat.Format = "dd/MM/yyyy";
                    worksheet.Columns().AdjustToContents();
                    worksheet.RangeUsed().Style.Border.OutsideBorder = XLBorderStyleValues.Thick;
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Danhsachxenhapkho.xlsx");
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
