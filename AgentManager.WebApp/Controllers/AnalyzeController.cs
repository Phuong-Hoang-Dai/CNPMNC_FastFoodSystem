using FastFoodSystem.WebApp.Models;
using FastFoodSystem.WebApp.Models.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NuGet.Protocol;

namespace FastFoodSystem.WebApp.Controllers
{
    public class AnalyzeController : Controller
    {
        private readonly FastFoodSystemDbContext _context;

        List<FFSOrder> _orders = new List<FFSOrder> { };
        public AnalyzeController(FastFoodSystemDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            _orders = _context.FFSOrders.ToList();

            // Lọc và đếm số lượng đơn cho mỗi tháng
            var orderCountsByMonth = new Dictionary<string, int>();
            var orderCountsByDay = new Dictionary<string, int>();
            var orderCountsByWeek = new Dictionary<string, int>();
            foreach (var order in _orders)
            {
                var key = order.Date.ToString("MM/dd/yyyy"); // Lọc theo ngày tháng năm
                if (orderCountsByDay.ContainsKey(key))
                {
                    orderCountsByDay[key]++;
                }
                else
                {
                    orderCountsByDay[key] = 1;
                }
                if (orderCountsByMonth.ContainsKey(key))
                {
                    orderCountsByMonth[key]++;
                }
                else
                {
                    orderCountsByMonth[key] = 1;
                }
            }

            ViewBag.OrderCountsByMonth = orderCountsByMonth;
            ViewBag.OrderCountsByDay = orderCountsByDay;
            Console.WriteLine(orderCountsByDay.ToJson());
            return View();
        }
    }
}
