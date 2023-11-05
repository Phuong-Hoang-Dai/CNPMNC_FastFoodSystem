using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FastFoodSystem.WebApp.Models;
using FastFoodSystem.WebApp.Models.Data;
using FastFoodSystem.WebApp.Models.ViewModel;
using FastFoodSystem.WebApp.Controllers.ExcelImport;
using OfficeOpenXml;

namespace FastFoodSystem.WebApp.Controllers
{
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

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(SanPhamVM sanPhamVM, IFormFile excelFile)
        {
            if (excelFile != null && excelFile.Length > 0)
            {
                try
                {
                    using (var stream = excelFile.OpenReadStream())
                    using (var package = new ExcelPackage(stream))
                    {
                        var importedProducts = ExcelReader.ImportProductsFromExcel(package);

                        if (importedProducts != null && importedProducts.Any())
                        {
                            // Thêm các sản phẩm đã import vào CSDL ở đây
                            foreach (var product in importedProducts)
                            {
                                dBHelper.InsertProduct(product);
                            }
                            return RedirectToAction("Index");
                        }
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("excelFile", "Lỗi khi import từ file Excel: " + ex.Message);
                }
            }

            else
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
                return RedirectToAction("Index");
            }

            return View(sanPhamVM);
        }

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
