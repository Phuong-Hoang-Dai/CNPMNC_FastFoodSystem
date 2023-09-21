using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AgentManager.WebApp.Models.Data;

namespace AgentManager.WebApp.Controllers
{
    public class DeliveryNotesController : Controller
    {
        private readonly AgentManagerDbContext _context;

        public DeliveryNotesController(AgentManagerDbContext context)
        {
            _context = context;
        }

        // GET: DeliveryNotes
        public async Task<IActionResult> Index()
        {
            var agentManagerDbContext = _context.DeliveryNotes.Include(d => d.Agent).Include(d => d.Staff);
            return View(await agentManagerDbContext.ToListAsync());
        }

        // GET: DeliveryNotes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.DeliveryNotes == null)
            {
                return NotFound();
            }

            var deliveryNote = await _context.DeliveryNotes
                .Include(d => d.Agent)
                .Include(d => d.Staff)
                .FirstOrDefaultAsync(m => m.DeliveryNoteId == id);
            deliveryNote.DeliveryNoteDetails = await _context.DeliveryNoteDetails.
                Where(d => d.DeliveryNoteId.Equals(deliveryNote.DeliveryNoteId)).Include(d => d.Product).ToListAsync();
            if (deliveryNote == null)
            {
                return NotFound();
            }

            return View(deliveryNote);
        }

        // GET: DeliveryNotes/Create
        public IActionResult Create()
        {
            ViewData["AgentId"] = new SelectList(_context.Agents, "AgentId", "AgentName");
            ViewData["StaffId"] = new SelectList(_context.Staffs, "Id", "StaffName");
            return View();
        }

        // POST: DeliveryNotes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DeliveryNoteId,CreatedDate,TotalPrice,Payment,AgentId,StaffId")] DeliveryNote deliveryNote)
        {
            if (ModelState.IsValid)
            {
                _context.Add(deliveryNote);
                await _context.SaveChangesAsync();
                return RedirectToAction("Create","DeliveryNoteDetails", new { id = deliveryNote.DeliveryNoteId });
            }
            ViewData["AgentId"] = new SelectList(_context.Agents, "AgentId", "AgentName");
            ViewData["StaffId"] = new SelectList(_context.Staffs, "Id", "StaffName");
            return View(deliveryNote);
        }

        // GET: DeliveryNotes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.DeliveryNotes == null)
            {
                return NotFound();
            }

            var deliveryNote = await _context.DeliveryNotes.FindAsync(id);;
            deliveryNote.DeliveryNoteDetails = await _context.DeliveryNoteDetails.
                Where(d => d.DeliveryNoteId.Equals(deliveryNote.DeliveryNoteId)).Include(d => d.Product).ToListAsync();

            if (deliveryNote == null)
            {
                return NotFound();
            }
            ViewData["AgentId"] = new SelectList(_context.Agents, "AgentId", "AgentName");
            ViewData["StaffId"] = new SelectList(_context.Staffs, "Id", "StaffName");
            return View(deliveryNote);
        }

        // POST: DeliveryNotes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DeliveryNoteId,CreatedDate,TotalPrice,Payment,AgentId,StaffId")] DeliveryNote deliveryNote)
        {
            if (id != deliveryNote.DeliveryNoteId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(deliveryNote);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DeliveryNoteExists(deliveryNote.DeliveryNoteId))
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
            deliveryNote.DeliveryNoteDetails = await _context.DeliveryNoteDetails.
                Where(d => d.DeliveryNoteId.Equals(deliveryNote.DeliveryNoteId)).Include(d => d.Product).ToListAsync();
            ViewData["AgentId"] = new SelectList(_context.Agents, "AgentId", "AgentName");
            ViewData["StaffId"] = new SelectList(_context.Staffs, "Id", "StaffName");
            return View(deliveryNote);
        }

        // GET: DeliveryNotes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.DeliveryNotes == null)
            {
                return NotFound();
            }

            var deliveryNote = await _context.DeliveryNotes
                .Include(d => d.Agent)
                .Include(d => d.Staff)
                .FirstOrDefaultAsync(m => m.DeliveryNoteId == id);
            if (deliveryNote == null)
            {
                return NotFound();
            }

            return View(deliveryNote);
        }

        // POST: DeliveryNotes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.DeliveryNotes == null)
            {
                return Problem("Entity set 'AgentManagerDbContext.DeliveryNotes'  is null.");
            }
            var deliveryNote = await _context.DeliveryNotes.FindAsync(id);
            if (deliveryNote != null)
            {
                _context.DeliveryNotes.Remove(deliveryNote);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DeliveryNoteExists(int id)
        {
          return (_context.DeliveryNotes?.Any(e => e.DeliveryNoteId == id)).GetValueOrDefault();
        }
    }
}
