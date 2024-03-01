using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MotorcycleBusinessManagement.Models;

namespace MotorcycleBusinessManagement.Controllers
{
    public class ChitietxesController : Controller
    {
        private readonly CarSalesContext _context;

        public ChitietxesController(CarSalesContext context)
        {
            _context = context;
        }

        // GET: Chitietxes
        public async Task<IActionResult> Index()
        {
            var carSalesContext = _context.Chitietxes.Include(c => c.IdmxNavigation).Include(c => c.IdpnkNavigation).Include(c => c.IdpxkNavigation).Include(c => c.IdxeNavigation);
            return View(await carSalesContext.ToListAsync());
        }
        public class TonKhoModel
        {
            public string? MauXe { get; set; }
            public string? TenXe { get; set; }
            public int SoLuongTon { get; set; }
        }
        public async Task<IActionResult> Tonkho()
        {
            var xeTonMoi = await _context.Chitietxes
                .Where(x => x.Trangthai == "Mới" && x.Active==true && x.Idxe!=null && x.Idmx!=null && x.Idpnk != null && x.Soluong==1)
                .GroupBy(x => new { x.IdmxNavigation.Tenmx, x.IdxeNavigation.Tenxe })
                .Select(g => new TonKhoModel
                {
                    MauXe = g.Key.Tenmx,
                    TenXe = g.Key.Tenxe,
                    SoLuongTon = g.Count()
                })
                .ToListAsync();
            var xeTonCu = await _context.Chitietxes
                .Where(x => x.Trangthai == "Cũ" && x.Active == true && x.Idxe != null && x.Idmx != null && x.Idpnk != null && x.Soluong == 1)
                .GroupBy(x => new { x.IdmxNavigation.Tenmx, x.IdxeNavigation.Tenxe })
                .Select(g => new TonKhoModel
                {
                    MauXe = g.Key.Tenmx,
                    TenXe = g.Key.Tenxe,
                    SoLuongTon = g.Count()
                })
                .ToListAsync();
            var xeTonTheoXeMoi = await _context.Chitietxes
                .Where(x => x.Trangthai == "Mới" && x.Active == true && x.Idxe != null && x.Idmx != null && x.Idpnk != null && x.Soluong == 1)
                .GroupBy(x => new {x.IdxeNavigation.Tenxe })
                .Select(g => new TonKhoModel
                {
                    TenXe = g.Key.Tenxe,
                    SoLuongTon = g.Count()
                })
                .ToListAsync();
            var xeTonTheoXeCu = await _context.Chitietxes
                .Where(x => x.Trangthai == "Cũ" && x.Active == true && x.Idxe != null && x.Idmx != null && x.Idpnk != null && x.Soluong == 1)
                .GroupBy(x => new { x.IdxeNavigation.Tenxe })
                .Select(g => new TonKhoModel
                {
                    TenXe = g.Key.Tenxe,
                    SoLuongTon = g.Count()
                })
                .ToListAsync();
            var xeDaBanMoi = await _context.Chitietxes
                .Where(x => x.Trangthai == "Mới" && x.Active == true && x.Idxe != null && x.Idmx != null && x.Idpnk != null && x.Soluong == 0 && x.Idpxk != null)
                .GroupBy(x => new { x.IdmxNavigation.Tenmx, x.IdxeNavigation.Tenxe })
                .Select(g => new TonKhoModel
                {
                    MauXe = g.Key.Tenmx,
                    TenXe = g.Key.Tenxe,
                    SoLuongTon = g.Count()
                })
                .ToListAsync();
            var xeDaBanCu = await _context.Chitietxes
                .Where(x => x.Trangthai == "Cũ" && x.Active == true && x.Idxe != null && x.Idmx != null && x.Idpnk != null && x.Soluong == 0 && x.Idpxk != null)
                .GroupBy(x => new { x.IdmxNavigation.Tenmx, x.IdxeNavigation.Tenxe })
                .Select(g => new TonKhoModel
                {
                    MauXe = g.Key.Tenmx,
                    TenXe = g.Key.Tenxe,
                    SoLuongTon = g.Count()
                })
                .ToListAsync();
            var xeDaBanTheoXeMoi = await _context.Chitietxes
                .Where(x => x.Trangthai == "Mới" && x.Active == true && x.Idxe != null && x.Idmx != null && x.Idpnk != null && x.Soluong == 0 && x.Idpxk != null)
                .GroupBy(x => new { x.IdxeNavigation.Tenxe })
                .Select(g => new TonKhoModel
                {
                    TenXe = g.Key.Tenxe,
                    SoLuongTon = g.Count()
                })
                .ToListAsync();
            var xeDaBanTheoXeCu = await _context.Chitietxes
                .Where(x => x.Trangthai == "Cũ" && x.Active == true && x.Idxe != null && x.Idmx != null && x.Idpnk != null && x.Soluong == 0 && x.Idpxk != null)
                .GroupBy(x => new { x.IdxeNavigation.Tenxe })
                .Select(g => new TonKhoModel
                {
                    TenXe = g.Key.Tenxe,
                    SoLuongTon = g.Count()
                })
                .ToListAsync();
            ViewBag.xeTonMoi = xeTonMoi;
            ViewBag.xeTonCu = xeTonCu;
            ViewBag.xeTonTheoXeMoi = xeTonTheoXeMoi;
            ViewBag.xeTonTheoXeCu = xeTonTheoXeCu;
            ViewBag.xeDaBanMoi = xeDaBanMoi;
            ViewBag.xeDaBanCu = xeDaBanCu;
            ViewBag.xeDaBanTheoXeMoi = xeDaBanTheoXeMoi;
            ViewBag.xeDaBanTheoXeCu = xeDaBanTheoXeCu;
            return View();
        }

