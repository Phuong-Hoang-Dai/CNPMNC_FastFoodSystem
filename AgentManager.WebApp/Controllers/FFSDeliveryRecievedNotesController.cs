﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FastFoodSystem.WebApp.Models.Data;
using Newtonsoft.Json;
using FastFoodSystem.WebApp.Models.ViewModel;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace FastFoodSystem.WebApp.Controllers
{
    public class FFSDeliveryRecievedNotesController : Controller
    {
        private readonly FastFoodSystemDbContext _context;

        public FFSDeliveryRecievedNotesController(FastFoodSystemDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var agentManagerDbContext = _context.FFSDeliveryRecievedNotes.Include(f => f.Staff);
            return View(await agentManagerDbContext.ToListAsync());
        }

        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.FFSDeliveryRecievedNotes == null)
            {
                return NotFound();
            }

            var fFSDeliveryRecievedNote = await _context.FFSDeliveryRecievedNotes
                .Include(f => f.Staff)
                .FirstOrDefaultAsync(m => m.FFSDeliveryRecievedNoteId == id);
            fFSDeliveryRecievedNote.FFSShipments = _context.FFSShipments
                .Include(n => n.FFSIngredient).Where(i => i.FFSDeliveryRecievedNoteId == id).ToList();
            if (fFSDeliveryRecievedNote == null)
            {
                return NotFound();
            }

            return View(fFSDeliveryRecievedNote);
        }

        public IActionResult Create()
        {
            var Ingredients = new SelectList(_context.FFSIngredients, "FFSIngredientId", "Name");
            ViewBag.Ingredients = Ingredients;
            FFSDeliveryRecievedNote deliveryRecievedNote = new FFSDeliveryRecievedNote
            {
                FFSShipments = new List<FFSShipment>()
                {
                    new FFSShipment { Quantity = 1} 
                }
            };
            return View(deliveryRecievedNote);
        }

        [HttpPost]
        public async Task<IActionResult> Create(FFSDeliveryRecievedNote deliveryRecievedNote)
        {
            deliveryRecievedNote.State = "nhập";
            deliveryRecievedNote.Date = DateTime.Now;
            deliveryRecievedNote.FFSDeliveryRecievedNoteId = deliveryRecievedNote.Date.ToOADate().ToString(); 
            deliveryRecievedNote.StaffId = "1680b360-ab1f-4762-ba3b-12d3fe304d48";
            _context.Add(deliveryRecievedNote);

            foreach (var item in deliveryRecievedNote.FFSShipments)
            {
                item.FFSDeliveryRecievedNoteId = deliveryRecievedNote.FFSDeliveryRecievedNoteId;
                _context.Add(item);
                
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
       
        }




        public IActionResult CreateExport()
        {
            var Ingredients = new SelectList(_context.FFSIngredients, "FFSIngredientId", "Name");
            ViewBag.Ingredients = Ingredients;
            FFSDeliveryRecievedNote deliveryRecievedNote = new FFSDeliveryRecievedNote
            {
                FFSShipments = new List<FFSShipment>()
                {
                    new FFSShipment { Quantity = 1}
                }
            };

            return View(deliveryRecievedNote);
        }

        [HttpPost]
        public async Task<IActionResult> CreateExport(FFSDeliveryRecievedNote deliveryRecievedNote)
        {
            deliveryRecievedNote.State = "xuất";
            deliveryRecievedNote.Date = DateTime.Now.Date;
            deliveryRecievedNote.FFSDeliveryRecievedNoteId = deliveryRecievedNote.Date.ToOADate().ToString();
            deliveryRecievedNote.StaffId = "1680b360-ab1f-4762-ba3b-12d3fe304d48";
            _context.Add(deliveryRecievedNote);
            foreach (var item in deliveryRecievedNote.FFSShipments)
            {
                item.FFSDeliveryRecievedNoteId = deliveryRecievedNote.FFSDeliveryRecievedNoteId;
                _context.Add(item);
                FFSIngredient ingredient = await _context.FFSIngredients.FindAsync(item.FFSIngredientId);
                if (item.Quantity > ingredient.Quantity)
                {
                    ModelState.AddModelError("Quantity", $"Nguyên liệu {ingredient.Name} không đủ, chỉ còn {ingredient.Quantity} đơn vị");
                    return View(deliveryRecievedNote);
                }
                else
                {
                    ingredient.Quantity -= item.Quantity;
                    _context.Update(ingredient);
                }
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }





        // GET: FFSDeliveryRecievedNotes/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.FFSDeliveryRecievedNotes == null)
            {
                return NotFound();
            }

            var fFSDeliveryRecievedNote = await _context.FFSDeliveryRecievedNotes
                .Include(s => s.Staff)
                .FirstOrDefaultAsync(m => m.FFSDeliveryRecievedNoteId == id);
            fFSDeliveryRecievedNote.FFSShipments = _context.FFSShipments
               .Include(n => n.FFSIngredient).Where(i => i.FFSDeliveryRecievedNoteId == id).ToList();
            if (fFSDeliveryRecievedNote == null)
            {
                return NotFound();
            }
            return View(fFSDeliveryRecievedNote);
        }

        // POST: FFSDeliveryRecievedNotes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, FFSDeliveryRecievedNote fFSDeliveryRecievedNote)
        {
            if (id != fFSDeliveryRecievedNote.FFSDeliveryRecievedNoteId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(fFSDeliveryRecievedNote);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FFSDeliveryRecievedNoteExists(fFSDeliveryRecievedNote.FFSDeliveryRecievedNoteId))
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
            ViewData["StaffId"] = new SelectList(_context.Staffs, "Id", "Id", fFSDeliveryRecievedNote.StaffId);
            return View(fFSDeliveryRecievedNote);
        }

        // GET: FFSDeliveryRecievedNotes/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.FFSDeliveryRecievedNotes == null)
            {
                return NotFound();
            }

            var fFSDeliveryRecievedNote = await _context.FFSDeliveryRecievedNotes
                .Include(f => f.Staff)
                .FirstOrDefaultAsync(m => m.FFSDeliveryRecievedNoteId == id);
            fFSDeliveryRecievedNote.FFSShipments = _context.FFSShipments
               .Include(n => n.FFSIngredient)
               .Where(i => i.FFSDeliveryRecievedNoteId == id).ToList();
            if (fFSDeliveryRecievedNote == null)
            {
                return NotFound();
            }

            return View(fFSDeliveryRecievedNote);
        }

        // POST: FFSDeliveryRecievedNotes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.FFSDeliveryRecievedNotes == null)
            {
                return Problem("Entity set 'AgentManagerDbContext.FFSDeliveryRecievedNotes'  is null.");
            }
            var fFSDeliveryRecievedNote = await _context.FFSDeliveryRecievedNotes.FindAsync(id);
            List<FFSShipment> shipments = _context.FFSShipments.Where(l => l.FFSDeliveryRecievedNoteId == fFSDeliveryRecievedNote.FFSDeliveryRecievedNoteId).ToList();
            if (fFSDeliveryRecievedNote != null)
            {
                _context.FFSDeliveryRecievedNotes.Remove(fFSDeliveryRecievedNote);
                foreach(var item in shipments)
                {
                    if(item != null)
                    {
                        _context.FFSShipments.Remove(item);
                    }
                }
            }
            
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FFSDeliveryRecievedNoteExists(string id)
        {
          return (_context.FFSDeliveryRecievedNotes?.Any(e => e.FFSDeliveryRecievedNoteId == id)).GetValueOrDefault();
        }
        public IActionResult NewItem()
        {
            var Ingredients = new SelectList(_context.FFSIngredients, "FFSIngredientId", "Name");
            ViewBag.Ingredients = Ingredients;

            return PartialView("_AddItem", new FFSShipment());
        }
        public IActionResult NewItemExport()
        {
            var Ingredients = new SelectList(_context.FFSIngredients, "FFSIngredientId", "Name");
            ViewBag.Ingredients = Ingredients;

            return PartialView("_AddItemExport", new FFSShipment());
        }
    }
}
