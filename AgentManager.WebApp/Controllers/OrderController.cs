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

namespace AgentManager.WebApp.Controllers
{
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
    }
}