        public IActionResult Trangchu()
        {
            return View();
        }
        [HttpPost]
        public List<object> GetDataChart()
        {
            List<object> data = new List<object>();
            var xeTonMoi = _context.Chitietxes
                .Where(x => x.Trangthai == "Mới" && x.Active == true && x.Idxe != null && x.Idmx != null && x.Idpnk != null && x.Soluong==1)
                .GroupBy(x => new { x.IdxeNavigation.Tenxe })
                .Select(g => g.Key.Tenxe)
                .ToList();
            var xeTonCu = _context.Chitietxes
                .Where(x => x.Trangthai == "Mới" && x.Active == true && x.Idxe != null && x.Idmx != null && x.Idpnk != null && x.Soluong == 1)
                .GroupBy(x => new { x.IdxeNavigation.Tenxe })
                .Select(g => g.Count())
                .ToList();

            data.Add(xeTonMoi);
            data.Add(xeTonCu);
            return data;
        }
        [HttpPost]
        public List<object> xeTonKho()
        {
            List<object> data = new List<object>();
            var xeTonMoi = _context.Chitietxes
                .Where(x => x.Active == true && x.Idxe != null && x.Idmx != null && x.Idpnk != null && x.Soluong == 1)
                .GroupBy(x => new { x.IdxeNavigation.Tenxe })
                .Select(g => g.Key.Tenxe)
                .ToList();
            var xeTonCu = _context.Chitietxes
                .Where(x => x.Active == true && x.Idxe != null && x.Idmx != null && x.Idpnk != null && x.Soluong == 1)
                .GroupBy(x => new { x.IdxeNavigation.Tenxe })
                .Select(g => g.Count())
                .ToList();

            data.Add(xeTonMoi);
            data.Add(xeTonCu);
            return data;
        }
        [HttpPost]
        public List<object> xeDaBan()
        {
            List<object> data = new List<object>();
            var xeTonMoi = _context.Chitietxes
                .Where(x => x.Active == true && x.Idxe != null && x.Idmx != null && x.Idpnk != null && x.Soluong == 0 && x.Idpxk != null)
                .GroupBy(x => new { x.IdxeNavigation.Tenxe })
                .Select(g => g.Key.Tenxe)
                .ToList();
            var xeTonCu = _context.Chitietxes
                .Where(x => x.Active == true && x.Idxe != null && x.Idmx != null && x.Idpnk != null && x.Soluong == 0 && x.Idpxk!= null)
                .GroupBy(x => new { x.IdxeNavigation.Tenxe })
                .Select(g => g.Count())
                .ToList();

            data.Add(xeTonMoi);
            data.Add(xeTonCu);
            return data;
        }
        [HttpPost]
        public List<object> xeDaBanMoi()
        {
            List<object> data = new List<object>();
            var xeTonMoi = _context.Chitietxes
                .Where(x => x.Trangthai == "Mới" && x.Active == true && x.Idxe != null && x.Idmx != null && x.Idpnk != null && x.Soluong == 0 && x.Idpxk != null)
                .GroupBy(x => new { x.IdxeNavigation.Tenxe })
                .Select(g => g.Key.Tenxe)
                .ToList();
            var xeTonCu = _context.Chitietxes
                .Where(x => x.Trangthai == "Mới" && x.Active == true && x.Idxe != null && x.Idmx != null && x.Idpnk != null && x.Soluong == 0 && x.Idpxk != null)
                .GroupBy(x => new { x.IdxeNavigation.Tenxe })
                .Select(g => g.Count())
                .ToList();

            data.Add(xeTonMoi);
            data.Add(xeTonCu);
            return data;
        }
        [HttpPost]
        public List<object> xeDaBanCu()
        {
            List<object> data = new List<object>();
            var xeTonMoi = _context.Chitietxes
                .Where(x => x.Trangthai == "Cũ" && x.Active == true && x.Idxe != null && x.Idmx != null && x.Idpnk != null && x.Soluong == 0 && x.Idpxk != null)
                .GroupBy(x => new { x.IdxeNavigation.Tenxe })
                .Select(g => g.Key.Tenxe)
                .ToList();
            var xeTonCu = _context.Chitietxes
                .Where(x => x.Trangthai == "Cũ" && x.Active == true && x.Idxe != null && x.Idmx != null && x.Idpnk != null && x.Soluong == 0 && x.Idpxk != null)
                .GroupBy(x => new { x.IdxeNavigation.Tenxe })
                .Select(g => g.Count())
                .ToList();

            data.Add(xeTonMoi);
            data.Add(xeTonCu);
            return data;
        }
        [HttpPost]
        public List<object> xeTonKhoCu()
        {
            List<object> data = new List<object>();
            var xeTonMoi = _context.Chitietxes
                .Where(x => x.Trangthai == "Cũ" && x.Active == true && x.Idxe != null && x.Idmx != null && x.Idpnk != null && x.Soluong == 1)
                .GroupBy(x => new { x.IdxeNavigation.Tenxe })
                .Select(g => g.Key.Tenxe)
                .ToList();
            var xeTonCu = _context.Chitietxes
                .Where(x => x.Trangthai == "Cũ" && x.Active == true && x.Idxe != null && x.Idmx != null && x.Idpnk != null && x.Soluong == 1)
                .GroupBy(x => new { x.IdxeNavigation.Tenxe })
                .Select(g => g.Count())
                .ToList();

            data.Add(xeTonMoi);
            data.Add(xeTonCu);
            return data;
        }
        //public List<double?> tienChiTheoThang()
        //{
        //    DateTime thang = DateTime.Now;
        //    int Thang = thang.Month;
        //    var tienDaChiThang1 = _context.Phieuchitiennhapxes
        //        .Where(x => x.Active == true && x.Ngaychi.Value.Month == 1)
        //        .ToList();
        //    var tienDaChiThang2 = _context.Phieuchitiennhapxes
        //        .Where(x => x.Active == true && x.Ngaychi.Value.Month == 2)
        //        .ToList();
        //    var tienDaChiThang3 = _context.Phieuchitiennhapxes
        //        .Where(x => x.Active == true && x.Ngaychi.Value.Month == 3)
        //        .ToList();
        //    var tienDaChiThang4 = _context.Phieuchitiennhapxes
        //        .Where(x => x.Active == true && x.Ngaychi.Value.Month == 4)
        //        .ToList();
        //    var tienDaChiThang5 = _context.Phieuchitiennhapxes
        //        .Where(x => x.Active == true && x.Ngaychi.Value.Month == 5)
        //        .ToList();
        //    var tienDaChiThang6 = _context.Phieuchitiennhapxes
        //        .Where(x => x.Active == true && x.Ngaychi.Value.Month == 6)
        //        .ToList();
        //    var tienDaChiThang7 = _context.Phieuchitiennhapxes
        //        .Where(x => x.Active == true && x.Ngaychi.Value.Month == 7)
        //        .ToList();
        //    var tienDaChiThang8 = _context.Phieuchitiennhapxes
        //        .Where(x => x.Active == true && x.Ngaychi.Value.Month == 8)
        //        .ToList();
        //    var tienDaChiThang9 = _context.Phieuchitiennhapxes
        //        .Where(x => x.Active == true && x.Ngaychi.Value.Month == 9)
        //        .ToList();
        //    var tienDaChiThang10 = _context.Phieuchitiennhapxes
        //        .Where(x => x.Active == true && x.Ngaychi.Value.Month == 10)
        //        .ToList();
        //    var tienDaChiThang11 = _context.Phieuchitiennhapxes
        //        .Where(x => x.Active == true && x.Ngaychi.Value.Month == 11)
        //        .ToList();
        //    var tienDaChiThang12 = _context.Phieuchitiennhapxes
        //        .Where(x => x.Active == true && x.Ngaychi.Value.Month == 12)
        //        .ToList();
        //    List<double?> data = new List<double?>();
        //    double? tongTienChiThang1 = 0;
        //    for (var i = 0; i < tienDaChiThang1.Count; i++)
        //    {
        //        tongTienChiThang1 += tienDaChiThang1[i].Tongtienchi;
        //    }
        //    double? tongTienChiThang2 = 0;
        //    for (var i = 0; i < tienDaChiThang2.Count; i++)
        //    {
        //        tongTienChiThang2 += tienDaChiThang2[i].Tongtienchi;
        //    }
        //    double? tongTienChiThang3 = 0;
        //    for (var i = 0; i < tienDaChiThang3.Count; i++)
        //    {
        //        tongTienChiThang3 += tienDaChiThang3[i].Tongtienchi;
        //    }
        //    double? tongTienChiThang4 = 0;
        //    for (var i = 0; i < tienDaChiThang4.Count; i++)
        //    {
        //        tongTienChiThang4 += tienDaChiThang4[i].Tongtienchi;
        //    }
        //    double? tongTienChiThang5 = 0;
        //    for (var i = 0; i < tienDaChiThang5.Count; i++)
        //    {
        //        tongTienChiThang5 += tienDaChiThang5[i].Tongtienchi;
        //    }
        //    double? tongTienChiThang6 = 0;
        //    for (var i = 0; i < tienDaChiThang6.Count; i++)
        //    {
        //        tongTienChiThang6 += tienDaChiThang6[i].Tongtienchi;
        //    }
        //    double? tongTienChiThang7 = 0;
        //    for (var i = 0; i < tienDaChiThang7.Count; i++)
        //    {
        //        tongTienChiThang7 += tienDaChiThang7[i].Tongtienchi;
        //    }
        //    double? tongTienChiThang8 = 0;
        //    for (var i = 0; i < tienDaChiThang8.Count; i++)
        //    {
        //        tongTienChiThang8 += tienDaChiThang8[i].Tongtienchi;
        //    }
        //    double? tongTienChiThang9 = 0;
        //    for (var i = 0; i < tienDaChiThang9.Count; i++)
        //    {
        //        tongTienChiThang9 += tienDaChiThang9[i].Tongtienchi;
        //    }
        //    double? tongTienChiThang10 = 0;
        //    for (var i = 0; i < tienDaChiThang10.Count; i++)
        //    {
        //        tongTienChiThang10 += tienDaChiThang10[i].Tongtienchi;
        //    }
        //    double? tongTienChiThang11 = 0;
        //    for (var i = 0; i < tienDaChiThang11.Count; i++)
        //    {
        //        tongTienChiThang11 += tienDaChiThang11[i].Tongtienchi;
        //    }
        //    double? tongTienChiThang12 = 0;
        //    for (var i = 0; i < tienDaChiThang12.Count; i++)
        //    {
        //        tongTienChiThang12 += tienDaChiThang12[i].Tongtienchi;
        //    }
        //    data.Add(tongTienChiThang1);
        //    data.Add(tongTienChiThang2);
        //    data.Add(tongTienChiThang3);
        //    data.Add(tongTienChiThang4);
        //    data.Add(tongTienChiThang5);
        //    data.Add(tongTienChiThang6);
        //    data.Add(tongTienChiThang7);
        //    data.Add(tongTienChiThang8);
        //    data.Add(tongTienChiThang9);
        //    data.Add(tongTienChiThang10);
        //    data.Add(tongTienChiThang11);
        //    data.Add(tongTienChiThang12); 

