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
    public class PhieuthutienbanxesController : Controller
    {
        private readonly CarSalesContext _context;

        public PhieuthutienbanxesController(CarSalesContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Indexold(string searchString, DateTime fromDate, DateTime toDate)
        {
            var phieuThuTien = await _context.Phieuthutienbanxes
                .Include(p => p.IdhtttNavigation).Include(p => p.IdnvNavigation)
                .OrderByDescending(b => b.Idpt).Where(c => c.Active == true).ToListAsync();
            var phieuDaThu = await _context.Phieuxuats.Include(n => n.IdnvNavigation)
                                                        .Include(n => n.IdptNavigation)
                                                        .Include(n => n.IdkhNavigation)
                                                        .OrderByDescending(b => b.Idpxk)
                                                        .Where(c => c.Active == true && c.Idpt != null && c.Dathu == true).ToListAsync();
            if (!string.IsNullOrEmpty(searchString))
            {
                phieuThuTien = await _context.Phieuthutienbanxes
                .Include(p => p.IdhtttNavigation).Include(p => p.IdnvNavigation)
                .OrderByDescending(b => b.Idpt)
                .Where(c => c.Active == true && c.Sopt.Contains(searchString) || c.IdhtttNavigation.Tenhttt.Contains(searchString)
                        || c.IdnvNavigation.Tennv.Contains(searchString)).ToListAsync();
                phieuDaThu = await _context.Phieuxuats.Include(n => n.IdnvNavigation)
                                                        .Include(n => n.IdptNavigation)
                                                        .Include(n => n.IdkhNavigation)
                                                        .OrderByDescending(b => b.Idpxk)
                                                        .Where(c => c.Active == true && c.Idpt != null && c.Dathu == true
                                                        && c.Sopxk.Contains(searchString) || c.IdptNavigation.Sopt.Contains(searchString))
                                                        .ToListAsync();
                //if (phieuThuTien.Any() == false)
                //{
                //    TempData["AlertpttMessage"] = "";
                //}
                //if (phieuDaThu.Any() == false)
                //{
                //    TempData["AlertpdtMessage"] = "";
                //}
            }

            var fdate = fromDate.ToString("ddMMyyyy");
            var tdate = toDate.ToString("ddMMyyyy");
            if (fdate != "01010001" && tdate != "01010001" && Int32.Parse(fdate) <= Int32.Parse(tdate))
            {
                phieuThuTien = await _context.Phieuthutienbanxes
                .Include(p => p.IdhtttNavigation).Include(p => p.IdnvNavigation)
                .OrderByDescending(b => b.Idpt)
                .Where(c => c.Active == true && (c.Ngaythu >= fromDate) && (c.Ngaythu <= toDate)).ToListAsync();
                phieuDaThu = await _context.Phieuxuats.Include(n => n.IdnvNavigation)
                                                        .Include(n => n.IdptNavigation)
                                                        .Include(n => n.IdkhNavigation)
                                                        .OrderByDescending(b => b.Idpxk)
                                                        .Where(c => c.Active == true && c.Idpt != null && c.Dathu == true
                                                        && (c.Ngayxuat >= fromDate) && (c.Ngayxuat <= toDate)
                                                        || (c.IdptNavigation.Ngaythu >= fromDate) && (c.IdptNavigation.Ngaythu <= toDate))
                                                        .ToListAsync();
                //if (phieuThuTien.Any() == false)
                //{
                //    TempData["AlertpttMessage"] = "";
                //}
                //if (phieuDaThu.Any() == false)
                //{
                //    TempData["AlertpdtMessage"] = "";
                //}
            }
            ViewBag.phieuThuTien = phieuThuTien;
            ViewBag.phieuDaThu = phieuDaThu;
            return View();
        }

        public async Task<IActionResult> Index(string searchString, DateTime fromDate, DateTime toDate)
        {
            var phieuThuTien = await _context.Phieuthutienbanxes
                .Include(p => p.IdhtttNavigation).Include(p => p.IdnvNavigation)
                .OrderByDescending(b => b.Idpt).Where(c => c.Active == true).ToListAsync();
            
            if (!string.IsNullOrEmpty(searchString))
            {
                phieuThuTien = await _context.Phieuthutienbanxes
                .Include(p => p.IdhtttNavigation).Include(p => p.IdnvNavigation)
                .OrderByDescending(b => b.Idpt)
                .Where(c => c.Active == true && c.Sopt.Contains(searchString) || c.IdhtttNavigation.Tenhttt.Contains(searchString)
                        || c.IdnvNavigation.Tennv.Contains(searchString)).ToListAsync();
                
            }

            var fdate = fromDate.ToString("ddMMyyyy");
            var tdate = toDate.ToString("ddMMyyyy");
            if (fdate != "01010001" && tdate != "01010001" && Int32.Parse(fdate) <= Int32.Parse(tdate))
            {
                phieuThuTien = await _context.Phieuthutienbanxes
                .Include(p => p.IdhtttNavigation).Include(p => p.IdnvNavigation)
                .OrderByDescending(b => b.Idpt)
                .Where(c => c.Active == true && (c.Ngaythu >= fromDate) && (c.Ngaythu <= toDate)).ToListAsync();
                
            }
            ViewBag.phieuThuTien = phieuThuTien;
            return View();
        }

        public async Task<IActionResult> IndexPDT(string searchString, DateTime fromDate, DateTime toDate)
        {
            var phieuDaThu = await _context.Phieuxuats.Include(n => n.IdnvNavigation)
                                                        .Include(n => n.IdptNavigation)
                                                        .Include(n => n.IdkhNavigation)
                                                        .OrderByDescending(b => b.Idpxk)
                                                        .Where(c => c.Active == true && c.Idpt != null && c.Dathu == true).ToListAsync();
            if (!string.IsNullOrEmpty(searchString))
            {
                phieuDaThu = await _context.Phieuxuats.Include(n => n.IdnvNavigation)
                                                        .Include(n => n.IdptNavigation)
                                                        .Include(n => n.IdkhNavigation)
                                                        .OrderByDescending(b => b.Idpxk)
                                                        .Where(c => c.Active == true && c.Idpt != null && c.Dathu == true
                                                        && c.Sopxk.Contains(searchString) || c.IdptNavigation.Sopt.Contains(searchString))
                                                        .ToListAsync();
                
            }

            var fdate = fromDate.ToString("ddMMyyyy");
            var tdate = toDate.ToString("ddMMyyyy");
            if (fdate != "01010001" && tdate != "01010001" && Int32.Parse(fdate) <= Int32.Parse(tdate))
            {
               
                phieuDaThu = await _context.Phieuxuats.Include(n => n.IdnvNavigation)
                                                        .Include(n => n.IdptNavigation)
                                                        .Include(n => n.IdkhNavigation)
                                                        .OrderByDescending(b => b.Idpxk)
                                                        .Where(c => c.Active == true && c.Idpt != null && c.Dathu == true
                                                        && (c.Ngayxuat >= fromDate) && (c.Ngayxuat <= toDate)
                                                        || (c.IdptNavigation.Ngaythu >= fromDate) && (c.IdptNavigation.Ngaythu <= toDate))
                                                        .ToListAsync();
                
            }
            ViewBag.phieuDaThu = phieuDaThu;
            return View();
        }


        // GET: Phieuchitiennhapxes/Details/5

        [HttpGet]
        public IActionResult Create()
        {
            string Sophieutudong = TaoSoPhieu();
            ViewData["idtd"] = Sophieutudong;
            TempData["Px"] = _context.Phieuxuats.Include(a => a.IdnvNavigation).Include(b => b.IdptNavigation).Include(c => c.IdkhNavigation)
                .Where(x => x.Active == true && x.Dathu==false && x.Idpt==null && x.Idnv != null && x.Idkh != null && x.Tongtienxuat != 0 && x.Tongtienxuat!=null).ToList();

            TempData["httt"] = _context.Httts.Where(x => x.Active == true).ToList();
            TempData["Nhanvien"] = _context.Nhanviens.Where(x => x.Active == true).ToList();
            Phieuthutienbanxe pttnx = new Phieuthutienbanxe();
            pttnx.Phieuxuats.Add(new Phieuxuat() { Idpxk = 1 });
            return View(pttnx);
        }

        [HttpPost]
        public IActionResult Create(Phieuthutienbanxe ptt)
        {
            if (ptt.Phieuxuats.Count() == 0)
            {
                try
                {

                    ptt.Ngaythu = DateTime.Now;
                    ptt.Active = true;
                    _context.Add(ptt);
                    ptt.Phieuxuats.RemoveAll(n => n.Active == false || n.Sopxk == null || n.Idpxk == null);

                    // Lặp qua danh sách Phieunhapkho
                    foreach (var updatedPhieuxuat in ptt.Phieuxuats)
                    {
                        // Kiểm tra xem Idct đã tồn tại hay chưa
                        Phieuxuat existingPx = ptt.Phieuxuats
                            .FirstOrDefault(c => c.Idpxk == updatedPhieuxuat.Idpxk);

                        if (existingPx != null)
                        {
                            existingPx.Sopxk = updatedPhieuxuat.Sopxk;
                            existingPx.Ngayxuat = updatedPhieuxuat.Ngayxuat;
                            existingPx.Sohd = updatedPhieuxuat.Sohd;
                            existingPx.Ngayhd = updatedPhieuxuat.Ngayhd;
                            existingPx.Dathu = true;
                            existingPx.Tongtienxuat = updatedPhieuxuat.Tongtienxuat;
                            existingPx.Idkh = updatedPhieuxuat.Idkh;
                            existingPx.Idnv = updatedPhieuxuat.Idnv;

                            _context.Phieuxuats.Update(existingPx);
                        }
                        else
                        {
                            // Thêm mới Chitietxe
                            ptt.Phieuxuats.Add(updatedPhieuxuat);
                        }
                    }

                    // Lưu các thay đổi
                    _context.SaveChanges();

                    TempData["AlertErrorMessage"] = ptt.Sopt;
                    return RedirectToAction("Index");
                }
                catch
                {
                    TempData["AlertErrorMessage"] = "";
                    return RedirectToAction("Index");
                }
            }
            else
            {
                try
                {

                    ptt.Ngaythu = DateTime.Now;
                    ptt.Active = true;
                    _context.Add(ptt);
                    ptt.Phieuxuats.RemoveAll(n => n.Active == false || n.Sopxk == null || n.Idpxk == null);

                    // Lặp qua danh sách Phieunhapkho
                    foreach (var updatedPhieuxuat in ptt.Phieuxuats)
                    {
                        // Kiểm tra xem Idct đã tồn tại hay chưa
                        Phieuxuat existingPx = ptt.Phieuxuats
                            .FirstOrDefault(c => c.Idpxk == updatedPhieuxuat.Idpxk);

                        if (existingPx != null)
                        {
                            existingPx.Sopxk = updatedPhieuxuat.Sopxk;
                            existingPx.Ngayxuat = updatedPhieuxuat.Ngayxuat;
                            existingPx.Sohd = updatedPhieuxuat.Sohd;
                            existingPx.Ngayhd = updatedPhieuxuat.Ngayhd;
                            existingPx.Dathu = true;
                            existingPx.Tongtienxuat = updatedPhieuxuat.Tongtienxuat;
                            existingPx.Idkh = updatedPhieuxuat.Idkh;
                            existingPx.Idnv = updatedPhieuxuat.Idnv;

                            _context.Phieuxuats.Update(existingPx);
                        }
                        else
                        {
                            // Thêm mới Chitietxe
                            ptt.Phieuxuats.Add(updatedPhieuxuat);
                        }
                    }

                    // Lưu các thay đổi
                    _context.SaveChanges();

                    TempData["AlertSuccessMessage"] = ptt.Sopt;
                    return RedirectToAction("Index");
                }
                catch
                {
                    TempData["AlertErrorMessage"] = "";
                    return RedirectToAction("Index");
                }
            }
        }
        private string TaoSoPhieu()
        {
            // Lấy ngày tháng năm hiện tại
            string ngayThangNam = DateTime.Now.ToString("ddMMyy");

            // Kiểm tra xem có phiếu cùng ngày không
            var phieuCu = _context.Phieuthutienbanxes.OrderByDescending(x => x.Sopt).FirstOrDefault(p => p.Sopt.StartsWith($"PT{ngayThangNam}"));

            if (phieuCu != null)
            {
                // Tách lấy số phiếu hiện tại
                string soPhieuCu = phieuCu.Sopt.Substring(8);
                int soPhieuMoi = int.Parse(soPhieuCu) + 1;
                return $"PT{ngayThangNam}{soPhieuMoi:D4}";
            }
            else
            {
                // Nếu không có phiếu cùng ngày, bắt đầu từ 1
                return $"PT{ngayThangNam}0001";
            }
        }
        [HttpGet]
        public IActionResult Details(int id)
        {
            try
            {
                TempData["Px"] = _context.Phieuxuats.Include(a => a.IdnvNavigation).Include(b => b.IdptNavigation).Include(c => c.IdkhNavigation)
                .Where(x => x.Active == true).ToList();
                TempData["httt"] = _context.Httts.Where(x => x.Active == true).ToList();
                TempData["Nhanvien"] = _context.Nhanviens.Where(x => x.Active == true).ToList();
                Phieuthutienbanxe? ptt = _context.Phieuthutienbanxes
                    .Include(e => e.Phieuxuats)
                    .Where(a => a.Idpt == id).FirstOrDefault();

                return View(ptt);
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
                TempData["Px"] = _context.Phieuxuats.Include(a => a.IdnvNavigation).Include(b => b.IdptNavigation).Include(c => c.IdkhNavigation)
                .Where(x => x.Active == true).ToList();

                TempData["httt"] = _context.Httts.Where(x => x.Active == true).ToList();
                TempData["Nhanvien"] = _context.Nhanviens.Where(x => x.Active == true).ToList();
                Phieuthutienbanxe? ptt = _context.Phieuthutienbanxes
                    .Include(e => e.Phieuxuats)
                    .Where(a => a.Idpt == id).FirstOrDefault();

                return View(ptt);
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        public IActionResult Delete(Phieuthutienbanxe ptt)
        {
            try { 
            Phieuthutienbanxe? existingApplicant = _context.Phieuthutienbanxes
                .Include(e => e.Phieuxuats)
                .FirstOrDefault(a => a.Idpt == ptt.Idpt);
            existingApplicant.Active = false;

            var ctxActive = _context.Phieuxuats.Where(x => x.Idpt == ptt.Idpt).ToList();
            foreach (var item in ctxActive)
            {
                item.Idpt = null;
                item.Dathu = false;
            }
            
            _context.SaveChanges();
            TempData["AlertDeleteMessage"] = ptt.Sopt;
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
            TempData["Tenxe"] = _context.Chitietxes.Include(a => a.IdxeNavigation).Include(b => b.IdpnkNavigation).Include(c => c.IdmxNavigation).Include(d => d.IdpxkNavigation)
                .Where(x => x.Active == true && x.Idxe != null && x.Idpnk != null && x.Idmx != null && x.Idpxk != null).ToList();

            TempData["httt"] = _context.Httts.Where(x => x.Active == true).ToList();
            TempData["Nhanvien"] = _context.Nhanviens.Where(x => x.Active == true).ToList();
            Phieuthutienbanxe? ptt = _context.Phieuthutienbanxes
                    .Include(e => e.Noidungthus)
                    .Where(a => a.Idpt == id).FirstOrDefault();

            if (ptt == null)
            {
                return NotFound(); // Xử lý trường hợp không tìm thấy ứng viên
            }

            return View(ptt);
        }



        [HttpPost]
        public IActionResult Edit(int id, Phieuthutienbanxe ptt)
        {
            if (id != ptt.Idpt)
            {
                return BadRequest(); // Xử lý trường hợp id không khớp
            }

            // Kiểm tra xem ứng viên có tồn tại trong cơ sở dữ liệu hay không
            Phieuthutienbanxe? existingApplicant = _context.Phieuthutienbanxes
                .Include(e => e.Noidungthus)
                .FirstOrDefault(a => a.Idpt == ptt.Idpt);

            if (existingApplicant == null)
            {
                return NotFound(); // Xử lý trường hợp không tìm thấy ứng viên
            }

            //// Loại bỏ các kinh nghiệm có YearsWorked == 0
            existingApplicant.Noidungthus.RemoveAll(n => n.Sotienthu == null && n.Idct==null);



            // Cập nhật thông tin của ứng viên
            existingApplicant.Sopt = ptt.Sopt; // Thay thế bằng các trường thông tin khác cần cập nhật
            existingApplicant.Ngaythu = ptt.Ngaythu;
            existingApplicant.Idhttt = ptt.Idhttt;
            existingApplicant.Idnv = ptt.Idnv;
            existingApplicant.Active = true;

            // Xóa tất cả các trải nghiệm cũ
            existingApplicant.Noidungthus.Clear();

            // Thêm lại các trải nghiệm từ form
            foreach (var ptts in ptt.Noidungthus)
            {
                existingApplicant.Noidungthus.Add(ptts);
            }

            _context.SaveChanges();
            TempData["AlertEditMessage"] = ptt.Sopt;
            return RedirectToAction("Index");
        }

        public IActionResult DownPDF(int? id)
        {
            try { 
            if (id == null || _context.Phieuthutienbanxes == null)
            {
                return NotFound();
            }


            var pct = _context.Phieuthutienbanxes
                .Include(p => p.IdhtttNavigation)
                .Include(p => p.IdnvNavigation)
                .Where(x => x.Active == true && x.Idpt == id).FirstOrDefault();

            var ctxuat = _context.Phieuxuats
                .Include(c => c.IdkhNavigation)
                .Include(c => c.IdnvNavigation)
                .Where(x => x.Active == true && x.Idpt == id).ToList();
            ViewBag.ctxuat = ctxuat;

            if (pct == null)
            {
                return NotFound();
            }

            return new ViewAsPdf("DownPDF", pct)
            {
                FileName = $"Phieuthutien {pct.Sopt}.pdf",
                PageOrientation = Rotativa.AspNetCore.Options.Orientation.Portrait,
                PageSize = Rotativa.AspNetCore.Options.Size.A4,
                ViewData = new ViewDataDictionary(new EmptyModelMetadataProvider(), new ModelStateDictionary())
                {
                    Model = pct,
                    ["ctxuat"] = ctxuat
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
            double? tongTienThu = 0;
            
            var phieuThuTien = await _context.Phieuthutienbanxes
                .Include(p => p.IdhtttNavigation)
                .Include(p => p.IdnvNavigation)
                .OrderByDescending(b => b.Idpt)
                .Where(c => c.Active == true)
                .ToListAsync();

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Phieuthutienbanxes");
                var currentRow = 3;



                worksheet.Cell(2, 1).Value = "Danh sách phiếu thu tiền bán xe";

                //worksheet.Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                //worksheet.Cell(currentRow, 1).Value = "Danh sách các xe đã bán";

                worksheet.Cell(currentRow, 2).Value = "Số phiếu thu";
                worksheet.Cell(currentRow, 3).Value = "Ngày thu";
                worksheet.Cell(currentRow, 4).Value = "Nhân viên thu";
                worksheet.Cell(currentRow, 5).Value = "Hình thức thu";
                worksheet.Cell(currentRow, 6).Value = "Số tiền";
                
                foreach (var slt in phieuThuTien)
                {
                    currentRow++;
                    worksheet.Cell(currentRow, 2).Value = slt.Sopt;
                    worksheet.Cell(currentRow, 3).Value = slt.Ngaythu;
                    worksheet.Cell(currentRow, 3).Style.DateFormat.Format = "dd/MM/yyyy";
                    worksheet.Cell(currentRow, 4).Value = slt.IdnvNavigation.Tennv;
                    worksheet.Cell(currentRow, 5).Value = slt.IdhtttNavigation.Tenhttt;
                    worksheet.Cell(currentRow, 6).Value = slt.Tongtienthu;

                    tongTienThu += slt.Tongtienthu;

                }
                currentRow += 3;
                worksheet.Cell(currentRow, 2).Value = "Tổng tiền thu";
                worksheet.Cell(currentRow, 3).Value = tongTienThu;
                
                using (var stream = new MemoryStream())
                {

                    worksheet.Columns().AdjustToContents();
                    worksheet.RangeUsed().Style.Border.OutsideBorder = XLBorderStyleValues.Thick;
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Danhsachphieuthu.xlsx");
                }
            }
            }
            catch
            {
                TempData["AlertErrorMessage"] = "";
                return RedirectToAction("Index");
            }
        }
        public async Task<IActionResult> ExportExcelPDT()
        {
            try { 
                double? tongTienThu = 0;
                double? tongTienDaThu = 0;

                var phieuDaThu = await _context.Phieuxuats.Include(n => n.IdnvNavigation)
                                                            .Include(n => n.IdptNavigation)
                                                            .Include(n => n.IdptNavigation.IdnvNavigation)
                                                            .Include(n => n.IdptNavigation.IdhtttNavigation)
                                                            .Include(n => n.IdkhNavigation)
                                                            .OrderByDescending(b => b.Idpxk)
                                                            .Where(c => c.Active == true && c.Idpt != null && c.Dathu == true)
                                                            .ToListAsync();

                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Phieuxuats");
                    var currentRow = 3;



                    worksheet.Cell(2, 1).Value = "Danh sách phiếu đã thu";

                    //worksheet.Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    //worksheet.Cell(currentRow, 1).Value = "Danh sách các xe đã bán";

                    worksheet.Cell(currentRow, 2).Value = "Số phiếu bán";
                    worksheet.Cell(currentRow, 3).Value = "Ngày bán";
                    worksheet.Cell(currentRow, 4).Value = "Số phiếu thu";
                    worksheet.Cell(currentRow, 5).Value = "Ngày thu";
                    worksheet.Cell(currentRow, 6).Value = "Nhân viên bán";
                    worksheet.Cell(currentRow, 7).Value = "Khách hàng mua";
                    worksheet.Cell(currentRow, 8).Value = "Nhân viên thu";
                    worksheet.Cell(currentRow, 9).Value = "Hình thức thu";
                    worksheet.Cell(currentRow, 10).Value = "Số tiền phải thu";


                    foreach (var slt in phieuDaThu)
                    {
                        currentRow++;
                        worksheet.Cell(currentRow, 2).Value = slt.Sopxk;
                        worksheet.Cell(currentRow, 3).Value = slt.Ngayxuat;
                        worksheet.Cell(currentRow, 3).Style.DateFormat.Format = "dd/MM/yyyy";
                        worksheet.Cell(currentRow, 4).Value = slt.IdptNavigation.Sopt;
                        worksheet.Cell(currentRow, 5).Value = slt.IdptNavigation.Ngaythu;
                        worksheet.Cell(currentRow, 5).Style.DateFormat.Format = "dd/MM/yyyy";
                        worksheet.Cell(currentRow, 6).Value = slt.IdnvNavigation.Tennv;
                        worksheet.Cell(currentRow, 7).Value = slt.IdkhNavigation.Tenkh;
                        worksheet.Cell(currentRow, 8).Value = slt.IdptNavigation.IdnvNavigation.Tennv;
                        worksheet.Cell(currentRow, 9).Value = slt.IdptNavigation.IdhtttNavigation.Tenhttt;
                        worksheet.Cell(currentRow, 10).Value = slt.Tongtienxuat;

                        tongTienThu += slt.Tongtienxuat;
                        tongTienDaThu += slt.IdptNavigation.Tongtienthu;
                    }
                    currentRow += 3;
                    worksheet.Cell(currentRow, 2).Value = "Tổng tiền phải thu";
                    worksheet.Cell(currentRow, 3).Value = tongTienThu;
                    currentRow += 3;
                    double? tienNo = tongTienDaThu - tongTienThu;
                    if(tienNo < 0)
                    {
                        worksheet.Cell(currentRow, 2).Value = "Tổng tiền còn lại";
                        worksheet.Cell(currentRow, 3).Value = "Cần thu thêm " + (tienNo*-1);
                    }
                    else
                    {
                        worksheet.Cell(currentRow, 2).Value = "Tổng tiền còn lại";
                        worksheet.Cell(currentRow, 3).Value = "Đã thu đủ";
                    }
                    using (var stream = new MemoryStream())
                    {

                        worksheet.Columns().AdjustToContents();
                        worksheet.RangeUsed().Style.Border.OutsideBorder = XLBorderStyleValues.Thick;
                        workbook.SaveAs(stream);
                        var content = stream.ToArray();
                        return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Danhsachphieudathu.xlsx");
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
