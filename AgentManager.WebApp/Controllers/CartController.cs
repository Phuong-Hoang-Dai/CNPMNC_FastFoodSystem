using FastFoodSystem.WebApp.Models.Data;
using FastFoodSystem.WebApp.Models;
using FastFoodSystem.WebApp.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Protocols;
using Newtonsoft.Json;
using NuGet.Protocol;
using Microsoft.EntityFrameworkCore;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.Text;

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
                StaffId = "22cddc5f-1379-40a3-bad8-16030613f304",
                TableId = "Table456",
                FFSVoucherId = "P2",
                FFSProductOrders = productOrders
            };
            // Lưu hoá đơn mới vào cơ sở dữ liệu
            _context.FFSOrders.Add(newOrder);
            _context.SaveChanges();
            Console.WriteLine("Add Success");

            
            return RedirectToAction("Bill", "Cart", new { id = newOrderId });
        }

        public IActionResult Bill(int id)
        {
            RetrieveCartitem(out List<CartItem> lst, out decimal totalbill);
            var bill = _context.FFSOrders.FirstOrDefault(o => o.FFSOrderId == id);
            foreach(var item in lst)
            {
                Console.WriteLine(item.FFSProductId);
            }
            Console.WriteLine(lst.ToJson());
            ViewBag.Bill = bill;
            ////Clean Cart
            /*HttpContext.Session.Clear();*/
            /*return RedirectToAction("ExportToPdf", "Cart", new { nid = id });*/
            return View(lst);
        }

        public IActionResult ExportToPdf(int id)
        {
            RetrieveCartitem(out List<CartItem> lst, out decimal totalbill);
            var bill = _context.FFSOrders.FirstOrDefault(o => o.FFSOrderId == id);

            using (MemoryStream ms = new MemoryStream())
            {
                Document document = new Document(PageSize.A6, 25, 25, 0, 0);
                PdfWriter writer = PdfWriter.GetInstance(document, ms);
                document.Open();

                // Đọc mẫu HTML từ tệp hoặc chuỗi HTML
                string htmlTemplate = System.IO.File.ReadAllText("Views/Shared/template2.html"); // Thay đổi đường dẫn

                // Tạo một StringBuilder để xây dựng nội dung HTML
                StringBuilder staffInfoHtml = new StringBuilder();

                foreach (var cart in lst)
                {
                    staffInfoHtml.Append($"<tr><td>{cart.tenSanPham}</td><td>{cart.Quantity}</td><td>{cart.gia}</td><td>{cart.total}</td></tr>");
                }

                // Thay thế {{StaffInfo}} bằng nội dung đã xây dựng
                htmlTemplate = htmlTemplate.Replace("{{StaffInfo}}", staffInfoHtml.ToString())
                                            .Replace("{{IdBill}}", id.ToString())
                                            .Replace("{{IdStaff}}", bill.StaffId)
                                            .Replace("{{Total}}", bill.Cash.ToString())
                                            .Replace("{{DateTime}}", bill.Date.ToString());


                // Render HTML thành PDF
                HTMLWorker worker = new HTMLWorker(document);
                using (StringReader sr = new StringReader(htmlTemplate))
                {
                    worker.Parse(sr);
                }
                document.Close();
                HttpContext.Session.Clear();
                byte[] pdfBytes = ms.ToArray();
                return File(pdfBytes, "application/pdf", "staff_info.pdf");
            }
        }
    }
}
