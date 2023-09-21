using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AgentManager.WebApp.Models.Data;
using Microsoft.AspNetCore.Authorization;

namespace AgentManager.WebApp.Controllers
{
	public class AgentController : Controller
    {
        private readonly AgentManagerDbContext _context;

        public AgentController(AgentManagerDbContext context)
        {
            _context = context;
        }

        // GET: Agent
        public async Task<IActionResult> Index()
        {
            var agentManagerDbContext = _context.Agents.Include(a => a.AgentCategory).Include(a => a.District);
            return View(await agentManagerDbContext.ToListAsync());
        }

        // GET: Agent/Details/5
        public async Task<IActionResult> Details(int? id)
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

        // GET: Agent/Create
        public IActionResult Create()
        {
            ViewData["AgentCategoryId"] = new SelectList(_context.AgentCategories, "AgentCategoryId", "MaxDebt");
            ViewData["DistrictId"] = new SelectList(_context.Districts, "DistrictID", "DistrictName");
            return View();
        }

        // POST: Agent/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AgentId,AgentName,Address,Phone,ReceptionDate,DistrictId,AgentCategoryId")] Agent agent)
        {
            if (ModelState.IsValid)
            {
                agent.ReceptionDate = DateTime.Now;
                _context.Add(agent);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AgentCategoryId"] = new SelectList(_context.AgentCategories, "AgentCategoryId", "MaxDebt");
            ViewData["DistrictId"] = new SelectList(_context.Districts, "DistrictID", "DistrictName");
            return View(agent);
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
            ViewData["AgentCategoryId"] = new SelectList(_context.AgentCategories, "AgentCategoryId", "MaxDebt");
            ViewData["DistrictId"] = new SelectList(_context.Districts, "DistrictID", "DistrictName");
            return View(agent);
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
