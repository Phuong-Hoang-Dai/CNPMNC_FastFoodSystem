using FastFoodSystem.WebApp.Models;
using FastFoodSystem.WebApp.Models.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace FastFoodSystem.WebApp.Controllers
{
    [Authorize (Roles = "Admin,Manager")]
    public class VoucherController : Controller
    {
        private readonly FastFoodSystemDbContext _context;
        DBHelper dbHelper;
        public VoucherController(FastFoodSystemDbContext context, FastFoodSystemDbContext db)
        {
            _context = context;
            dbHelper = new DBHelper(db);
        }
        public async Task<IActionResult> Index(string searchText = "")
        {
            ViewBag.SearchText = searchText;
            var fFSVouchers = _context.FFSVouchers;

            if (!String.IsNullOrEmpty(searchText))
            {
                var fFSVouchersListSearch = _context.FFSVouchers
                    .Where(a => a.FFSVoucherId.Contains(searchText));

                return View(await fFSVouchersListSearch.ToListAsync());
            }
            return View(await fFSVouchers.ToListAsync());
        }
        public async Task<IActionResult> Details(string? id)
        {
            var voucher = dbHelper.GetVoucherByID(id);

            if (voucher == null) return NotFound();

            FFSVoucher voucher1 = new FFSVoucher()
            {
                FFSVoucherId = dbHelper.GetVoucherByID(id).FFSVoucherId,
                Num = dbHelper.GetVoucherByID(id).Num,
                Price = dbHelper.GetVoucherByID(id).Price,
                StartDate = dbHelper.GetVoucherByID(id).StartDate,
                EndDate = dbHelper.GetVoucherByID(id).StartDate,
                State = dbHelper.GetVoucherByID(id).State,
            };
            if (voucher1 == null) return NotFound();

            else return View(voucher1);
        }
        public async Task<IActionResult> Delete(string? id)
        {
            if (id == null || _context.FFSVouchers == null)
            {
                return NotFound();
            }
            var voucher = await _context.FFSVouchers
                .FirstOrDefaultAsync(m => m.FFSVoucherId == id);
            if (voucher == null)
            {
                return NotFound();
            }
            return View(voucher);
        }
        // POST: Voucher/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.FFSVouchers == null)
            {
                return Problem("Entity set 'AgentManagerDbContext.Agents'  is null.");
            }
            var voucher = await _context.FFSVouchers.FindAsync(id);
            if (voucher != null)
            {
                _context.FFSVouchers.Remove(voucher);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Edit(string? id)
        {
            if (id == null || _context.FFSVouchers == null)
            {
                return NotFound();
            }

            var voucher = await _context.FFSVouchers.FindAsync(id);
            if (voucher == null)
            {
                return NotFound();
            }
            return View(voucher);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("FFSVoucherId, Num, StartDate, EndDate, State, Price")] FFSVoucher voucher)
        {
            if (id != voucher.FFSVoucherId)
            {
                return NotFound();
            }
            var crvoucher = await _context.FFSVouchers.FindAsync(id);
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Entry(crvoucher).CurrentValues.SetValues(voucher);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    ModelState.AddModelError(String.Empty, "Có lỗi xảy ra khi cập nhập dữ liệu");
                }

            }

            return View(voucher);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(FFSVoucher voucher)
        {
            if (ModelState.IsValid)
            {
                FFSVoucher newVoucher = new FFSVoucher();

                newVoucher.FFSVoucherId = voucher.FFSVoucherId;
                newVoucher.Num = voucher.Num;
                newVoucher.Price = voucher.Price;
                newVoucher.StartDate = voucher.StartDate;
                newVoucher.EndDate = voucher.StartDate;
                newVoucher.State = voucher.State;

                _context.Add(newVoucher);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(voucher);
        }
    }
}
