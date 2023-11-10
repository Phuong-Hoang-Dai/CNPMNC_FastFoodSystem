using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FastFoodSystem.WebApp.Models.Data;
using Microsoft.AspNetCore.Authorization;

namespace FastFoodSystem.WebApp.Controllers
{
    [Authorize (Roles = "Admin,Manager,Staff")]
    public class FFSCateresController : Controller
    {
        private readonly FastFoodSystemDbContext _context;

        public FFSCateresController(FastFoodSystemDbContext context)
        {
            _context = context;
        }

        // GET: FFSCateres
        public async Task<IActionResult> Index()
        {
              return _context.FFSCateres != null ? 
                          View(await _context.FFSCateres.ToListAsync()) :
                          Problem("Entity set 'FastFoodSystemDbContext.FFSCateres'  is null.");
        }

        // GET: FFSCateres/Details/5
        [Authorize (Roles = "Admin,Manager")]
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.FFSCateres == null)
            {
                return NotFound();
            }

            var fFSCatere = await _context.FFSCateres
                .FirstOrDefaultAsync(m => m.FFSCatereId == id);
            if (fFSCatere == null)
            {
                return NotFound();
            }

            return View(fFSCatere);
        }

        // GET: FFSCateres/Create
        [Authorize (Roles = "Admin,Manager")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: FFSCateres/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize (Roles = "Admin,Manager")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FFSCatereId,Name,Address,ContractId,PhoneNumber,EmailAddress")] FFSCatere fFSCatere)
        {
            if (ModelState.IsValid)
            {
                _context.Add(fFSCatere);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(fFSCatere);
        }

        // GET: FFSCateres/Edit/5
        [Authorize (Roles = "Admin,Manager")]
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.FFSCateres == null)
            {
                return NotFound();
            }

            var fFSCatere = await _context.FFSCateres.FindAsync(id);
            if (fFSCatere == null)
            {
                return NotFound();
            }
            return View(fFSCatere);
        }

        // POST: FFSCateres/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize (Roles = "Admin,Manager")]
        public async Task<IActionResult> Edit(string id, [Bind("FFSCatereId,Name,Address,ContractId,PhoneNumber,EmailAddress")] FFSCatere fFSCatere)
        {
            if (id != fFSCatere.FFSCatereId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(fFSCatere);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FFSCatereExists(fFSCatere.FFSCatereId))
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
            return View(fFSCatere);
        }

        // GET: FFSCateres/Delete/5
        [Authorize (Roles = "Admin,Manager")]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.FFSCateres == null)
            {
                return NotFound();
            }

            var fFSCatere = await _context.FFSCateres
                .FirstOrDefaultAsync(m => m.FFSCatereId == id);
            if (fFSCatere == null)
            {
                return NotFound();
            }

            return View(fFSCatere);
        }

        // POST: FFSCateres/Delete/5
        [Authorize (Roles = "Admin,Manager")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.FFSCateres == null)
            {
                return Problem("Entity set 'FastFoodSystemDbContext.FFSCateres'  is null.");
            }
            var fFSCatere = await _context.FFSCateres.FindAsync(id);
            if (fFSCatere != null)
            {
                _context.FFSCateres.Remove(fFSCatere);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FFSCatereExists(string id)
        {
          return (_context.FFSCateres?.Any(e => e.FFSCatereId == id)).GetValueOrDefault();
        }
    }
}
