using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FastFoodSystem.WebApp.Models;
using FastFoodSystem.WebApp.Models.Data;
using FastFoodSystem.WebApp.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;

namespace FastFoodSystem.WebApp.Controllers
{
    [Authorize(Roles = "Admin,Manager,Staff")]
    public class ProductController : Controller
    {
        DBHelper dBHelper;
        public ProductController(FastFoodSystemDbContext db)
        {
            dBHelper = new DBHelper(db);
        }

        public ActionResult Index()
        {
            ViewData["listProduct"] = dBHelper.GetProducts();
            return View();
        }

        public IActionResult Details(string id)
        {
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

        [Authorize(Roles = "Admin,Manager")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Manager")]
        public IActionResult Create(SanPhamVM sanPhamVM)
        {
            if (ModelState.IsValid)
            {
                FFSProduct sanPham = new FFSProduct()
                {
                    Name = sanPhamVM.tenSanPham,
                    Image = sanPhamVM.anh,
                    Price = sanPhamVM.gia,
                    Desc = sanPhamVM.mota,
                    FFSProductId = sanPhamVM.maSanPham,
                    FFSProductCategoryId = sanPhamVM.loaiSanPham
                };
                dBHelper.InsertProduct(sanPham);
                return RedirectToAction("index");
            }
            return View(sanPhamVM);
        }

        [Authorize(Roles = "Admin,Manager")]
        public IActionResult Delete(string id)
        {
            SanPhamVM sanPhamVM = new SanPhamVM()
            {
                maSanPham = id,
                tenSanPham = dBHelper.GetProductByID(id).Name,
                anh = dBHelper.GetProductByID(id).Image,
                gia = dBHelper.GetProductByID(id).Price
            };
            if (sanPhamVM == null)
                return NotFound();
            else return View(sanPhamVM);
        }
        [HttpPost]
        [Authorize(Roles = "Admin,Manager")]
        public IActionResult Delete(SanPhamVM sanPhamVM)
        {
            if (ModelState.IsValid)
            {
                dBHelper.DeleteProduct(sanPhamVM.maSanPham);
                return RedirectToAction("index");
            }
            else Console.WriteLine("ERROR");
            return View(sanPhamVM);
        }

        [Authorize(Roles = "Admin,Manager")]
        public IActionResult Edit(string id)
        {
            SanPhamVM sanPhamVM = new SanPhamVM()
            {
                maSanPham = dBHelper.GetProductByID(id).FFSProductId,
                tenSanPham = dBHelper.GetProductByID(id).Name,
                anh = dBHelper.GetProductByID(id).Image,
                gia = dBHelper.GetProductByID(id).Price,
                mota = dBHelper.GetProductByID(id).Desc,
                loaiSanPham = dBHelper.GetProductByID(id).FFSProductCategoryId,
            };
            Console.WriteLine("Post Edit Product Clone:", sanPhamVM);
            if (sanPhamVM == null) return NotFound();
            else return View(sanPhamVM);
        }
        [HttpPost]
        [Authorize(Roles = "Admin,Manager")]
        public IActionResult Edit(SanPhamVM sanPhamVM)
        {
            if (ModelState.IsValid)
            {
                FFSProduct sanPham = new FFSProduct()
                {
                    Name = sanPhamVM.tenSanPham,
                    Image = sanPhamVM.anh,
                    Price = sanPhamVM.gia,
                    Desc = sanPhamVM.mota,
                    FFSProductId = sanPhamVM.maSanPham,
                    FFSProductCategoryId = sanPhamVM.loaiSanPham
                };
                dBHelper.EditProduct(sanPham);
                return RedirectToAction("index");
            }
            return View(sanPhamVM);
        }
    }
}
