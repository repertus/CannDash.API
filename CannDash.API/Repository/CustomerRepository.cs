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

        public int GetNumberOfExpiredCustomersForDispensary(int dispensaryId, DateTime startDate, DateTime endDate)
        {
            var dispensary = _dataContext.Dispensaries.Find(dispensaryId);

            return ((from customer in dispensary.Customers
                     where customer.DispensaryId == dispensaryId && (customer.MmicExpiration >= startDate && customer.MmicExpiration <= endDate)
                     select customer.CustomerId).Count());
        }
    }
}