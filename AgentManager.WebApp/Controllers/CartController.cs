using FastFoodSystem.WebApp.Models.Data;
using FastFoodSystem.WebApp.Models;
using FastFoodSystem.WebApp.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Protocols;
using Newtonsoft.Json;
using NuGet.Protocol;
using Microsoft.EntityFrameworkCore;

namespace FastFoodSystem.WebApp.Controllers
{
    public class CartController : Controller
    {
        private readonly IHttpContextAccessor _contx;
        private readonly FastFoodSystemDbContext _context;
        DBHelper dBHelper;
        public CartController(FastFoodSystemDbContext db, FastFoodSystemDbContext context)
        {
            _contx = new HttpContextAccessor();
            dBHelper = new DBHelper(db);
            _context = context;
        }
        public void RetrieveCartitem(out List<CartItem> list, out decimal bill)
        {
            list = new List<CartItem>(); // Gán giá trị mặc định cho list
            bill = 0;
            string cartItemsString = _contx.HttpContext.Session.GetString("CartItems");

            if (!string.IsNullOrEmpty(cartItemsString))
            {
                List<CartItem> cartItems = JsonConvert.DeserializeObject<List<CartItem>>(cartItemsString);
                List<CartItem> sanPhamVMs = new List<CartItem>();

                foreach (var cartItem in cartItems)
                {
                    CartItem sanPhamVM = new CartItem()
                    {
                        FFSProductId = cartItem.FFSProductId,
                        tenSanPham = dBHelper.GetProductByID(cartItem.FFSProductId).Name,
                        anh = dBHelper.GetProductByID(cartItem.FFSProductId).Image,
                        gia = dBHelper.GetProductByID(cartItem.FFSProductId).Price,
                        Quantity = cartItem.Quantity
                    };
                    bill += sanPhamVM.total;
                    sanPhamVMs.Add(sanPhamVM);
                }

                list = sanPhamVMs;
            }
        }

        public IActionResult Index()
        {
            RetrieveCartitem(out List<CartItem> sanPhamVMs, out decimal bill);
            bool state = false;
            if (sanPhamVMs != null) state = true;
            ViewBag.Bill = bill;
            ViewBag.State = state;
            return View(sanPhamVMs);
        }


        [HttpPost]
        public IActionResult Index(bool state)
        {
            RetrieveCartitem(out List<CartItem> list, out decimal bill);
            int newOrderId = 0;
            var latestOrder = _context.FFSOrders.OrderByDescending(o => o.FFSOrderId).FirstOrDefault();

            if (latestOrder != null)
            {
                // Nếu có hoá đơn, tăng giá trị ID lên 1 để tạo ID mới
                newOrderId = latestOrder.FFSOrderId + 1;
            }
            // Tạo một hoá đơn mới với ID mới
            List<FFSProductOrder> productOrders = new List<FFSProductOrder>();
            foreach (var cartItem in list)
            {
                FFSProductOrder productOrder = new FFSProductOrder
                {
                    FFSProductId = cartItem.FFSProductId,
                    Quantity = cartItem.Quantity,
                    FFSOrderId = newOrderId
                };
                productOrders.Add(productOrder);
            }
            var newOrder = new FFSOrder
            {
                //Attribute
                Date = DateTime.Now,
                Cash = Convert.ToDouble(bill),
                StaffId = "00f8d252-bfa5-4d15-9484-970f67456d75",
                TableId = "Table456",
                FFSVoucherId = "P2",
                FFSProductOrders = productOrders
            };
            // Lưu hoá đơn mới vào cơ sở dữ liệu
            _context.FFSOrders.Add(newOrder);
            _context.SaveChanges();
            Console.WriteLine("Add Success");

            //Clean Cart
            HttpContext.Session.Clear(); 
            return RedirectToAction("Index", "Order");
        }
    }
}
