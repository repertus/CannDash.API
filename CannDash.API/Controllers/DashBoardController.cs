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
        private CustomerRepository customerRepository;
        private DriverRepository driverRepository;
        private OrderRepository orderRepository;

        public DashBoardController()
        {
            db = new CannDashDataContext();
            customerRepository = new CustomerRepository(db);
            driverRepository = new DriverRepository(db);
            orderRepository = new OrderRepository(db);
        }

        // GET: api/Dashboard/5
        //Todo: authorize role for only admin
        [ResponseType(typeof(Dispensary))]
        public IHttpActionResult GetDispensary(int id)
        {
            DateTime now = DateTime.Now;
            var startDate = new DateTime(now.Year, now.Month, 1);
            var endDate = startDate.AddMonths(1).AddDays(-1);
            var lastMonthStartDate = startDate.AddMonths(-1);
            var lastMonthEndDate = lastMonthStartDate.AddMonths(1).AddDays(-1);
            var lastTenDays = DateTime.Today.AddDays(-14);
            var lastThirtyDays = DateTime.Today.AddDays(-30);

            Dispensary dispensary = db.Dispensaries.Find(id);


            var selectedNumberDates = dispensary.Orders
                            .Where(s => s.DispensaryId == id && s.OrderDelivered == true && (s.OrderDate >= startDate && s.OrderDate <= now))
                            .GroupBy(s => s.OrderDate.Value.Date)
                            .Select(t => new
                            {
                                X = t.Key.Date,
                                Y = t.Sum(s => s.TotalCost),
                            }).ToList();


            var emptyData = Enumerable.Range(0, (int)Math.Round((now-startDate).TotalDays))
                .Select(i => new
                {
                    X = startDate.AddDays(i),
                    Y = 0
                });

            if (dispensary == null)
            {
                return NotFound();
            }

            return Ok(new
            {
                dispensary.DispensaryId,
                dispensary.CompanyName,
                TotalCustomers = dispensary.Customers.Count(),
                TotalExpirations = customerRepository.GetNumberOfExpiredCustomersForDispensary(id, startDate, endDate),
                DriversWorking = driverRepository.GetNumberOfDriversWorkingForDispensary(id),
                DriversOff = driverRepository.GetNumberOfDriversOffForDispensary(id),
                OrdersDelivered = orderRepository.GetNumberOfOrdersDeliveredForDispensary(id),
                OrdersPendingDelivery = orderRepository.GetNumberOfPendingDeliveryOrdersForDispensary(id),
                CurrentDaySales = orderRepository.GetCurrentDaySalesForDispensary(id),
                CurrentMonthSales = orderRepository.GetCurrentMonthSalesForDispensary(id, startDate, endDate),
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
                    .OrderBy(t => t.salesDay),
              StackBarGraph = new
              {
                    Title = "Sales Summary",
                    Ranges = new
                    {
                        TM = "This Month",
                        LM = "Last Month",
                        W2 = "2 Weeks Ago"

                    },
                    MainChart = new
                    {
                        Key = "Daily Sales",
                        Values = new
                        {
                            TM = selectedNumberDates.Union(emptyData
                                .Where(e => !selectedNumberDates.Select(x => x.X).Contains(e.X)))
                                .Select(t => new
                                {
                                    X = t.X.ToString("dd"),
                                    Y = t.Y
                                })
                                .OrderBy(t => t.X),

                            LM = dispensary.Orders
                                .Where(s => s.DispensaryId == id && s.OrderDelivered == true && (s.OrderDate >= lastMonthStartDate && s.OrderDate <= lastMonthEndDate))
                                .GroupBy(s => s.OrderDate.Value.Date)
                                .Select(t => new
                                {
                                    X = t.Key.ToString("dd"),
                                    Y = t.Sum(s => s.TotalCost),
                                })
                               .OrderBy(t => t.X),

                            W2 = dispensary.Orders
                                .Where(s => s.DispensaryId == id && s.OrderDelivered == true && (s.OrderDate >= lastTenDays && s.OrderDate <= DateTime.Today))
                                .GroupBy(s => s.OrderDate.Value.Date)
                                .Select(t => new
                                {
                                    X = t.Key.ToString("dd"),
                                    Y = t.Sum(s => s.TotalCost),
                                })
                               .OrderBy(t => t.X),

                        }
                    }
              }
            });
        }
    }
}