        //    return data;
        //}
        public List<double?> TienChiTheoThang()
        {
            List<double?> data = new List<double?>();

            for (int month = 1; month <= 12; month++)
            {
                var tienDaChiThang = _context.Phieuchitiennhapxes
                    .Where(x => x.Active == true && x.Ngaychi.Value.Month == month)
                    .ToList();

                double? tongTienChiThang = 0;

                foreach (var phieuChi in tienDaChiThang)
                {
                    tongTienChiThang += phieuChi.Tongtienchi;
                }

                data.Add(tongTienChiThang);
            }

            return data;
        }
        public List<double?> TienThuTheoThang()
        {
            List<double?> data = new List<double?>();
            
            for (int month = 1; month <= 12; month++)
            {
                var tienDaThuThang = _context.Phieuthutienbanxes
                    .Where(x => x.Active == true && x.Ngaythu.Value.Month == month)
                    .ToList();

                double? tongTienThuThang = 0;

                foreach (var phieuThu in tienDaThuThang)
                {
                    tongTienThuThang += phieuThu.Tongtienthu;
                }

                data.Add(tongTienThuThang);
            }

            return data;
        }

        public List<double?> TienChiTheoNam()
        {
            List<double?> data = new List<double?>();

            for (int year = 2023; year <= 2033; year++)
            {
                var tienDaChiNam = _context.Phieuchitiennhapxes
                    .Where(x => x.Active == true && x.Ngaychi.Value.Year == year)
                    .ToList();

                double? tongTienChiNam = 0;

                foreach (var phieuChi in tienDaChiNam)
                {
                    tongTienChiNam += phieuChi.Tongtienchi;
                }

                data.Add(tongTienChiNam);
            }

            return data;
        }
        public List<double?> TienThuTheoNam()
        {
            List<double?> data = new List<double?>();

            for (int year = 2023; year <= 2033; year++)
            {
                var tienDaThuNam = _context.Phieuthutienbanxes
                    .Where(x => x.Active == true && x.Ngaythu.Value.Year == year)
                    .ToList();

                double? tongTienThuNam = 0;

                foreach (var phieuThu in tienDaThuNam)
                {
                    tongTienThuNam += phieuThu.Tongtienthu;
                }

                data.Add(tongTienThuNam);
            }

            return data;
        }
        public List<double?> LoiNhuanTheoNam()
        {
            List<double?> data = new List<double?>();

            for (int year = 2023; year <= 2033; year++)
            {
                var tienDaThuNam = _context.Phieuthutienbanxes
                    .Where(x => x.Active == true && x.Ngaythu.Value.Year == year)
                    .ToList();
                var tienDaChiNam = _context.Phieuchitiennhapxes
                    .Where(x => x.Active == true && x.Ngaychi.Value.Year == year)
                    .ToList();
                double? tongTienThuNam = 0;

                foreach (var phieuThu in tienDaThuNam)
                {
                    tongTienThuNam += phieuThu.Tongtienthu;
                }
                double? tongTienChiNam = 0;

                foreach (var phieuChi in tienDaChiNam)
                {
                    tongTienChiNam += phieuChi.Tongtienchi;
                }
                double? loinhuan = tongTienThuNam - tongTienChiNam;

                data.Add(loinhuan);
            }

            return data;
        }
        public async Task<IActionResult> ExportExcel()
        {

            var soluongton = await _context.Chitietxes
                .Include(c => c.IdmxNavigation)
                .Include(c => c.IdpnkNavigation)
                .Include(c => c.IdpnkNavigation.IdnccNavigation)
                .Include(c => c.IdpnkNavigation.IdnvNavigation)
                .Include(c => c.IdpxkNavigation)
                .Include(c => c.IdxeNavigation)
                .Include(c => c.IdxeNavigation.IdlxNavigation)
                .Where(i => i.Soluong == 1)
                .OrderByDescending(x => x.Idct)
                .ToListAsync();

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Chitietxes");
                var currentRow = 2;

                //worksheet.Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                worksheet.Cell(currentRow, 1).Value = "Danh sách các xe tồn kho";

                worksheet.Cell(currentRow, 2).Value = "Số lượng";
                worksheet.Cell(currentRow, 3).Value = "Giá nhập";
                worksheet.Cell(currentRow, 4).Value = "Số khung";
                worksheet.Cell(currentRow, 5).Value = "Số máy";
                worksheet.Cell(currentRow, 6).Value = "Trạng thái";
                worksheet.Cell(currentRow, 7).Value = "Biển số lúc mua";
                worksheet.Cell(currentRow, 8).Value = "Biển số lúc bán";
                worksheet.Cell(currentRow, 9).Value = "Màu xe";
                worksheet.Cell(currentRow, 10).Value = "Số phiếu nhập";
                worksheet.Cell(currentRow, 11).Value = "Ngày nhập";
                worksheet.Cell(currentRow, 12).Value = "Số hóa đơn";
                worksheet.Cell(currentRow, 13).Value = "Ngày hóa đơn";
                worksheet.Cell(currentRow, 14).Value = "Tên nhà cung cấp";
                worksheet.Cell(currentRow, 15).Value = "Tên nhân viên nhập";
                worksheet.Cell(currentRow, 16).Value = "Tên xe";
                worksheet.Cell(currentRow, 17).Value = "Tên loại xe";
                worksheet.Cell(currentRow, 18).Value = "Thời gian bảo hành";
                foreach (var slt in soluongton)
                {

                    currentRow++;
                    worksheet.Cell(currentRow, 2).Value = slt.Soluong;
                    worksheet.Cell(currentRow, 3).Value = slt.Gianhap;
                    worksheet.Cell(currentRow, 4).Value = slt.Sokhung;
                    worksheet.Cell(currentRow, 5).Value = slt.Somay;
                    worksheet.Cell(currentRow, 6).Value = slt.Trangthai;
                    worksheet.Cell(currentRow, 7).Value = slt.Biensolucmua;
                    worksheet.Cell(currentRow, 8).Value = slt.Biensolucban;
                    worksheet.Cell(currentRow, 9).Value = slt.IdmxNavigation.Tenmx;
                    worksheet.Cell(currentRow, 10).Value = slt.IdpnkNavigation.Mapnk;
                    worksheet.Cell(currentRow, 11).Value = slt.IdpnkNavigation.Ngaynhap;
                    worksheet.Cell(currentRow, 12).Value = slt.IdpnkNavigation.Sohd;
                    worksheet.Cell(currentRow, 13).Value = slt.IdpnkNavigation.Ngayhd;
                    worksheet.Cell(currentRow, 14).Value = slt.IdpnkNavigation.IdnccNavigation.Tenncc;
                    worksheet.Cell(currentRow, 15).Value = slt.IdpnkNavigation.IdnvNavigation.Tennv;
                    worksheet.Cell(currentRow, 16).Value = slt.IdxeNavigation.Tenxe;
                    worksheet.Cell(currentRow, 17).Value = slt.IdxeNavigation.IdlxNavigation.Tenlx;
                    worksheet.Cell(currentRow, 18).Value = slt.Thoigianbaohanh;


                }
                using (var stream = new MemoryStream())
                {

                    worksheet.Columns().AdjustToContents();
                    worksheet.RangeUsed().Style.Border.OutsideBorder = XLBorderStyleValues.Thick;
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Danhsachxetonkho.xlsx");
                }
            }

        }

