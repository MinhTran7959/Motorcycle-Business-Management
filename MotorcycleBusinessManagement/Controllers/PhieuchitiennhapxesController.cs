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
    public class PhieuchitiennhapxesController : Controller
    {
        private readonly CarSalesContext _context;

        public PhieuchitiennhapxesController(CarSalesContext context)
        {
            _context = context;
        }

        // GET: Phieuchitiennhapxes
        public async Task<IActionResult> Indexold(string searchString, DateTime fromDate, DateTime toDate)
        {
            
            var phieuChiTien = await _context.Phieuchitiennhapxes.Include(p => p.IdhtttNavigation)
                .Include(p => p.IdnvNavigation)
                .OrderByDescending(b=>b.Idpc)
                .Where(c=>c.Active==true).ToListAsync();
            
            var phieuDaChi = await _context.Phieunhapkhos.Include(n => n.IdnccNavigation)
                                                        .Include(n=>n.IdnvNavigation)
                                                        .Include(n=>n.IdpcNavigation)
                                                        .OrderByDescending(b => b.Idpnk)
                                                        .Where(c => c.Active == true && c.Idpc != null && c.Dachi == true).ToListAsync();
            if (!string.IsNullOrEmpty(searchString))
            {
                phieuChiTien = await _context.Phieuchitiennhapxes.Include(p => p.IdhtttNavigation)
                .Include(p => p.IdnvNavigation)
                .OrderByDescending(b => b.Idpc)
                .Where(c => c.Active == true && c.Sopc.Contains(searchString) || c.IdhtttNavigation.Tenhttt.Contains(searchString)
                        || c.IdnvNavigation.Tennv.Contains(searchString)).ToListAsync();
                phieuDaChi = await _context.Phieunhapkhos.Include(n => n.IdnccNavigation)
                                                        .Include(n => n.IdnvNavigation)
                                                        .Include(n => n.IdpcNavigation)
                                                        .OrderByDescending(b => b.Idpnk)
                                                        .Where(c => c.Active == true && c.Idpc != null && c.Dachi == true
                                                        && c.Mapnk.Contains(searchString) || c.IdpcNavigation.Sopc.Contains(searchString))
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
                phieuChiTien = await _context.Phieuchitiennhapxes.Include(p => p.IdhtttNavigation)
                .Include(p => p.IdnvNavigation)
                .OrderByDescending(b => b.Idpc)
                .Where(c => c.Active == true && (c.Ngaychi >= fromDate) && (c.Ngaychi <= toDate)).ToListAsync();
                phieuDaChi = await _context.Phieunhapkhos.Include(n => n.IdnccNavigation)
                                                        .Include(n => n.IdnvNavigation)
                                                        .Include(n => n.IdpcNavigation)
                                                        .OrderByDescending(b => b.Idpnk)
                                                        .Where(c => c.Active == true && c.Idpc != null && c.Dachi == true
                                                        && (c.Ngaynhap >= fromDate) && (c.Ngaynhap <= toDate)
                                                        || (c.IdpcNavigation.Ngaychi >= fromDate) && (c.IdpcNavigation.Ngaychi <= toDate))
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
            ViewBag.phieuChiTien = phieuChiTien;
            ViewBag.phieuDaChi = phieuDaChi;
            return View();
        }

        public async Task<IActionResult> Index(string searchString, DateTime fromDate, DateTime toDate)
        {

            var phieuChiTien = await _context.Phieuchitiennhapxes.Include(p => p.IdhtttNavigation)
                .Include(p => p.IdnvNavigation)
                .OrderByDescending(b => b.Idpc)
                .Where(c => c.Active == true).ToListAsync();

            
            if (!string.IsNullOrEmpty(searchString))
            {
                phieuChiTien = await _context.Phieuchitiennhapxes.Include(p => p.IdhtttNavigation)
                .Include(p => p.IdnvNavigation)
                .OrderByDescending(b => b.Idpc)
                .Where(c => c.Active == true && c.Sopc.Contains(searchString) || c.IdhtttNavigation.Tenhttt.Contains(searchString)
                        || c.IdnvNavigation.Tennv.Contains(searchString)).ToListAsync();
            }

            var fdate = fromDate.ToString("ddMMyyyy");
            var tdate = toDate.ToString("ddMMyyyy");
            if (fdate != "01010001" && tdate != "01010001" && Int32.Parse(fdate) <= Int32.Parse(tdate))
            {
                phieuChiTien = await _context.Phieuchitiennhapxes.Include(p => p.IdhtttNavigation)
                .Include(p => p.IdnvNavigation)
                .OrderByDescending(b => b.Ngaychi)
                .Where(c => c.Active == true && (c.Ngaychi >= fromDate) && (c.Ngaychi <= toDate)).ToListAsync();
                
            }
            ViewBag.phieuChiTien = phieuChiTien;
            return View();
        }

        public async Task<IActionResult> IndexPDC(string searchString, DateTime fromDate, DateTime toDate)
        {

            var phieuDaChi = await _context.Phieunhapkhos.Include(n => n.IdnccNavigation)
                                                        .Include(n => n.IdnvNavigation)
                                                        .Include(n => n.IdpcNavigation)
                                                        .OrderByDescending(b => b.Idpnk)
                                                        .Where(c => c.Active == true && c.Idpc != null && c.Dachi == true).ToListAsync();
            if (!string.IsNullOrEmpty(searchString))
            {
                
                phieuDaChi = await _context.Phieunhapkhos.Include(n => n.IdnccNavigation)
                                                        .Include(n => n.IdnvNavigation)
                                                        .Include(n => n.IdpcNavigation)
                                                        .OrderByDescending(b => b.Idpnk)
                                                        .Where(c => c.Active == true && c.Idpc != null && c.Dachi == true
                                                        && c.Mapnk.Contains(searchString) || c.IdpcNavigation.Sopc.Contains(searchString))
                                                        .ToListAsync();
                
            }

            var fdate = fromDate.ToString("ddMMyyyy");
            var tdate = toDate.ToString("ddMMyyyy");
            if (fdate != "01010001" && tdate != "01010001" && Int32.Parse(fdate) <= Int32.Parse(tdate))
            {
                phieuDaChi = await _context.Phieunhapkhos.Include(n => n.IdnccNavigation)
                                                        .Include(n => n.IdnvNavigation)
                                                        .Include(n => n.IdpcNavigation)
                                                        .OrderByDescending(b => b.IdpcNavigation.Ngaychi)
                                                        .Where(c => c.Active == true && c.Idpc != null && c.Dachi == true
                                                        && (c.Ngaynhap >= fromDate) && (c.Ngaynhap <= toDate)
                                                        || (c.IdpcNavigation.Ngaychi >= fromDate) && (c.IdpcNavigation.Ngaychi <= toDate))
                                                        .ToListAsync();
                
            }
            ViewBag.phieuDaChi = phieuDaChi;
            return View();
        }


        [HttpGet]
        public IActionResult Create()
        {
            string Sophieutudong = TaoSoPhieu();
            ViewData["idtd"] = Sophieutudong;
            TempData["Pnk"] = _context.Phieunhapkhos.Include(a=>a.IdnccNavigation).Include(b=>b.IdnvNavigation)
                .Where(x => x.Active == true && x.Dachi == false && x.Idpc==null && x.Idnv != null && x.Idncc !=null && x.Tongtiennhap != 0 && x.Tongtiennhap != null).ToList();

            TempData["httt"] = _context.Httts.Where(x => x.Active == true).ToList();
            TempData["Nhanvien"] = _context.Nhanviens.Where(x => x.Active == true).ToList();
            Phieuchitiennhapxe pctnx = new Phieuchitiennhapxe();
            pctnx.Phieunhapkhos.Add(new Phieunhapkho() { Idpnk = 1 });
            return View(pctnx);
        }

        [HttpPost]
        public IActionResult Create(Phieuchitiennhapxe pct)
        {
            if (pct.Phieunhapkhos.Count() == 0)
            {
                try
                {

                    pct.Ngaychi = DateTime.Now;
                    pct.Active = true;
                    _context.Add(pct);
                    pct.Phieunhapkhos.RemoveAll(n => n.Active == false || n.Mapnk == null || n.Idpnk == null);
                    // Lặp qua danh sách Phieunhapkho
                    foreach (var updatedPhieunhapkho in pct.Phieunhapkhos)
                    {
                        // Kiểm tra xem Idct đã tồn tại hay chưa
                        Phieunhapkho existingPnk = pct.Phieunhapkhos
                            .FirstOrDefault(c => c.Idpnk == updatedPhieunhapkho.Idpnk);

                        if (existingPnk != null)
                        {
                            existingPnk.Mapnk = updatedPhieunhapkho.Mapnk;
                            existingPnk.Ngaynhap = updatedPhieunhapkho.Ngaynhap;
                            existingPnk.Sohd = updatedPhieunhapkho.Sohd;
                            existingPnk.Ngayhd = updatedPhieunhapkho.Ngayhd;
                            existingPnk.Dachi = true;
                            existingPnk.Tongtiennhap = updatedPhieunhapkho.Tongtiennhap;
                            existingPnk.Idncc = updatedPhieunhapkho.Idncc;
                            existingPnk.Idnv = updatedPhieunhapkho.Idnv;

                            _context.Phieunhapkhos.Update(existingPnk);
                        }
                        else
                        {
                            // Thêm mới Chitietxe
                            pct.Phieunhapkhos.Add(updatedPhieunhapkho);
                        }
                    }



                    // Lưu các thay đổi
                    _context.SaveChanges();

                    TempData["AlertErrorMessage"] = pct.Sopc;
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

                    pct.Ngaychi = DateTime.Now;
                    pct.Active = true;
                    _context.Add(pct);
                    pct.Phieunhapkhos.RemoveAll(n => n.Active == false || n.Mapnk == null || n.Idpnk == null);
                    // Lặp qua danh sách Phieunhapkho
                    foreach (var updatedPhieunhapkho in pct.Phieunhapkhos)
                    {
                        // Kiểm tra xem Idct đã tồn tại hay chưa
                        Phieunhapkho existingPnk = pct.Phieunhapkhos
                            .FirstOrDefault(c => c.Idpnk == updatedPhieunhapkho.Idpnk);

                        if (existingPnk != null)
                        {
                            existingPnk.Mapnk = updatedPhieunhapkho.Mapnk;
                            existingPnk.Ngaynhap = updatedPhieunhapkho.Ngaynhap;
                            existingPnk.Sohd = updatedPhieunhapkho.Sohd;
                            existingPnk.Ngayhd = updatedPhieunhapkho.Ngayhd;
                            existingPnk.Dachi = true;
                            existingPnk.Tongtiennhap = updatedPhieunhapkho.Tongtiennhap;
                            existingPnk.Idncc = updatedPhieunhapkho.Idncc;
                            existingPnk.Idnv = updatedPhieunhapkho.Idnv;

                            _context.Phieunhapkhos.Update(existingPnk);
                        }
                        else
                        {
                            // Thêm mới Chitietxe
                            pct.Phieunhapkhos.Add(updatedPhieunhapkho);
                        }
                    }



                    // Lưu các thay đổi
                    _context.SaveChanges();

                    TempData["AlertSuccessMessage"] = pct.Sopc;
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
            var phieuCu = _context.Phieuchitiennhapxes.OrderByDescending(x => x.Sopc).FirstOrDefault(p => p.Sopc.StartsWith($"PC{ngayThangNam}"));

            if (phieuCu != null)
            {
                // Tách lấy số phiếu hiện tại
                string soPhieuCu = phieuCu.Sopc.Substring(8);
                int soPhieuMoi = int.Parse(soPhieuCu) + 1;
                return $"PC{ngayThangNam}{soPhieuMoi:D4}";
            }
            else
            {
                // Nếu không có phiếu cùng ngày, bắt đầu từ 1
                return $"PC{ngayThangNam}0001";
            }
        }
        [HttpGet]
        public IActionResult Details(int id)
        {
            try
            {
                TempData["Pnk"] = _context.Phieunhapkhos.Include(a => a.IdnccNavigation).Include(b => b.IdnvNavigation)
                .Where(x => x.Active == true&&x.Idncc!=null&&x.Idnv!=null).ToList();
                TempData["httt"] = _context.Httts.Where(x => x.Active == true).ToList();
                TempData["Nhanvien"] = _context.Nhanviens.Where(x => x.Active == true).ToList();
                Phieuchitiennhapxe? pct = _context.Phieuchitiennhapxes
                    .Include(e => e.Phieunhapkhos)
                    .Where(a => a.Idpc == id).FirstOrDefault();

                return View(pct);
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
                TempData["Pnk"] = _context.Phieunhapkhos.Include(a => a.IdnccNavigation).Include(b => b.IdnvNavigation)
                .Where(x => x.Active == true && x.Idncc != null && x.Idnv!=null).ToList();

                TempData["httt"] = _context.Httts.Where(x => x.Active == true).ToList();
                TempData["Nhanvien"] = _context.Nhanviens.Where(x => x.Active == true).ToList();
                Phieuchitiennhapxe? pct = _context.Phieuchitiennhapxes
                    .Include(e => e.Phieunhapkhos)
                    .Where(a => a.Idpc == id).FirstOrDefault();

                return View(pct);
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        public IActionResult Delete(Phieuchitiennhapxe pct)
        {
            try { 
            Phieuchitiennhapxe? existingApplicant = _context.Phieuchitiennhapxes
                .Include(e => e.Phieunhapkhos)
                .FirstOrDefault(a => a.Idpc == pct.Idpc);
            existingApplicant.Active = false;

            var pnkActive = _context.Phieunhapkhos.Where(x => x.Idpc == pct.Idpc).ToList();
            foreach (var item in pnkActive)
            {
                item.Dachi = false;
                item.Idpc = null;
            }
            
            //_context.Attach(pnk);
            //_context.Entry(pnk).State = EntityState.Deleted;
            _context.SaveChanges();
            TempData["AlertDeleteMessage"] = existingApplicant.Sopc;
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
                TempData["Pnk"] = _context.Phieunhapkhos.Include(a => a.IdnccNavigation).Include(b => b.IdnvNavigation)
                .Where(x => x.Active == true && x.Dachi == false && x.Idpc == null || x.Idpc == id).ToList();

                TempData["httt"] = _context.Httts.Where(x => x.Active == true).ToList();
                TempData["Nhanvien"] = _context.Nhanviens.Where(x => x.Active == true).ToList();
                Phieuchitiennhapxe? pct = _context.Phieuchitiennhapxes
                    .Include(e => e.Phieunhapkhos)
                    .Where(a => a.Idpc == id).FirstOrDefault();

                return View(pct);
            }
            catch
            {
                return View();
            }
        }



        [HttpPost]
        public IActionResult Edit(int id, Phieuchitiennhapxe pct)
        {
            if (id != pct.Idpc)
            {
                return BadRequest(); // Xử lý trường hợp id không khớp
            }

            // Kiểm tra xem ứng viên có tồn tại trong cơ sở dữ liệu hay không
            Phieuchitiennhapxe? existingApplicant = _context.Phieuchitiennhapxes
                .Include(e => e.Phieunhapkhos)
                .FirstOrDefault(a => a.Idpc == pct.Idpc);

            if (existingApplicant == null)
            {
                return NotFound(); // Xử lý trường hợp không tìm thấy ứng viên
            }

            pct.Phieunhapkhos.RemoveAll(n => n.Active == false || n.Mapnk == null);
            List<Phieunhapkho> phieunhapkho1 = new List<Phieunhapkho>();
            Dictionary<int, int> idpnkMapping = new Dictionary<int, int>();

            foreach (Phieunhapkho pnk in pct.Phieunhapkhos)
            {
                int idpnk = pnk.Idpnk;
                Phieunhapkho nk = new Phieunhapkho();
                nk.Mapnk = pnk.Mapnk;
                nk.Ngaynhap = pnk.Ngaynhap;
                nk.Sohd = pnk.Sohd;
                nk.Ngayhd = pnk.Ngayhd;
                nk.Dachi = pnk.Dachi;
                nk.Tongtiennhap = pnk.Tongtiennhap;
                nk.Idncc = pnk.Idncc;
                nk.Idnv = pnk.Idnv;
                nk.Idpc = pnk.Idpc;
                nk.Active = pnk.Active;

                phieunhapkho1.Add(nk);

                // Kiểm tra NULL trước khi thực hiện các thay đổi
                idpnkMapping.Add(idpnk, nk.Idpnk);
            }
            TempData["IdpnkMapping"] = idpnkMapping;
            // Cập nhật thông tin của ứng viên
            existingApplicant.Sopc = pct.Sopc; // Thay thế bằng các trường thông tin khác cần cập nhật
            existingApplicant.Ngaychi = pct.Ngaychi;
            existingApplicant.Tongtienchi = pct.Tongtienchi;
            existingApplicant.Idnv = pct.Idnv;
            existingApplicant.Idhttt = pct.Idhttt;
            existingApplicant.Active = true;

            // Xóa tất cả các trải nghiệm cũ
            List<Phieunhapkho> phieunhapkhos = _context.Phieunhapkhos.Where(x => x.Idpc == pct.Idpc).ToList();
            foreach (var updatedPnk in phieunhapkhos)
            {
                updatedPnk.Idpc = null;
                _context.Phieunhapkhos.Update(updatedPnk);
            }
            _context.Phieunhapkhos.RemoveRange(phieunhapkhos);

            _context.SaveChanges();
            _context.Phieunhapkhos.AddRange(phieunhapkho1);
            _context.SaveChanges();
            TempData["AlertEditMessage"] = pct.Sopc;
            return RedirectToAction("Index");
        }


        public IActionResult UpdateChitietxeIdpnk()
        {
            // Lấy IdpnkMapping từ TempData
            Dictionary<int, int> idpnkMapping = TempData["IdpnkMapping"] as Dictionary<int, int>;

            if (idpnkMapping != null)
            {
                // Duyệt qua từng cặp idpnk cũ và idpnk mới và thực hiện cập nhật
                foreach (var kvp in idpnkMapping)
                {
                    int oldIdpnk = kvp.Key;
                    int newIdpnk = kvp.Value;

                    // Cập nhật idpnk của các bản ghi Chitietxe
                    List<Chitietxe> chitietxesToUpdate = _context.Chitietxes.Where(x => x.Idpnk == oldIdpnk).ToList();
                    foreach (var ctx in chitietxesToUpdate)
                    {
                        ctx.Idpnk = newIdpnk;
                    }

                    _context.Chitietxes.UpdateRange(chitietxesToUpdate);
                }

                // Lưu thay đổi vào cơ sở dữ liệu
                _context.SaveChanges();
            }

            // ... (Các xử lý khác nếu cần)

            return RedirectToAction("Index");
        }
        public IActionResult DownPDF(int? id)
        {
            try { 
            if (id == null || _context.Phieuchitiennhapxes == null)
            {
                return NotFound();
            }


            var pct = _context.Phieuchitiennhapxes
                .Include(p => p.IdhtttNavigation)
                .Include(p => p.IdnvNavigation)
                .Where(x => x.Active == true && x.Idpc == id && x.Idnv != null && x.Idhttt!=null).FirstOrDefault();

            var ctnhap = _context.Phieunhapkhos
                .Include(c => c.IdnccNavigation)
                .Include(c => c.IdnvNavigation)
                .Where(x => x.Active == true && x.Idpc == id && x.Idncc!=null && x.Idnv!=null).ToList();
            ViewBag.ctnhap = ctnhap;

            if (pct == null)
            {
                return NotFound();
            }

            return new ViewAsPdf("DownPDF", pct)
            {
                FileName = $"Phieuchitien {pct.Sopc}.pdf",
                PageOrientation = Rotativa.AspNetCore.Options.Orientation.Portrait,
                PageSize = Rotativa.AspNetCore.Options.Size.A4,
                ViewData = new ViewDataDictionary(new EmptyModelMetadataProvider(), new ModelStateDictionary())
                {
                    Model = pct,
                    ["ctnhap"] = ctnhap
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
            double? tongTienChi = 0;

            var phieuChiTien = await _context.Phieuchitiennhapxes
                .Include(p => p.IdhtttNavigation)
                .Include(p => p.IdnvNavigation)
                .OrderByDescending(b => b.Idpc)
                .Where(c => c.Active == true)
                .ToListAsync();

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Phieuchitiennhapxe");
                var currentRow = 3;



                worksheet.Cell(2, 1).Value = "Danh sách phiếu thu tiền bán xe";

                //worksheet.Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                //worksheet.Cell(currentRow, 1).Value = "Danh sách các xe đã bán";

                worksheet.Cell(currentRow, 2).Value = "Số phiếu chi";
                worksheet.Cell(currentRow, 3).Value = "Ngày chi";
                worksheet.Cell(currentRow, 4).Value = "Nhân viên thu";
                worksheet.Cell(currentRow, 5).Value = "Hình thức thu";
                worksheet.Cell(currentRow, 6).Value = "Số tiền";

                foreach (var slt in phieuChiTien)
                {
                    currentRow++;
                    worksheet.Cell(currentRow, 2).Value = slt.Sopc;
                    worksheet.Cell(currentRow, 3).Value = slt.Ngaychi;
                    worksheet.Cell(currentRow, 3).Style.DateFormat.Format = "dd/MM/yyyy";
                    worksheet.Cell(currentRow, 4).Value = slt.IdnvNavigation.Tennv;
                    worksheet.Cell(currentRow, 5).Value = slt.IdhtttNavigation.Tenhttt;
                    worksheet.Cell(currentRow, 6).Value = slt.Tongtienchi;

                    tongTienChi += slt.Tongtienchi;

                }
                currentRow += 3;
                worksheet.Cell(currentRow, 2).Value = "Tổng tiền chi";
                worksheet.Cell(currentRow, 3).Value = tongTienChi;

                using (var stream = new MemoryStream())
                {

                    worksheet.Columns().AdjustToContents();
                    worksheet.RangeUsed().Style.Border.OutsideBorder = XLBorderStyleValues.Thick;
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Danhsachphieuchi.xlsx");
                }
            }
            }
            catch
            {
                TempData["AlertErrorMessage"] = "";
                return RedirectToAction("Index");
            }
        }
        public async Task<IActionResult> ExportExcelPDC()
        {
            try { 
            double? tongTienChi = 0;
            double? tongTienDaChi = 0;

            var phieuDaChi = await _context.Phieunhapkhos.Include(n => n.IdnccNavigation)
                                                        .Include(n => n.IdnvNavigation)
                                                        .Include(n => n.IdpcNavigation)
                                                        .Include(n => n.IdpcNavigation.IdnvNavigation)
                                                        .Include(n => n.IdpcNavigation.IdhtttNavigation)
                                                        .OrderByDescending(b => b.Idpnk)
                                                        .Where(c => c.Active == true && c.Idpc != null && c.Dachi == true).ToListAsync();

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Phieudachi");
                var currentRow = 3;



                worksheet.Cell(2, 1).Value = "Danh sách phiếu đã chi";

                //worksheet.Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                //worksheet.Cell(currentRow, 1).Value = "Danh sách các xe đã bán";

                worksheet.Cell(currentRow, 2).Value = "Số phiếu nhập";
                worksheet.Cell(currentRow, 3).Value = "Ngày nhập";
                worksheet.Cell(currentRow, 4).Value = "Số phiếu chi";
                worksheet.Cell(currentRow, 5).Value = "Ngày chi";
                worksheet.Cell(currentRow, 6).Value = "Nhân viên nhập";
                worksheet.Cell(currentRow, 7).Value = "Nhà cung cấp";
                worksheet.Cell(currentRow, 8).Value = "Nhân viên chi";
                worksheet.Cell(currentRow, 9).Value = "Hình thức chi";
                worksheet.Cell(currentRow, 10).Value = "Số tiền phải chi";


                foreach (var slt in phieuDaChi)
                {
                    currentRow++;
                    worksheet.Cell(currentRow, 2).Value = slt.Mapnk;
                    worksheet.Cell(currentRow, 3).Value = slt.Ngaynhap;
                    worksheet.Cell(currentRow, 3).Style.DateFormat.Format = "dd/MM/yyyy";
                    worksheet.Cell(currentRow, 4).Value = slt.IdpcNavigation.Sopc;
                    worksheet.Cell(currentRow, 5).Value = slt.IdpcNavigation.Ngaychi;
                    worksheet.Cell(currentRow, 5).Style.DateFormat.Format = "dd/MM/yyyy";
                    worksheet.Cell(currentRow, 6).Value = slt.IdnvNavigation.Tennv;
                    worksheet.Cell(currentRow, 7).Value = slt.IdnccNavigation.Tenncc;
                    worksheet.Cell(currentRow, 8).Value = slt.IdpcNavigation.IdnvNavigation.Tennv;
                    if(slt.IdpcNavigation!= null && slt.IdpcNavigation.IdhtttNavigation != null)
                    {
                        worksheet.Cell(currentRow, 9).Value = slt.IdpcNavigation.IdhtttNavigation.Tenhttt;
                    }
                    else
                    {
                        worksheet.Cell(currentRow, 9).Value = "Không có";
                    }
                    worksheet.Cell(currentRow, 10).Value = slt.Tongtiennhap;

                    tongTienChi += slt.Tongtiennhap;
                    tongTienDaChi += slt.IdpcNavigation.Tongtienchi;
                }
                currentRow += 3;
                worksheet.Cell(currentRow, 2).Value = "Tổng tiền phải chi";
                worksheet.Cell(currentRow, 3).Value = tongTienChi;
                currentRow += 3;
                double? tienNo = tongTienDaChi - tongTienChi;
                if (tienNo < 0)
                {
                    worksheet.Cell(currentRow, 2).Value = "Số tiền còn lại";
                    worksheet.Cell(currentRow, 3).Value = "Cần thu thêm " + (tienNo * -1);
                }
                else
                {
                    worksheet.Cell(currentRow, 2).Value = "Số tiền còn lại";
                    worksheet.Cell(currentRow, 3).Value = "Đã thu đủ";
                }
                using (var stream = new MemoryStream())
                {

                    worksheet.Columns().AdjustToContents();
                    worksheet.RangeUsed().Style.Border.OutsideBorder = XLBorderStyleValues.Thick;
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Danhsachphieudachi.xlsx");
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
