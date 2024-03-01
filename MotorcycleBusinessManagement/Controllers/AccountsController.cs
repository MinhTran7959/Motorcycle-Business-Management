using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MotorcycleBusinessManagement.Models;

namespace MotorcycleBusinessManagement.Controllers
{
    public class AccountsController : Controller
    {
        private readonly CarSalesContext _context;
        private readonly ILogger<AccountsController> _logger;
        public AccountsController(CarSalesContext context, ILogger<AccountsController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: Accounts
        public async Task<IActionResult> Index()
        {
            var activeAccounts = await _context.Accounts
           .Where(x => x.Active == true)
           .OrderByDescending(x => x.Idacc)
           .ToListAsync();

            var hiddenAccounts = await _context.Accounts
                .Where(x => x.Active == false)
                .OrderByDescending(x => x.Idacc)
                .ToListAsync();

            ViewBag.ActiveAccounts = activeAccounts;
            ViewBag.HiddenAccounts = hiddenAccounts;

            return View();
        }


        // GET: Accounts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Accounts == null)
            {
                return NotFound();
            }

            var account = await _context.Accounts
                .FirstOrDefaultAsync(m => m.Idacc == id);
            if (account == null)
            {
                return NotFound();
            }

            return View(account);
        }

        // GET: Accounts/Create
        //public IActionResult Create()
        //{
        //    return View();
        //}

        // POST: Accounts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Idacc,Username,Password,Phone,Email,Role,Active")] Account account)
        {
            if (ModelState.IsValid)
            {
                var check = _context.Accounts.FirstOrDefault(s => s.Username == account.Username);
                if (check == null)
                {
                    account.Password = GetMD5(account.Password);
                    account.Active = true;
                    _context.Add(account);
                    await _context.SaveChangesAsync();
                    ViewBag.Account = account.Username;
                    TempData["AlertSuccessMessage"] = account.Username;
                }
                else
                {
                    TempData["AlertErrorMessage"] = account.Username;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(account);
        }

        // GET: Accounts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Accounts == null)
            {
                return NotFound();
            }

            var account = await _context.Accounts.FindAsync(id);
            if (account == null)
            {
                return NotFound();
            }
            return View(account);
        }

        // POST: Accounts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Idacc,Username,Password,Phone,Email,Role,Active")] Account account)
        {
            if (id != account.Idacc)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    account.Password = GetMD5(account.Password);
                    _context.Update(account);
                    await _context.SaveChangesAsync();
                    TempData["AlertUpdateMessage"] = account.Username;
                }
                catch
                {
                    return RedirectToAction(nameof(Index));
                }
                
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Accounts/Edit/5
        public async Task<IActionResult> EditUser(int? id)
        {
            if (id == null || _context.Accounts == null)
            {
                return NotFound();
            }

            var account = await _context.Accounts.FindAsync(id);
            if (account == null)
            {
                return NotFound();
            }
            return View(account);
        }

        // POST: Accounts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditUser(int id, [Bind("Idacc,Username,Password,Phone,Email,Role,Active")] Account account)
        {
            if (id != account.Idacc)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    account.Password = GetMD5(account.Password);
                    _context.Update(account);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AccountExists(account.Idacc))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                TempData["AlertUpdateAccMessage"] = account.Username;
                return RedirectToAction("Trangchu", "Chitietxes");
            }
            return View(account);
        }

        public async Task<IActionResult> Hidden(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var account = await _context.Accounts
                .FirstOrDefaultAsync(m => m.Idacc == id);
            if (account == null)
            {
                return NotFound();
            }


            account.Active = false;

            await _context.SaveChangesAsync();
            TempData["AlertDeleteMessage"] = account.Username;
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Return(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var account = await _context.Accounts
                .FirstOrDefaultAsync(m => m.Idacc == id);
            if (account == null)
            {
                return NotFound();
            }


            account.Active = true;

            await _context.SaveChangesAsync();
            TempData["AlertReturnMessage"] = account.Username;
            return RedirectToAction(nameof(Index));
        }

        // GET: Accounts/Delete/5
        public async Task<IActionResult> Delete1(int? id)
        {
            if (id == null || _context.Accounts == null)
            {
                return NotFound();
            }

            var account = await _context.Accounts
                .FirstOrDefaultAsync(m => m.Idacc == id);
            if (account == null)
            {
                return NotFound();
            }

            return View(account);
        }

        // POST: Accounts/Delete/5
        [HttpPost, ActionName("Delete1")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Accounts == null)
            {
                return Problem("Entity set 'CarSalesContext.Accounts'  is null.");
            }
            var account = await _context.Accounts.FindAsync(id);
            if (account != null)
            {
                _context.Accounts.Remove(account);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AccountExists(int id)
        {
            return (_context.Accounts?.Any(e => e.Idacc == id)).GetValueOrDefault();
        }

        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            if (HttpContext.User.Identity.Name == null)
                return View("Login");
            else
            {
                if (string.IsNullOrWhiteSpace(returnUrl) || !returnUrl.StartsWith("/"))
                {
                    returnUrl = "/Chitietxes/Trangchu";
                }
                return Redirect(returnUrl);
            }
        }
        public static string GetMD5(string str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromData = Encoding.UTF8.GetBytes(str);
            byte[] targetData = md5.ComputeHash(fromData);
            string byte2String = null;
            for (int i = 0; i < targetData.Length; i++)
            {
                byte2String += targetData[i].ToString("x2");
            }
            return byte2String;
        }
        [HttpPost]
        public async Task<IActionResult> Login(Account taiKhoan, string returnUrl)
        {
            Account a = _context.Accounts
                .FirstOrDefault(x => x.Username == taiKhoan.Username);
            var f_password = GetMD5(taiKhoan.Password);
            if (a == null)
            {
                return Redirect("Login");
            }
            if (a.Password.Equals(f_password))
            {
                await SignInUser(a);

                if (string.IsNullOrWhiteSpace(returnUrl) || !returnUrl.StartsWith("/"))
                {
                    returnUrl = "/Chitietxes/Trangchu";
                }
                return Redirect(returnUrl);
            }
            else
            {
                TempData["LoginFailed"] = "";
                return RedirectToAction("Login");
            }
        }
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }
        private async Task SignInUser(Account accounts)
        {
            CarSalesContext context = new CarSalesContext();
            Account user = context.Accounts.Where(x => x.Idacc == accounts.Idacc).FirstOrDefault();
            //Account user = context.Account.Where(x => x.Username == accounts.Username).FirstOrDefault();

            var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Idacc.ToString()),
                    new Claim(ClaimTypes.Role, user.Role.ToString())
                };

            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity));
        }
    }
}