        // GET: Chitietxes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Chitietxes == null)
            {
                return NotFound();
            }

            var chitietxe = await _context.Chitietxes
                .Include(c => c.IdmxNavigation)
                .Include(c => c.IdpnkNavigation)
                .Include(c => c.IdpxkNavigation)
                .Include(c => c.IdxeNavigation)
                .FirstOrDefaultAsync(m => m.Idct == id);
            if (chitietxe == null)
            {
                return NotFound();
            }

            return View(chitietxe);
        }

        // GET: Chitietxes/Create
        public IActionResult Create()
        {
            ViewData["Idmx"] = new SelectList(_context.Mauxes, "Idmx", "Idmx");
            ViewData["Idpnk"] = new SelectList(_context.Phieunhapkhos, "Idpnk", "Idpnk");
            ViewData["Idpxk"] = new SelectList(_context.Phieuxuats, "Idpxk", "Idpxk");
            ViewData["Idxe"] = new SelectList(_context.Xes, "Idxe", "Idxe");
            return View();
        }

        // POST: Chitietxes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Idct,Idpnk,Idxe,Idmx,Soluong,Gianhap,Sokhung,Somay,Trangthai,Idpxk,Biensolucmua,Biensolucban,Thoigianbaohanh,Active")] Chitietxe chitietxe)
        {
            if (ModelState.IsValid)
            {
                _context.Add(chitietxe);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Idmx"] = new SelectList(_context.Mauxes, "Idmx", "Idmx", chitietxe.Idmx);
            ViewData["Idpnk"] = new SelectList(_context.Phieunhapkhos, "Idpnk", "Idpnk", chitietxe.Idpnk);
            ViewData["Idpxk"] = new SelectList(_context.Phieuxuats, "Idpxk", "Idpxk", chitietxe.Idpxk);
            ViewData["Idxe"] = new SelectList(_context.Xes, "Idxe", "Idxe", chitietxe.Idxe);
            return View(chitietxe);
        }

        // GET: Chitietxes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Chitietxes == null)
            {
                return NotFound();
            }

            var chitietxe = await _context.Chitietxes.FindAsync(id);
            if (chitietxe == null)
            {
                return NotFound();
            }
            ViewData["Idmx"] = new SelectList(_context.Mauxes, "Idmx", "Idmx", chitietxe.Idmx);
            ViewData["Idpnk"] = new SelectList(_context.Phieunhapkhos, "Idpnk", "Idpnk", chitietxe.Idpnk);
            ViewData["Idpxk"] = new SelectList(_context.Phieuxuats, "Idpxk", "Idpxk", chitietxe.Idpxk);
            ViewData["Idxe"] = new SelectList(_context.Xes, "Idxe", "Idxe", chitietxe.Idxe);
            return View(chitietxe);
        }

        // POST: Chitietxes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Idct,Idpnk,Idxe,Idmx,Soluong,Gianhap,Sokhung,Somay,Trangthai,Idpxk,Biensolucmua,Biensolucban,Thoigianbaohanh,Active")] Chitietxe chitietxe)
        {
            if (id != chitietxe.Idct)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(chitietxe);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ChitietxeExists(chitietxe.Idct))
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
            ViewData["Idmx"] = new SelectList(_context.Mauxes, "Idmx", "Idmx", chitietxe.Idmx);
            ViewData["Idpnk"] = new SelectList(_context.Phieunhapkhos, "Idpnk", "Idpnk", chitietxe.Idpnk);
            ViewData["Idpxk"] = new SelectList(_context.Phieuxuats, "Idpxk", "Idpxk", chitietxe.Idpxk);
            ViewData["Idxe"] = new SelectList(_context.Xes, "Idxe", "Idxe", chitietxe.Idxe);
            return View(chitietxe);
        }

        // GET: Chitietxes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Chitietxes == null)
            {
                return NotFound();
            }

            var chitietxe = await _context.Chitietxes
                .Include(c => c.IdmxNavigation)
                .Include(c => c.IdpnkNavigation)
                .Include(c => c.IdpxkNavigation)
                .Include(c => c.IdxeNavigation)
                .FirstOrDefaultAsync(m => m.Idct == id);
            if (chitietxe == null)
            {
                return NotFound();
            }

            return View(chitietxe);
        }

        // POST: Chitietxes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Chitietxes == null)
            {
                return Problem("Entity set 'CarSalesContext.Chitietxes'  is null.");
            }
            var chitietxe = await _context.Chitietxes.FindAsync(id);
            if (chitietxe != null)
            {
                _context.Chitietxes.Remove(chitietxe);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ChitietxeExists(int id)
        {
          return (_context.Chitietxes?.Any(e => e.Idct == id)).GetValueOrDefault();
        }
    }
}
