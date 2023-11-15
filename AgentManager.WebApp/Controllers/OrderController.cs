using FastFoodSystem.WebApp.Models;
using FastFoodSystem.WebApp.Models.Data;
using FastFoodSystem.WebApp.Models.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Runtime.InteropServices;
using NuGet.Protocol;
using Microsoft.AspNetCore.Authorization;
using iTextSharp.text.pdf;

namespace AgentManager.WebApp.Controllers
{
    //[Authorize (Roles = "Admin,Manager,Staff")]
    public class OrderController : Controller
    {
        // Setting Session form item cart
        private readonly ILogger<OrderController> logger;
        private readonly IHttpContextAccessor _contx;
        private readonly FastFoodSystemDbContext _context;
        //
        public int SelectedCategoryId { get; set; }
        DBHelper dBHelper;
        public OrderController(ILogger<OrderController> logger, IHttpContextAccessor contx, FastFoodSystemDbContext context)
        {
            this.logger = logger;
            _contx = contx;
            _context = context;
        }

        // GET: OrderController
        // Trong controller
        public async Task<IActionResult> Index(string selectedCategoryId = "BG")
        {
            var model = new ProductCategoryViewModel();
            model.Categories = await _context.FFSProductCategories.ToListAsync();

            if (selectedCategoryId != "")
            {
                model.Products = await _context.FFSProducts.Where(p => p.FFSProductCategoryId == selectedCategoryId).ToListAsync();
            }
            else
            {
                model.Products = await _context.FFSProducts.ToListAsync();
            }

            ViewBag.Categories = model.Categories;
            ViewBag.SelectedCategoryId = selectedCategoryId; // Truyền danh mục đã chọn vào ViewBag

            return View(model);
        }

        List<CartItem> cartItems = new List<CartItem>(); // Danh sách sản phẩm trong giỏ hàng
        [HttpPost]
        public IActionResult Index([FromBody] CartItem data)
        {
            try
            {
                string cartItemsString = _contx.HttpContext.Session.GetString("CartItems");
                cartItems = JsonConvert.DeserializeObject<List<CartItem>>(cartItemsString);
            }
            catch { }
            if (data != null)
            {
                string productId = data.FFSProductId;
                int quantity = data.Quantity;
                // Tìm sản phẩm trong danh sách giỏ hàng
                var existingItem = cartItems.FirstOrDefault(item => item.FFSProductId == productId);

                if (existingItem != null)
                {
                    // Cập nhật số lượng nếu sản phẩm đã tồn tại
                    existingItem.Quantity = quantity;
                }
                else
                {
                    // Thêm sản phẩm mới vào giỏ hàng
                    var product = new CartItem { FFSProductId = productId, Quantity = quantity };
                    cartItems.Add(product);
                }

                //Session saving - 1hour
                string cartItemString = JsonConvert.SerializeObject(cartItems);
                _contx.HttpContext.Session.SetString("CartItems", cartItemString);
            }
            return Ok(); // Trả về kết quả Ajax thành công
        }

        public IActionResult ListOrder()
        {
            var orders = _context.FFSOrders.ToList();
            orders.Reverse();
            foreach(var order in orders)
            {
                Console.WriteLine(order.FFSOrderId);
            }
            return View(orders);
        }
        
