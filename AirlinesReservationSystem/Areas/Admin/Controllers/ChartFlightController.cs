using AirlinesReservationSystem.Helper;
using AirlinesReservationSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AirlinesReservationSystem.Areas.Admin.Controllers
{
    //[AuthorizeRole("Admin")]
    public class ChartFlightController : Controller
    {
        private readonly Model1 db = new Model1();
        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (AuthHelper.getIdentityEmployeee() == null)
            {
                filterContext.Result = new RedirectToRouteResult(
                    new System.Web.Routing.RouteValueDictionary(new { Controller = "Security", Action = "Login" }));
            }
            base.OnActionExecuted(filterContext);
        }
        // GET: Admin/ChartFlight
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetTickeBuyedByYear(String year)
        {
            List<int> Listyears = new List<int>();
            int[] listArr = new int[12];

            int yearOP = int.Parse(year);

            var months = Enumerable.Range(1, 12); // Danh sách tháng từ 1 đến 12
            var ticketData = db.FlightSchedules
            .Where(t => t.departures_at.Year == yearOP) // Lọc theo năm 2024
            .GroupBy(t => t.departures_at.Month) // Nhóm theo tháng
            .Select(g => new
            {
                Month = g.Key, // Tháng
                TotalTickets = g.Sum(t => t.bookedSeats) // Tổng số vé
            })
            .OrderBy(r => r.Month) // Sắp xếp theo tháng
            .ToList();
            var result = months.Select(m => new
            {
                Month = m,
                TotalTickets = ticketData.FirstOrDefault(t => t.Month == m)?.TotalTickets ?? 0 // Nếu không có dữ liệu thì trả về 0
            }).ToList();


            for(int i = 0; i < 12; i++)
            {
                listArr[i] = result[i].TotalTickets;
            }


            //for (int i = 0; i< 12; i++)//12 tháng , lấy dữ liệu vé được mua trong mỗi tháng của tháng 12 
            //{
            //    if(result[0].Month == null)
            //    {

            //    }
            //}
            return Json(listArr, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult TotalSalaryByYear(String years)
        {
            // var months = Enumerable.Range(1, 12); // Danh sách tháng từ 1 đến 12
            // int yearOP = int.Parse(years);
            // var ticketData = db.FlightSchedules
            //.Where(t => t.departures_at.Year == yearOP) // Lọc theo năm 2024
            //.GroupBy(t => t.id) // Nhóm theo tháng
            //.Select(g => new
            //{
            //     Month = g.Key, // Tháng
            //     TotalSalary = g.Sum(t => t.bookedSeats) // Tổng số vé
            // })
            //.OrderBy(r => r.Month) // Sắp xếp theo tháng
            //.ToList();


            // var result = months.Select(m => new
            // {
            //     Month = m,
            //     TotalSalary = ticketData.FirstOrDefault(t => t.Month == m)?.TotalSalary ?? 0 // Nếu không có dữ liệu thì trả về 0
            // }).ToList();
            int yearOP = int.Parse(years);
            var revenueByMonth = db.FlightSchedules
            .Where(t => t.departures_at.Year == yearOP) // Lọc theo năm 2024
            .GroupBy(f => new { Year = f.departures_at.Year, Month = f.departures_at.Month, FlightId = f.id })
            .Select(g => new
            {
                Year = g.Key.Year,
                Month = g.Key.Month,
                FlightId = g.Key.FlightId,
                TotalRevenue = g.Sum(f => f.cost * f.bookedSeats)
            })
            .OrderBy(r => r.Year)
            .ThenBy(r => r.Month)
            .ToList();

            var months = Enumerable.Range(1, 12); // Danh sách tháng từ 1 đến 12

            var result = months.Select(m => new
            {
                Month = m,
                TotalSalary = revenueByMonth.FirstOrDefault(t => t.Month == m)?.TotalRevenue ?? 0 // Nếu không có dữ liệu thì trả về 0
            }).ToList();

            var results = new int[12];
            for (int month = 1; month <= 12; month++)
            {
                // Lấy tổng doanh thu cho tháng hiện tại
                results[month - 1] = revenueByMonth
                    .Where(item => item.Month == month) // Lọc theo tháng
                    .Sum(item => item.TotalRevenue);   // Tính tổng doanh thu
            }


            return Json(results, JsonRequestBehavior.AllowGet);

        }

    }
}