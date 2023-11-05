using FastFoodSystem.WebApp.Models.Data;
using FastFoodSystem.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using FastFoodSystem.WebApp.Models.ViewModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace FastFoodSystem.WebApp.Controllers
{
    [Authorize (Roles = "Admin,Manager")]
    public class PositionController : Controller
    {
        DBHelper dBHelper;
        public PositionController(FastFoodSystemDbContext db)
        {
            dBHelper = new DBHelper(db);
        }

        public IActionResult Index()
        {
            ViewData["listPositions"] = dBHelper.GetPositions();
            return View();
        }
        public IActionResult Create()
        {
            return View();
        }
        
        [HttpPost]
        public IActionResult Create(PositionVM positionVM)
        {
            if (ModelState.IsValid)
            {
                Position position = new Position()
                {
                    PositionName = positionVM.tenChucVu,
                    
                };
                dBHelper.InsertPositions(position);
                return RedirectToAction("index");
            }
            return View(positionVM);
        }
        
        public IActionResult Delete(int id)
        {
            PositionVM positionVM = new PositionVM()
            {
                maChucVu = id,
                tenChucVu = dBHelper.GetPositionByID(id).PositionName,
            };
            if (positionVM == null)
                return NotFound();
            else return View(positionVM);
        }
        [HttpPost]
        public IActionResult Delete(PositionVM positionVM)
        {
            
            if (ModelState.IsValid)
            {
                if (dBHelper.GetStaffByIdPosition(positionVM.maChucVu) == null)
                {
                    dBHelper.DeletePositions(positionVM.maChucVu);
                    return RedirectToAction("index");
                }
                else Console.WriteLine("ERROR");

            }
            else Console.WriteLine("ERROR");
            return View(positionVM);
        }
        
        public IActionResult Edit(int id)
        {
            PositionVM positionVM = new PositionVM()
            {
                maChucVu = dBHelper.GetPositionByID(id).PositionId,
                tenChucVu = dBHelper.GetPositionByID(id).PositionName,
            };
            Console.WriteLine("Post Edit Positon Clone:", positionVM);
            if (positionVM == null) return NotFound();
            else return View(positionVM);
        }
        [HttpPost]
        public IActionResult Edit(PositionVM positionVM)
        {
            if (ModelState.IsValid)
            {
                // Truy xuất đối tượng Position cần chỉnh sửa từ cơ sở dữ liệu
                Position position = dBHelper.GetPositionByID(positionVM.maChucVu);

                if (position != null)
                {
                    // Cập nhật thông tin mới từ PositionVM
                    position.PositionName = positionVM.tenChucVu;

                    // Cập nhật thông tin vào cơ sở dữ liệu
                    dBHelper.EditPositions(position);

                    return RedirectToAction("index");
                }
                else
                {
                    return NotFound();
                }
            }
            return View(positionVM);
        }
    }
}
