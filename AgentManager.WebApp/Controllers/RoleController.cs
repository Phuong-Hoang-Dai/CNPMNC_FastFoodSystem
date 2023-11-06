using FastFoodSystem.WebApp.Models.Data;
using FastFoodSystem.WebApp.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace FastFoodSystem.WebApp.Controllers
{
    [Authorize(Roles = "Admin,Manager")]
    public class RoleController : Controller
    {
        private readonly FastFoodSystemDbContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleController(FastFoodSystemDbContext context, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _roleManager = roleManager;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _roleManager.Roles.ToListAsync());
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RoleVM role)
        {
            if (ModelState.IsValid)
            {
                var newRole = new IdentityRole(role.RoleName);
                await _roleManager.CreateAsync(newRole); 
                return RedirectToAction(nameof(Index));
            }
            return View(role);
        }

        // GET: Agent/Edit/5
        public async Task<IActionResult> Edit(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var role = await _roleManager.FindByIdAsync(id);

            if (role == null)
            {
                return NotFound();
            }

            EditRoleVM editRoleVM = new EditRoleVM()
            {
                RoleName = role.Name,
                RoleId = role.Id
            };

            return View(editRoleVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, EditRoleVM editRoleVM)
        {
            if (id != editRoleVM.RoleId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                IdentityRole? role = await _roleManager.FindByIdAsync(id);

                if (role == null)
                {
                    return NotFound();
                }
                role.Name = editRoleVM.RoleName;

                await _roleManager.UpdateAsync(role);
                 
                return RedirectToAction(nameof(Index));
            }
            return View(editRoleVM);
        }

        // GET: Agent/Delete/5
        public async Task<IActionResult> Delete(string? id)
        {
            var role = await _roleManager.FindByIdAsync(id);

            if (role == null)
            {
                return NotFound();
            }

            EditRoleVM editRoleVM = new EditRoleVM()
            {
                RoleName = role.Name,
                RoleId = role.Id
            };

            return View(editRoleVM);

        }

        // POST: Agent/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
           
            IdentityRole? role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                return NotFound();
            }

            await _roleManager.DeleteAsync(role);

            return RedirectToAction(nameof(Index));
        }
    }
}
