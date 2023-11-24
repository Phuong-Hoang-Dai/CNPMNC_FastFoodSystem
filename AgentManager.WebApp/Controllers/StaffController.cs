using FastFoodSystem.WebApp.Models;
using FastFoodSystem.WebApp.Models.Data;
using FastFoodSystem.WebApp.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using iTextSharp.text.pdf;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using Microsoft.AspNetCore.Authorization;

namespace FastFoodSystem.WebApp.Controllers
{
    [Authorize(Roles = "Admin,Manager")]
    public class StaffController : Controller
    {
        private readonly FastFoodSystemDbContext _context;
        DBHelper dbHelper;
        public StaffController(FastFoodSystemDbContext context, FastFoodSystemDbContext db)
        {
            _context = context;
            dbHelper = new DBHelper(db);
        }

        // GET: Agent
        public async Task<IActionResult> Index(string searchText = "")
        {
            ViewBag.SearchText = searchText;
            var agentManagerDbContext = _context.Staffs.Include(a => a.Position);


            if (!String.IsNullOrEmpty(searchText))
            {
                var fFSVouchersListSearch = _context.Staffs.Include(a => a.Position)
                    .Where(a => a.StaffName.Contains(searchText));

                return View(await fFSVouchersListSearch.ToListAsync());
            }

            return View(await agentManagerDbContext.ToListAsync());
        }

        // GET: Agent/Details/5
        public async Task<IActionResult> Details(string? id)
        {
            var staff = dbHelper.GetStaffByID(id);

            if (staff == null) return NotFound();

            List<Position> positions = GetPositions();
            var position = positions.FirstOrDefault(p => p.PositionId == staff.PositionId);
            if (position == null) return NotFound();
            StaffVM staffVM = new StaffVM()
            {
                StaffName = dbHelper.GetStaffByID(id).StaffName,
                Address = dbHelper.GetStaffByID(id).Address,
                Gender = dbHelper.GetStaffByID(id).Gender,
                DoB = dbHelper.GetStaffByID(id).DoB,
                Email = dbHelper.GetStaffByID(id).Email,
                PositionId = dbHelper.GetStaffByID(id).PositionId,
                Position = position,
            };
            if (staffVM == null) return NotFound();

            else return View(staffVM);
        }

        private List<Position> GetPositions()
        {
            return _context.Positions.ToList();
        }

        // GET: Agent/Create
        public IActionResult Create()
        {
            ViewData["PositionId"] = new SelectList(_context.Positions, "PositionId", "PositionName");
            var gender = new List<string> { "Nam", "Nữ" };
            ViewData["Gender"] = new SelectList(gender);
            return View();
        }

        // POST: Agent/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(StaffVM staff)
        {
            if (ModelState.IsValid)
            {
                Staff newStaff = new Staff();

                newStaff.StaffName = staff.StaffName;
                newStaff.Gender = staff.Gender;
                newStaff.DoB = staff.DoB;
                newStaff.Address = staff.Address;
                newStaff.DepartmentId = 1;
                newStaff.PositionId = staff.PositionId;
                newStaff.Email = staff.Email;

                newStaff.Id = Guid.NewGuid().ToString();
                newStaff.UserName = staff.Email;
                newStaff.SecurityStamp = Guid.NewGuid().ToString();
                newStaff.EmailConfirmed = true;
                newStaff.PhoneNumberConfirmed = false;
                newStaff.TwoFactorEnabled = false;
                newStaff.LockoutEnabled = false;
                newStaff.AccessFailedCount = 0;

                _context.Add(newStaff);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PositionId"] = new SelectList(_context.Positions, "PositionId", "PositionName");
            var gender = new List<string> { "Nam", "Nữ" };
            ViewData["Gender"] = new SelectList(gender);
            return View(staff);
        }

        // GET: Agent/Edit/5
        public async Task<IActionResult> Edit(string? id)
        {
            if (id == null || _context.Staffs == null)
            {
                return NotFound();
            }

            var staff = await _context.Staffs.FindAsync(id);
            if (staff == null)
            {
                return NotFound();
            }
            var gender = new List<string> { "Nam", "Nữ" };
            ViewData["Gender"] = new SelectList(gender);
            ViewData["PositionId"] = new SelectList(_context.Positions, "PositionId", "PositionName");
            return View(staff);
        }

        // POST: Agent/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id, StaffName, Address, Gender, Dob, PositionId, Email")] Staff staff)
        {
            if (id != staff.Id)
            {
                return NotFound();
            }
            var crstaff = await _context.Staffs.FindAsync(id);
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Entry(crstaff).CurrentValues.SetValues(staff);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    ModelState.AddModelError(String.Empty, "Có lỗi xảy ra khi cập nhập dữ liệu");
                }

            }

            return View(staff);
        }

        // GET: Agent/Delete/5
        public async Task<IActionResult> Delete(string? id)
        {
            if (id == null || _context.Staffs == null)
            {
                return NotFound();
            }
            var staff = await _context.Staffs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (staff == null)
            {
                return NotFound();
            }
            return View(staff);
        }

        // POST: Agent/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Staffs == null)
            {
                return Problem("Entity set 'AgentManagerDbContext.Agents'  is null.");
            }
            var staff = await _context.Staffs.FindAsync(id);
            if (staff != null)
            {
                _context.Staffs.Remove(staff);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StaffExists(string id)
        {
            return (_context.Staffs?.Any(a => a.Id == id)).GetValueOrDefault();
        }

        public IActionResult ExportToPdf(string id)
        {
            var staff = _context.Staffs.FirstOrDefault(s => s.Email == id);

            if (staff == null)
            {
                return NotFound();
            }

            using (MemoryStream ms = new MemoryStream())
            {
                Document document = new Document(PageSize.A4, 25, 25, 30, 30);
                PdfWriter writer = PdfWriter.GetInstance(document, ms);
                document.Open();

                // Đọc mẫu HTML từ tệp hoặc chuỗi HTML
                string htmlContent = System.IO.File.ReadAllText("Views/Shared/template.html"); // Thay đổi đường dẫn

                htmlContent = htmlContent.Replace("[StaffName]", staff.StaffName)
                                          .Replace("[Address]", staff.Address)
                                          .Replace("[Gender]", staff.Gender)
                                          .Replace("[DoB]", staff.DoB.ToString())
                                          .Replace("[Email]", staff.Email);
                // Render HTML thành PDF
                HTMLWorker worker = new HTMLWorker(document);
                using (StringReader sr = new StringReader(htmlContent))
                {
                    worker.Parse(sr);
                }

                document.Close();

                byte[] pdfBytes = ms.ToArray();
                return File(pdfBytes, "application/pdf", "staff_info.pdf");
            }
        }
    }
}
