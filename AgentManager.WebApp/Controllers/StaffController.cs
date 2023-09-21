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

        public StaffController(AgentManagerDbContext context)
        {
            _context = context;
        }

        // GET: Agent
        public async Task<IActionResult> Index()
        {
            var agentManagerDbContext = _context.Staffs.Include(a => a.Position).Include(a => a.Department);
            return View(await agentManagerDbContext.ToListAsync());
        }

        // GET: Agent/Details/5
        public async Task<IActionResult> Details(string? id)
        {
            if (id == null || _context.Staffs == null)
            {
                return NotFound();
            }

            var staff = await _context.Staffs
                .Include(a => a.Position)
                .Include(a => a.Department)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (staff == null)
            {
                return NotFound();
            }

            return View(staff);
        }

        // GET: Agent/Create
        public IActionResult Create()
        {
            ViewData["PositionId"] = new SelectList(_context.Positions, "PositionId", "PositionName");
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "DepartmentId", "DepartmentName");
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
                newStaff.DepartmentId = staff.DepartmentId;
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
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "DepartmentId", "DepartmentName");
			var gender = new List<string> { "Nam", "Nữ" };
			ViewData["Gender"] = new SelectList(gender);
			return View(staff);
        }

        // GET: Agent/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Agents == null)
            {
                return NotFound();
            }

            var agent = await _context.Agents.FindAsync(id);
            if (agent == null)
            {
                return NotFound();
            }
            ViewData["PositionId"] = new SelectList(_context.Positions, "PositionId", "PositionName");
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "DepartmentId", "DepartmentName");
            return View();
        }

        // POST: Agent/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AgentId,AgentName,Address,Phone,ReceptionDate,DistrictId,AgentCategoryId")] Agent agent)
        {
            if (id != agent.AgentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(agent);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AgentExists(agent.AgentId))
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
            ViewData["AgentCategoryId"] = new SelectList(_context.AgentCategories, "AgentCategoryId", "MaxDebt");
            ViewData["DistrictId"] = new SelectList(_context.Districts, "DistrictID", "DistrictName");
            return View(agent);
        }

        // GET: Agent/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Agents == null)
            {
                return NotFound();
            }

            var agent = await _context.Agents
                .Include(a => a.AgentCategory)
                .Include(a => a.District)
                .FirstOrDefaultAsync(m => m.AgentId == id);
            if (agent == null)
            {
                return NotFound();
            }

            return View(agent);
        }

        // POST: Agent/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Agents == null)
            {
                return Problem("Entity set 'AgentManagerDbContext.Agents'  is null.");
            }
            var agent = await _context.Agents.FindAsync(id);
            if (agent != null)
            {
                _context.Agents.Remove(agent);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AgentExists(int id)
        {
            return (_context.Agents?.Any(e => e.AgentId == id)).GetValueOrDefault();
        }
    }
}
