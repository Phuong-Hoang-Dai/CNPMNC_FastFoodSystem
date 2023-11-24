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
    [Authorize(Roles = "Admin,Manager,Staff")]
    public class FFSIngredientsController : Controller
    {
        private readonly FastFoodSystemDbContext _context;

        public FFSIngredientsController(FastFoodSystemDbContext context)
        {
            _context = context;
        }

        // GET: FFSIngredients
        public async Task<IActionResult> Index(string searchText = "")
        {
            ViewBag.SearchText = searchText;
            var fastFoodSystemDbContext = _context.FFSIngredients.Include(f => f.FFSCatere);

            if (!String.IsNullOrEmpty(searchText))
            {
                List<FFSIngredient> ingredientListSearch = _context.FFSIngredients.Include(f => f.FFSCatere)
                    .Where(a => a.FFSIngredientId.Contains(searchText)).ToList();

                List<FFSIngredient> ingredientListSearchByName = _context.FFSIngredients.Include(f => f.FFSCatere)
                    .Where(a => a.Name.Contains(searchText)).ToList();

                foreach (var item in ingredientListSearchByName)
                    ingredientListSearch.Add(item);

                return View(ingredientListSearch);
            }

            return View(await fastFoodSystemDbContext.ToListAsync());
        }

        // GET: FFSIngredients/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.FFSIngredients == null)
            {
                return NotFound();
            }

            var fFSIngredient = await _context.FFSIngredients
                .Include(f => f.FFSCatere)
                .FirstOrDefaultAsync(m => m.FFSIngredientId == id);
            if (fFSIngredient == null)
            {
                return NotFound();
            }

            return View(fFSIngredient);
        }

        // GET: FFSIngredients/Create
        [Authorize(Roles = "Admin,Manager")]
        public IActionResult Create()
        {
            ViewData["FFSCatereId"] = new SelectList(_context.FFSCateres, "FFSCatereId", "FFSCatereId");
            return View();
        }

        // POST: FFSIngredients/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> Create([Bind("FFSIngredientId,Name,FFSCatereId")] FFSIngredient fFSIngredient)
        {
            _context.Add(fFSIngredient);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
            //ViewData["FFSCatereId"] = new SelectList(_context.FFSCateres, "FFSCatereId", "FFSCatereId", fFSIngredient.FFSCatereId);
            //return View(fFSIngredient);
        }

        // GET: FFSIngredients/Edit/5
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.FFSIngredients == null)
            {
                return NotFound();
            }

            var fFSIngredient = await _context.FFSIngredients.FindAsync(id);
            if (fFSIngredient == null)
            {
                return NotFound();
            }
            ViewData["FFSCatereId"] = new SelectList(_context.FFSCateres, "FFSCatereId", "FFSCatereId", fFSIngredient.FFSCatereId);
            return View(fFSIngredient);
        }

        // POST: FFSIngredients/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> Edit(string id, [Bind("FFSIngredientId,Name,Quantity,FFSCatereId")] FFSIngredient fFSIngredient)
        {
            if (id != fFSIngredient.FFSIngredientId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(fFSIngredient);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FFSIngredientExists(fFSIngredient.FFSIngredientId))
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
            ViewData["FFSCatereId"] = new SelectList(_context.FFSCateres, "FFSCatereId", "FFSCatereId", fFSIngredient.FFSCatereId);
            return View(fFSIngredient);
        }

        // GET: FFSIngredients/Delete/5
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.FFSIngredients == null)
            {
                return NotFound();
            }

            var fFSIngredient = await _context.FFSIngredients
                .Include(f => f.FFSCatere)
                .FirstOrDefaultAsync(m => m.FFSIngredientId == id);
            if (fFSIngredient == null)
            {
                return NotFound();
            }

            return View(fFSIngredient);
        }

        // POST: FFSIngredients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.FFSIngredients == null)
            {
                return Problem("Entity set 'FastFoodSystemDbContext.FFSIngredients'  is null.");
            }
            var fFSIngredient = await _context.FFSIngredients.FindAsync(id);
            if (fFSIngredient != null)
            {
                _context.FFSIngredients.Remove(fFSIngredient);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FFSIngredientExists(string id)
        {
          return (_context.FFSIngredients?.Any(e => e.FFSIngredientId == id)).GetValueOrDefault();
        }
    }
}
