using CannDash.API.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CannDash.API.Repository
{
    public class DriverRepository
    {
        private readonly CannDashDataContext _dataContext;

        public DriverRepository(CannDashDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public int GetNumberOfDriversWorkingForDispensary(int dispensaryId)
        {
            var dispensary = _dataContext.Dispensaries.Find(dispensaryId);

            return ((from driver in dispensary.Drivers
                    where driver.DispensaryId == dispensaryId && driver.DriverCheckIn == true
                    select driver.DriverId).Count());
        }

        public int GetNumberOfDriversOffForDispensary(int dispensaryId)
        {
            var dispensary = _dataContext.Dispensaries.Find(dispensaryId);

            return ((from driver in dispensary.Drivers
                    where driver.DispensaryId == dispensaryId && driver.DriverCheckIn == false
                    select driver.DriverId).Count());
        }
    }
}