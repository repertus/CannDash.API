using CannDash.API.Infrastructure;
using CannDash.API.Models;
using CannDash.API.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace CannDash.API.Controllers
{
    public class DashBoardController : ApiController
    {
        private CannDashDataContext db;
        private DriverRepository driverRepository;

        public DashBoardController()
        {
            db = new CannDashDataContext();
            driverRepository = new DriverRepository(db);
        }

        // GET: api/Dashboard/5
        //Todo: authorize role for only admin
        [ResponseType(typeof(Dispensary))]
        public IHttpActionResult GetDispensary(int id)
        {
            DateTime now = DateTime.Now;
            var startDate = new DateTime(now.Year, now.Month, 1);
            var endDate = startDate.AddMonths(1).AddDays(-1);
            var lastThirtyDays = DateTime.Today.AddDays(-30);

            Dispensary dispensary = db.Dispensaries.Find(id);
            if (dispensary == null)
            {
                return NotFound();
            }

            return Ok(new
            {
                dispensary.DispensaryId,
                dispensary.CompanyName,
                TotalCustomers = dispensary.Customers.Count(),
                TotalExpirations = (from customer in dispensary.Customers
                                   where customer.DispensaryId == id && (customer.MmicExpiration >= startDate && customer.MmicExpiration <= endDate)
                                    select customer.CustomerId).Count(),
                DriversWorking = driverRepository.GetNumberOfDriversWorkingForDispensary(id),
                DriversOff = (from driver in dispensary.Drivers
                              where driver.DispensaryId == id && driver.DriverCheckIn == false
                              select driver.DriverId).Count(),
                OrdersDelivered = (from order in dispensary.Orders
                                   where order.DispensaryId == id && order.OrderDelivered == true && order.OrderDate == DateTime.Today
                                   select order.OrderId).Count(),
                OrdersPendingDelivery = (from order in dispensary.Orders
                                         where order.DispensaryId == id && order.OrderDelivered == false && order.OrderDate == DateTime.Today
                                         select order.OrderId).Count(),
                CurrentDaySales = (from sales in dispensary.Orders
                                   where sales.DispensaryId == id && sales.OrderDelivered == true && sales.OrderDate == DateTime.Today
                                   select sales.TotalCost).Sum(),
                CurrentMonthSales = (from sales in dispensary.Orders
                                     where sales.DispensaryId == id && sales.OrderDelivered == true && ( sales.OrderDate >= startDate && sales.OrderDate <= endDate)
                                     select sales.TotalCost).Sum(),
                LastThirtyDaySales =
                dispensary.Orders
                    .Where(s => s.DispensaryId == id && s.OrderDelivered == true && (s.OrderDate >= lastThirtyDays && s.OrderDate <= DateTime.Today))
                    .GroupBy(s => s.OrderDate.Value.Date)
                    .Select(t => new
                    {
                        salesDay = t.Key,
                        dailySales = t.Sum(s => s.TotalCost),
                        dailyOrders = t.Count()
                    })
                    .OrderBy(t => t.salesDay) 
            });
        }
    }
}
