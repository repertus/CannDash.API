using CannDash.API.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CannDash.API.Repository
{
    public class OrderRepository
    {
        private readonly CannDashDataContext _dataContext;
        private readonly DateRepository _dateRepository;

        public OrderRepository(CannDashDataContext db)
        {
            _dataContext = db;
            _dateRepository = new DateRepository();
        }

        public int GetNumberOfOrdersDeliveredForDispensary(int dispensaryId)
        {
            var dispensary = _dataContext.Dispensaries.Find(dispensaryId);

            return ((from order in dispensary.Orders
                    where order.DispensaryId == dispensaryId && order.OrderStatus == 3 && (order.OrderDate >= DateTime.Today && order.OrderDate < DateTime.Today.AddDays(1))
                     select order.OrderId).Count());
        }

        public int GetNumberOfPendingDeliveryOrdersForDispensary(int dispensaryId)
        {
            var dispensary = _dataContext.Dispensaries.Find(dispensaryId);

            return ((from order in dispensary.Orders
                     where order.DispensaryId == dispensaryId && order.OrderStatus == 1 && (order.OrderDate >= DateTime.Today && order.OrderDate < DateTime.Today.AddDays(1))
                     select order.OrderId).Count());
        }

        public int GetCurrentDaySalesForDispensary(int dispensaryId)
        {
            var dispensary = _dataContext.Dispensaries.Find(dispensaryId);

            return ((from sales in dispensary.Orders
                     where sales.DispensaryId == dispensaryId && sales.OrderStatus == 3 && (sales.OrderDate >= DateTime.Today && sales.OrderDate < DateTime.Today.AddDays(1))
                     select sales.TotalOrderSale).Sum());
        }

        public int GetCurrentMonthSalesForDispensary(int dispensaryId)
        {
            var dispensary = _dataContext.Dispensaries.Find(dispensaryId);

            return ((from sales in dispensary.Orders
                     where sales.DispensaryId == dispensaryId && sales.OrderStatus == 3 && (sales.OrderDate >= _dateRepository.firstDayOfMonth() && sales.OrderDate <= _dateRepository.todaysDate())
                     select sales.TotalOrderSale).Sum());
        }
    }
}