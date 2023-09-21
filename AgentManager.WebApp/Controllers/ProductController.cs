using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AgentManager.WebApp.Models;
using AgentManager.WebApp.Models.Data;
using AgentManager.WebApp.Models.ViewModel;

namespace AgentManager.WebApp.Controllers
{
    public class ProductController : Controller
    {
        DBHelper dBHelper;
        public ProductController(AgentManagerDbContext db)
        {
            dBHelper = new DBHelper(db);
        }

        public ActionResult Index()
        {
            ViewData["listProduct"] = dBHelper.GetProducts();
            return View();
        }

        public IActionResult Detail(int id)
        {
            ViewBag.ProductDetail = dBHelper.GetProductByID(id);
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(SanPhamVM sanPhamVM)
        {
            if (ModelState.IsValid)
            {
                Product sanPham = new Product()
                {
                    ProductName = sanPhamVM.tenSanPham,
                    Image = sanPhamVM.anh,
                    ProductWeight = (int)sanPhamVM.khoiLuong,
                    InventoryQuantity = sanPhamVM.soLuongTonKho,
                    ItemUnit = sanPhamVM.donViTinh,
                    Price = sanPhamVM.gia,
                    ProductCategoryId = 1
                };
                dBHelper.InsertProduct(sanPham);
                return RedirectToAction("index");
            }
            return View(sanPhamVM);
        }

        public IActionResult Delete(int id)
        {
            SanPhamVM sanPhamVM = new SanPhamVM()
            {
                maSanPham = id,
                tenSanPham = dBHelper.GetProductByID(id).ProductName,
                anh = dBHelper.GetProductByID(id).Image,
                khoiLuong = dBHelper.GetProductByID(id).ProductWeight,
                soLuongTonKho = dBHelper.GetProductByID(id).InventoryQuantity,
                donViTinh = dBHelper.GetProductByID(id).ItemUnit,
                gia = dBHelper.GetProductByID(id).Price
            };
            if (sanPhamVM == null)
                return NotFound();
            else return View(sanPhamVM);
        }
        [HttpPost]
        public IActionResult Delete(SanPhamVM sanPhamVM)
        {
            if (ModelState.IsValid)
            {
                dBHelper.DeleteProduct(sanPhamVM.maSanPham);
                return RedirectToAction("index");
            }
            return View(sanPhamVM);
        }

        public IActionResult Edit(int id)
        {
            SanPhamVM sanPhamVM = new SanPhamVM()
            {
                maSanPham = id,
                tenSanPham = dBHelper.GetProductByID(id).ProductName,
                anh = dBHelper.GetProductByID(id).Image,
                khoiLuong = dBHelper.GetProductByID(id).ProductWeight,
                soLuongTonKho = dBHelper.GetProductByID(id).InventoryQuantity,
                donViTinh = dBHelper.GetProductByID(id).ItemUnit,
                gia = dBHelper.GetProductByID(id).Price
            };
            if (sanPhamVM == null) return NotFound();
            else return View(sanPhamVM);
        }
        [HttpPost]
        public IActionResult Edit(SanPhamVM sanPhamVM)
        {
            if (ModelState.IsValid)
            {
                Product sanPham = new Product()
                {
                    ProductId = sanPhamVM.maSanPham,
                    ProductName = sanPhamVM.tenSanPham,
                    Image = sanPhamVM.anh,
                    ProductWeight = (int)sanPhamVM.khoiLuong,
                    InventoryQuantity = sanPhamVM.soLuongTonKho,
                    ItemUnit = sanPhamVM.donViTinh,
                    Price = sanPhamVM.gia,
                    ProductCategoryId = 1
                };
                dBHelper.EditProduct(sanPham);
                return RedirectToAction("Index");
            }
            return View(sanPhamVM);
        }
    }
}
