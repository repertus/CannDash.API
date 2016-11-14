using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CannDash.API.Repository
{
    public class DateRepository
    {
        public DateTime todaysDate()
        {
            return DateTime.Now;
        }

        public DateTime lastTwoWeeksStartDate()
        {
            return startDate().AddDays(-14);
        }

        public DateTime startDate()
        {
            var dayOfWeek = ((int)todaysDate().DayOfWeek);
            var firstDayOfWeek = new DateTime();

            switch (dayOfWeek)
            {
                case 1:
                    firstDayOfWeek = todaysDate();
                    break;
                case 2:
                    firstDayOfWeek = todaysDate().AddDays(-1);
                    break;
                case 3:
                    firstDayOfWeek = todaysDate().AddDays(-2);
                    break;
                case 4:
                    firstDayOfWeek = todaysDate().AddDays(-3);
                    break;
                case 5:
                    firstDayOfWeek = todaysDate().AddDays(-4);
                    break;
                case 6:
                    firstDayOfWeek = todaysDate().AddDays(-5);
                    break;
                case 0:
                    firstDayOfWeek = todaysDate().AddDays(-6);
                    break;
            }

            return (firstDayOfWeek);
        }

        public DateTime endDate()
        {
            return startDate().AddDays(7);
        }

        public DateTime lastWeekStartDate()
        {
            return startDate().AddDays(-7);
        }

        public DateTime lastWeekEndDate()
        {
            return startDate();
        }

        public DateTime firstDayOfMonth()
        {
            return new DateTime(todaysDate().Year, todaysDate().Month, 1);
        }
    }
}