        public IActionResult Delete(int id)
        {
            var order = _context.FFSOrders.FirstOrDefault(_context => _context.FFSOrderId == id);
            Console.WriteLine(order.ToJson());
            List<FFSProductOrder> products = _context.FFSProductOrders
            .Where(item => item.FFSOrderId == id)
            .OrderBy(item => item.FFSOrderId)
            .ToList();
            ViewBag.Products = products;
            List<CartItem> _products = new List<CartItem> { };
            foreach (var product in products)
            {
                var obj = _context.FFSProducts.FirstOrDefault(item => item.FFSProductId == product.FFSProductId);
                CartItem _product = new CartItem()
                {
                    FFSProductId = obj.FFSProductId,
                    tenSanPham = obj.Name,
                    gia = obj.Price,
                    Quantity = product.Quantity,
                };
                _products.Add(_product);
            }

            return View(order);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Console.WriteLine("ID:");
            Console.WriteLine(id);
            if (_context.FFSOrders == null)
            {
                return Problem("Entity set 'FastFoodSystemDbContext.FFSOrders'  is null.");
            }
            var fFSOrder = await _context.FFSOrders.FindAsync(id);
            if (fFSOrder != null)
            {
                List<FFSProductOrder> products = _context.FFSProductOrders
                .Where(item => item.FFSOrderId == id)
                .OrderBy(item => item.FFSOrderId)
                .ToList();

                List<CartItem> _products = new List<CartItem> { };
                foreach (var product in products)
                {
                    _context.FFSProductOrders.Remove(product);
                }
                _context.FFSOrders.Remove(fFSOrder);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(ListOrder));
        }
        public IActionResult Details(int id)
        {
            var order = _context.FFSOrders.FirstOrDefault(_context => _context.FFSOrderId == id);
            List<FFSProductOrder> products = _context.FFSProductOrders
            .Where(item => item.FFSOrderId == id)
            .OrderBy(item => item.FFSOrderId)
            .ToList();
            ViewBag.Products = products;
            List<CartItem> _products = new List<CartItem> { };
            foreach (var product in products)
            {
                var obj = _context.FFSProducts.FirstOrDefault(item => item.FFSProductId == product.FFSProductId);
                CartItem _product = new CartItem()
                {
                    FFSProductId = obj.FFSProductId,
                    tenSanPham = obj.Name,
                    gia = obj.Price,
                    Quantity = product.Quantity,
                };
                _products.Add(_product);
            }
            
            return View(order);
        }
        public async Task<IActionResult> Edit(int id)
        {
            var order = _context.FFSOrders.FirstOrDefault(_context => _context.FFSOrderId == id);
            Console.WriteLine(order.ToJson());
            List<FFSProductOrder> products = _context.FFSProductOrders
            .Where(item => item.FFSOrderId == id)
            .OrderBy(item => item.FFSOrderId)
            .ToList();
            ViewBag.Products = products;
            List<CartItem> _products = new List<CartItem> { };
            foreach (var product in products)
            {
                var obj = _context.FFSProducts.FirstOrDefault(item => item.FFSProductId == product.FFSProductId);
                CartItem _product = new CartItem()
                {
                    FFSProductId = obj.FFSProductId,
                    tenSanPham = obj.Name,
                    gia = obj.Price,
                    Quantity = product.Quantity,
                };
                _products.Add(_product);
            }

            return View(order);
        }
        [HttpPost, ActionName("Update")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateComfirm(int id, FFSOrder updatedOrder, List<FFSProductOrder> products)
        {
            List<FFSProductOrder> _products = _context.FFSProductOrders
            .Where(item => item.FFSOrderId == id)
            .OrderBy(item => item.FFSOrderId)
            .ToList();
            // Update quantities in the database
            double updatedCash = 0;
            foreach (var product in products)
            {
                var existingProduct = _products.FirstOrDefault(item => item.FFSProductId == product.FFSProductId);
                int pricePr = _context.FFSProducts.FirstOrDefault(item => item.FFSProductId == existingProduct.FFSProductId).Price;
                //Console.WriteLine(existingProduct.ToJson());
                if (existingProduct != null)
                {
                    existingProduct.Quantity = product.Quantity;
                    _context.Entry(existingProduct).State = EntityState.Modified;
                    updatedCash += existingProduct.Quantity * pricePr;
                }
            }
            Console.WriteLine(updatedCash);
            var order = _context.FFSOrders.FirstOrDefault(item => item.FFSOrderId == id);
            order.Cash = updatedCash;

            await _context.SaveChangesAsync();
            ViewBag.Products = products;
            return RedirectToAction(nameof(ListOrder));
        }
    }
}
