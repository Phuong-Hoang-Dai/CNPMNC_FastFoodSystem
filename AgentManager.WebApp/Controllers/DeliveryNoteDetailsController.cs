using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AgentManager.WebApp.Models.Data;
using AgentManager.WebApp.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;

namespace AgentManager.WebApp.Controllers
{
	public class DeliveryNoteDetailsController : Controller
    {
        private readonly AgentManagerDbContext _context;

        public DeliveryNoteDetailsController(AgentManagerDbContext context)
        {
            _context = context;
        }
        // GET: DeliveryNoteDetails/Create/
        public async Task<IActionResult> Create(int? id)
        {
            if (id == null || _context.DeliveryNotes == null)
            {
                return NotFound();
            }

            var DeliveryNotes = await _context.DeliveryNotes.FindAsync(id);
            if (DeliveryNotes == null)
            {
                return NotFound();
            }
            AddDeliveryNoteDetail addDeliveryNoteDetail = new AddDeliveryNoteDetail();
            addDeliveryNoteDetail.DeliveryNoteId = DeliveryNotes.DeliveryNoteId;
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductName");
            return View(addDeliveryNoteDetail);
        }

        // POST: DeliveryNoteDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int id, AddDeliveryNoteDetail addDeliveryNoteDetail)
        {
            Product product = _context.Products.Find(addDeliveryNoteDetail.ProductId);

            bool isAdded = _context.DeliveryNoteDetails
                            .Where(x => x.DeliveryNoteId.Equals(addDeliveryNoteDetail.DeliveryNoteId))
                            .Where(y => y.ProductId.Equals(addDeliveryNoteDetail.ProductId)).Any();
            if (isAdded)
            {
                ModelState.AddModelError("ProductId", "Sản phẩm này đã được thêm");
            }
            if(addDeliveryNoteDetail.Quantity < 1)
            {
                ModelState.AddModelError("Quantity", "Số lượng tối thiểu phải là một");
            }else if(addDeliveryNoteDetail.Quantity > product.InventoryQuantity)
            {
                ModelState.AddModelError("Quantity", "Số lượng vượt quá lượng hàng trong kho");
            }
            if (ModelState.IsValid)
            {
                

                DeliveryNoteDetail deliveryNoteDetail = new DeliveryNoteDetail();
                deliveryNoteDetail.ProductId = addDeliveryNoteDetail.ProductId;
                deliveryNoteDetail.Quantity = addDeliveryNoteDetail.Quantity;
                deliveryNoteDetail.DeliveryNoteId = addDeliveryNoteDetail.DeliveryNoteId;

                product.InventoryQuantity -= deliveryNoteDetail.Quantity;
                _context.Update(product);

                deliveryNoteDetail.Price = product.Price * deliveryNoteDetail.Quantity;
                _context.Add(deliveryNoteDetail);


                DeliveryNote? deliveryNote = _context.Find<DeliveryNote>(deliveryNoteDetail.DeliveryNoteId);
                deliveryNote.TotalPrice += deliveryNoteDetail.Price;
                _context.Update(deliveryNote);

                await _context.SaveChangesAsync();
                return RedirectToAction("Create", new { id = deliveryNote.DeliveryNoteId });
            }

            ViewData["DeliveryNoteId"] = addDeliveryNoteDetail.DeliveryNoteId; 
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductName", addDeliveryNoteDetail.ProductId);
            return View(addDeliveryNoteDetail);
        }

        // GET: DeliveryNoteDetails/Delete/5
        public async Task<IActionResult> Delete(int? productId, int? deliveryNoteId)
        {
            if (productId == null || _context.DeliveryNoteDetails == null)
            {
                return NotFound();
            }
            var deliveryNoteDetail = await _context.DeliveryNoteDetails.Where(m => m.ProductId == productId)
                .Where(k => k.DeliveryNoteId == deliveryNoteId).FirstOrDefaultAsync();
            if (deliveryNoteDetail == null)
            {
                return NotFound();
            }
            _context.DeliveryNoteDetails.Remove(deliveryNoteDetail);

            var deliveryNote = await _context.DeliveryNotes.FindAsync(deliveryNoteId);

            deliveryNote.TotalPrice -= deliveryNoteDetail.Price;

            _context.Update(deliveryNote);

            await _context.SaveChangesAsync();

            return RedirectToAction("Edit","DeliveryNotes",new {id = deliveryNoteId });
        }

        // POST: DeliveryNoteDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.DeliveryNoteDetails == null)
            {
                return Problem("Entity set 'AgentManagerDbContext.DeliveryNoteDetails'  is null.");
            }
            var deliveryNoteDetail = await _context.DeliveryNoteDetails.FindAsync(id);
            if (deliveryNoteDetail != null)
            {
                _context.DeliveryNoteDetails.Remove(deliveryNoteDetail);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DeliveryNoteDetailExists(int id)
        {
          return (_context.DeliveryNoteDetails?.Any(e => e.ProductId == id)).GetValueOrDefault();
        }
    }
}
