using CannDash.API.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CannDash.API.Repository
{
    public class CustomerRepository
    {

        private readonly CannDashDataContext _dataContext;

        public CustomerRepository(CannDashDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public int GetNumberOfExpiredCustomersForDispensary(int dispensaryId)
        {
            var dispensary = _dataContext.Dispensaries.Find(dispensaryId);
            DateTime now = DateTime.Now;
            var startDate = new DateTime(now.Year, now.Month, 1);
            var endDate = startDate.AddMonths(1).AddDays(-1);

            return ((from customer in dispensary.Customers
                     where customer.DispensaryId == dispensaryId && (customer.MmicExpiration >= startDate && customer.MmicExpiration <= endDate)
                     select customer.CustomerId).Count());
        }
    }
}