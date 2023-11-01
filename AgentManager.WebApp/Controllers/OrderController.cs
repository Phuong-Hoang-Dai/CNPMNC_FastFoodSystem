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
using Org.BouncyCastle.Asn1.X509;
using System.Runtime.InteropServices;


namespace AgentManager.WebApp.Controllers
{
    public class OrderController : Controller
    {
        private readonly FastFoodSystemDbContext _context;
        public int SelectedCategoryId { get; set; }
        DBHelper dBHelper;
        public OrderController(FastFoodSystemDbContext context)
        {
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

        public IActionResult AddToCart(string id)
        {
            dBHelper = new DBHelper(_context);
            SanPhamVM sanPhamVM = new SanPhamVM()
            {
                maSanPham = id,
                tenSanPham = dBHelper.GetProductByID(id).Name,
                anh = dBHelper.GetProductByID(id).Image,
                gia = dBHelper.GetProductByID(id).Price,
                loaiSanPham = dBHelper.GetProductByID(id).FFSProductCategoryId,
                mota = dBHelper.GetProductByID(id).Desc
            };
            if (sanPhamVM == null) return NotFound();
            else return View(sanPhamVM);
        }
        [HttpPost]
        public IActionResult AddToCart(string productId, int quantity)
        {
            // Tìm sản phẩm trong cơ sở dữ liệu dựa trên productId
            FFSProduct product = _context.FFSProducts.FirstOrDefault(p => p.FFSProductId == productId);

            if (product != null)
            {
                // Tạo một bản ghi FFSProductOrder và thêm sản phẩm vào giỏ hàng
                FFSProductOrder productOrder = new FFSProductOrder
                {
                    FFSProduct = product,
                    FFSProductId = productId,
                    Quantity = quantity
                    // Không cần thiết lập giá trị cho FFSOrderId vì đây là trường tự tăng
                };

                _context.FFSProductOrders.Add(productOrder);
                _context.SaveChanges();
            }
            // ...
            return View("Index");
        }


        public ActionResult Cart(string ffsProductId, int quantity)
        {
            
            return View();
        }

        public ActionResult Bill()
        {
            var cashReceipt = new CashReceiptModel
            {
                CustomerName = "Phan Hoang Viet",
                
            };
            return View(cashReceipt);
        }

        public FileResult GeneratePDF()
        {
            var cashReceipt = new CashReceiptModel
            {
                CustomerName = "Phan Hoang Viet",
            };

            using (MemoryStream stream = new MemoryStream())
            {
                Document document = new Document();
                
            }
            return null;
        }

        // GET: OrderController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: OrderController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: OrderController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: OrderController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: OrderController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: OrderController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: OrderController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
