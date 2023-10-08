using AgentManager.WebApp.Models;
using AgentManager.WebApp.Models.Data;
using AgentManager.WebApp.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security;

namespace AgentManager.WebApp.Controllers
{
    public class StaffController : Controller
    {
        private readonly AgentManagerDbContext _context;
        DBHelper dbHelper;
        public StaffController(AgentManagerDbContext context, AgentManagerDbContext db)
        {
            _context = context;
            dbHelper = new DBHelper(db);
        }

        // GET: Agent
        public async Task<IActionResult> Index()
        {
           // var agentManagerDbContext = _context.Staffs.Include(a => a.Position).Include(a => a.Department);
            var agentManagerDbContext = _context.Staffs.Include(a => a.Position);
            return View(await agentManagerDbContext.ToListAsync());
        }

        // GET: Agent/Details/5
        public async Task<IActionResult> Details(string? id)
        {
            StaffVM staffVM = new StaffVM()
            {
                StaffName = dbHelper.GetStaffByID(id).StaffName,
                Address = dbHelper.GetStaffByID(id).Address,
                Gender = dbHelper.GetStaffByID(id).Gender,
                DoB = dbHelper.GetStaffByID(id).DoB,
                Email = dbHelper.GetStaffByID(id).Email,
                PositionId = dbHelper.GetStaffByID(id).PositionId,          
            };
            if (staffVM == null) return NotFound();
            
            else return View(staffVM);
        }

        // GET: Agent/Create
        public IActionResult Create()
        {
            ViewData["PositionId"] = new SelectList(_context.Positions, "PositionId", "PositionName");
            //ViewData["DepartmentId"] = new SelectList(_context.Departments, "DepartmentId", "DepartmentName");
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
                //newStaff.DepartmentId = staff.DepartmentId;
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
            //ViewData["DepartmentId"] = new SelectList(_context.Departments, "DepartmentId", "DepartmentName");
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
            ViewData["PositionId"] = new SelectList(_context.Positions, "PositionId", "PositionName");
            //ViewData["DepartmentId"] = new SelectList(_context.Departments, "DepartmentId", "DepartmentName");
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
    }
}
