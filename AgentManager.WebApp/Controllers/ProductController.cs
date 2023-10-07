﻿using Microsoft.AspNetCore.Http;
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
