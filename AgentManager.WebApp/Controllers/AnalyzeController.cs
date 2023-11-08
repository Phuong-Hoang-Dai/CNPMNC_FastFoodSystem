using FastFoodSystem.WebApp.Models;
using FastFoodSystem.WebApp.Models.Data;
using FastFoodSystem.WebApp.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Newtonsoft.Json;
using NuGet.Protocol;
using System.Linq;

namespace FastFoodSystem.WebApp.Controllers
{
    public class AnalyzeController : Controller
    {
        private readonly FastFoodSystemDbContext _context;
        private DBHelper _dbHelper;
        List<FFSOrder> _orders = new List<FFSOrder> { };
        List<FFSProductOrder> fFSProductOrders = new List<FFSProductOrder> { };
        
        public AnalyzeController(FastFoodSystemDbContext context)
        {
            _context = context;

        }
        public IActionResult Index()
        {
            //Count order
            _orders = _context.FFSOrders.ToList();

            var orderCountsByMonth = new Dictionary<string, int>();
            var orderCountsByDay = new Dictionary<string, int>();
            var revenueByMonth = new Dictionary<string, double>();
            double total = 0;
            foreach (var order in _orders)
            {
                var day = order.Date.ToString("MM/dd/yyyy"); // Lọc theo ngày tháng năm
                var month = order.Date.ToString("MM/yyyy");
                double cash = order.Cash;
                if (orderCountsByDay.ContainsKey(day))
                {
                    orderCountsByDay[day]++;
                }
                else
                {
                    orderCountsByDay[day] = 1;
                }
                if (orderCountsByMonth.ContainsKey(month))
                {
                    orderCountsByMonth[month]++;
                    if (revenueByMonth.ContainsKey(month))
                    {
                        revenueByMonth[month] += cash; // Cộng thêm tiền từ đơn hàng vào tổng doanh thu của tháng
                    }
                    else
                    {
                        revenueByMonth[month] = cash; // Khởi tạo tổng doanh thu của tháng nếu chưa tồn tại
                    }
                }
                else
                {
                    orderCountsByMonth[month] = 1;
                    revenueByMonth[month] = cash;
                }
            }
            
            Console.WriteLine(_orders.ToJson());
            fFSProductOrders = _context.FFSProductOrders.ToList();
            var productIds = fFSProductOrders.Select(p => p.FFSProductId).ToList();
            var product = _context.FFSProducts.ToList();
            Console.WriteLine(productIds.ToJson());
            
            ViewBag.OrderCountsByMonth = orderCountsByMonth;
            ViewBag.OrderCountsByDay = orderCountsByDay;
            ViewBag.RevenueByMonth = revenueByMonth;
            

            //var model = new ProductCategoryViewModel();

            //model.Categories = _context.FFSProductCategories.ToList();
            //model.Products = _context.FFSProducts.ToList();
            //Console.WriteLine(model.Categories.ToJson());
            //foreach (var prodt in model.Products)
            //{
            //    Console.WriteLine(prodt.FFSProductId + " : " + prodt.FFSProductCategoryId);
            //}

            // Đếm số lượng sản phẩm của từng danh mục dựa trên productIds
            var categoryCounts = productIds
            .GroupBy(id => _context.FFSProducts.FirstOrDefault(p => p.FFSProductId == id)?.FFSProductCategoryId)
            .Where(group => group.Key != null)
            .Select(group => new { CategoryId = group.Key, Count = group.Count() })
            .ToList();


            var categoryNames = _context.FFSProductCategories.ToDictionary(cat => cat.FFSProductCategoryId, cat => cat.Name);
            var result = new Dictionary<string, int>();

            foreach (var categoryCount in categoryCounts)
            {
                if (categoryNames.ContainsKey(categoryCount.CategoryId))
                {
                    var categoryName = categoryNames[categoryCount.CategoryId];
                    result[categoryName] = categoryCount.Count;
                }
            }

            // Chuyển đổi Dictionary thành JSON và trả về
            var rates = JsonConvert.SerializeObject(result);
            ViewBag.CategoriesRates = rates;
            return View();
        }
    }
}
