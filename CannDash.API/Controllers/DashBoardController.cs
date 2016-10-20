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
        private GraphDataRepository graphDataRepository;

        public DashBoardController()
        {
            db = new CannDashDataContext();
            customerRepository = new CustomerRepository(db);
            driverRepository = new DriverRepository(db);
            orderRepository = new OrderRepository(db);
            graphDataRepository = new GraphDataRepository(db);
        }

        // GET: api/Dashboard/5
        //Todo: authorize role for only admin
        [ResponseType(typeof(Dispensary))]
        public IHttpActionResult GetDispensary(int id)
        {
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
                TotalExpirations = customerRepository.GetNumberOfExpiredCustomersForDispensary(id),
                DriversWorking = driverRepository.GetNumberOfDriversWorkingForDispensary(id),
                DriversOff = driverRepository.GetNumberOfDriversOffForDispensary(id),
                OrdersDelivered = orderRepository.GetNumberOfOrdersDeliveredForDispensary(id),
                OrdersPendingDelivery = orderRepository.GetNumberOfPendingDeliveryOrdersForDispensary(id),
                CurrentDaySales = orderRepository.GetCurrentDaySalesForDispensary(id),
                CurrentMonthSales = orderRepository.GetCurrentMonthSalesForDispensary(id),

                StackBarGraph = new
                {
                    Title = "Sales Summary",
                    Ranges = new
                    {
                        TW = "This Week",
                        LW = "Last Week",
                        W2 = "2 Weeks Ago"

                    },
                    MainChart = new object[] {
                       new {
                            Key = "Daily Sales",
                            Values = new
                            {
                                TW = graphDataRepository.GetChartDataForThisWeekSalesForDispensary(id),
                                LW = graphDataRepository.GetChartDataForLastWeekSalesForDispensary(id),
                                W2 = graphDataRepository.GetChartDataForLastTwoWeeksSalesForDispensary(id)

                            }
                        },
                        new {
                            Key = "Daily Orders",
                            Values = new
                            {
                                TW = graphDataRepository.GetChartDataForThisWeekOrdersForDispensary(id),
                                LW = graphDataRepository.GetChartDataForLastWeekOrdersForDispensary(id),
                                W2 = graphDataRepository.GetChartDataForLastTwoWeeksOrdersForDispensary(id)
                            }
                        }
                }
                }
            });
        }
    }
}
