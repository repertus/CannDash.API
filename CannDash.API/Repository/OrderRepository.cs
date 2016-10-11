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
                    where order.DispensaryId == dispensaryId && order.OrderDelivered == true && order.OrderDate == DateTime.Today
                    select order.OrderId).Count());
        }

        public int GetNumberOfPendingDeliveryOrdersForDispensary(int dispensaryId)
        {
            var dispensary = _dataContext.Dispensaries.Find(dispensaryId);

            return ((from order in dispensary.Orders
                     where order.DispensaryId == dispensaryId && order.OrderDelivered == false && order.OrderDate == DateTime.Today
                     select order.OrderId).Count());
        }

        public int GetCurrentDaySalesForDispensary(int dispensaryId)
        {
            var dispensary = _dataContext.Dispensaries.Find(dispensaryId);

            return ((from sales in dispensary.Orders
                     where sales.DispensaryId == dispensaryId && sales.OrderDelivered == true && sales.OrderDate == DateTime.Today
                     select sales.TotalCost).Sum());
        }

        public int GetCurrentMonthSalesForDispensary(int dispensaryId)
        {
            var dispensary = _dataContext.Dispensaries.Find(dispensaryId);

            return ((from sales in dispensary.Orders
                     where sales.DispensaryId == dispensaryId && sales.OrderDelivered == true && (sales.OrderDate >= _dateRepository.startDate() && sales.OrderDate <= _dateRepository.endDate())
                     select sales.TotalCost).Sum());
        }
    }
}