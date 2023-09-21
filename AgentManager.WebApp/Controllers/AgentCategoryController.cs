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
	public class AgentCategoryController : Controller
    {
        private readonly AgentManagerDbContext _context;

        public AgentCategoryController(AgentManagerDbContext context)
        {
            _context = context;
        }

        // GET: AgentCategory
        public async Task<IActionResult> Index()
        {
              return _context.AgentCategories != null ? 
                          View(await _context.AgentCategories.ToListAsync()) :
                          Problem("Entity set 'AgentManagerDbContext.AgentCategories'  is null.");
        }

        // GET: AgentCategory/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.AgentCategories == null)
            {
                return NotFound();
            }

            var agentCategory = await _context.AgentCategories
                .FirstOrDefaultAsync(m => m.AgentCategoryId == id);
            if (agentCategory == null)
            {
                return NotFound();
            }

            return View(agentCategory);
        }

        // GET: AgentCategory/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AgentCategory/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AgentCategoryId,MaxDebt")] AgentCategory agentCategory)
        {
            if (ModelState.IsValid)
            {
                if (agentCategory.MaxDebt < 1000000)
                {
                    ModelState.AddModelError("MaxDebt", "Nợ tối đa quá nhỏ");
                    return View(agentCategory);
                }

                _context.Add(agentCategory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(agentCategory);
        }

        // GET: AgentCategory/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.AgentCategories == null)
            {
                return NotFound();
            }

            var agentCategory = await _context.AgentCategories.FindAsync(id);
            if (agentCategory == null)
            {
                return NotFound();
            }
            return View(agentCategory);
        }

        // POST: AgentCategory/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AgentCategoryId,MaxDebt")] AgentCategory agentCategory)
        {
            if (id != agentCategory.AgentCategoryId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (agentCategory.MaxDebt < 1000000)
                    {
                        ModelState.AddModelError("MaxDebt", "Nợ tối đa quá nhỏ");
                        return View(agentCategory);
                    }
                    _context.Update(agentCategory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AgentCategoryExists(agentCategory.AgentCategoryId))
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
            return View(agentCategory);
        }

        // GET: AgentCategory/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.AgentCategories == null)
            {
                return NotFound();
            }

            var agentCategory = await _context.AgentCategories
                .FirstOrDefaultAsync(m => m.AgentCategoryId == id);
            if (agentCategory == null)
            {
                return NotFound();
            }

            return View(agentCategory);
        }

        // POST: AgentCategory/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.AgentCategories == null)
            {
                return Problem("Entity set 'AgentManagerDbContext.AgentCategories'  is null.");
            }
            var agentCategory = await _context.AgentCategories.FindAsync(id);
            if (agentCategory != null)
            {
                var agent = await _context.Agents.Where(m => m.AgentCategoryId
                .Equals(agentCategory.AgentCategoryId)).FirstOrDefaultAsync();
                if (agent != null)
                {
                    ModelState.AddModelError("AgentCategoryId", "Đã tồn tại đại lý thuộc loại này");
                    return View(agentCategory);
                }

                _context.AgentCategories.Remove(agentCategory);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AgentCategoryExists(int id)
        {
          return (_context.AgentCategories?.Any(e => e.AgentCategoryId == id)).GetValueOrDefault();
        }
    }
}
