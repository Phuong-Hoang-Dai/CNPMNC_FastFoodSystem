using FastFoodSystem.WebApp.Models.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AgentManager.WebApp.Controllers
{

    [Authorize(Roles = "Admin,Manager,Staff")]
    public class CategoriesController : Controller
    {
        private readonly FastFoodSystemDbContext _context;

        public CategoriesController(FastFoodSystemDbContext context)
        {
            _context = context;
        }

        // GET: CategoriesController
        public async Task<IActionResult> Index()
        {
            var ProductCategoriesContext = _context.FFSProductCategories;
            return View(await ProductCategoriesContext.ToListAsync());
        }
        // GET: CategoriesController/Create
        [Authorize(Roles = "Admin,Manager")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: CategoriesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> Create([Bind("FFSProductCategoryId, Name")] FFSProductCategory category)
        {
            if (ModelState.IsValid)
            {
                _context.Add(category);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else Console.WriteLine("Error");
            return View(category);
        }

        // GET: CategoriesController/Edit/5
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.FFSProductCategories == null)
            {
                return NotFound();
            }

            var category = await _context.FFSProductCategories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }
       
        // POST: CategoriesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> Edit(string id, [Bind("FFSProductCategoryId, Name")] FFSProductCategory productCategory)
        {
            if (id != productCategory.FFSProductCategoryId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productCategory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    Console.WriteLine("Not found");
                    return NotFound();
                }
                return RedirectToAction(nameof(Index));
            }
            return View(productCategory);
        }

        // GET: CategoriesController/Delete/5
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> Delete(string? id)
        {
            if (id == null || _context.FFSProductCategories == null)
            {
                return NotFound();
            }

            var productCategory = await _context.FFSProductCategories
                .FirstOrDefaultAsync(m => m.FFSProductCategoryId == id);
            if (productCategory == null)
            {
                return NotFound();
            }

            return View(productCategory);
        }

        // POST: CategoriesController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.FFSProductCategories == null)
            {
                return Problem("Entity set 'FastFoodSystemDbContext.FFSProductCategory'  is null.");
            }
            var category = await _context.FFSProductCategories.FindAsync(id);
            if (category != null)
            {
                _context.FFSProductCategories.Remove(category);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
