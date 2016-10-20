using CannDash.API.Infrastructure;
using CannDash.API.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CannDash.API.Repository
{
    public class GraphDataRepository
    {
        private readonly CannDashDataContext _dataContext;
        private readonly DateRepository dateRepository;

        public GraphDataRepository(CannDashDataContext db)
        {
            _dataContext = db;
            dateRepository = new DateRepository();
        }

        public IEnumerable<dynamic> GetChartDataForThisWeekSalesForDispensary(int dispensaryId)
        {
            var dispensary = _dataContext.Dispensaries.Find(dispensaryId);

            var selectedNumberDates = dispensary.Orders
                            .Where(s => s.DispensaryId == dispensaryId && s.OrderStatus == 3 && (s.OrderDate >= dateRepository.startDate() && s.OrderDate <= dateRepository.todaysDate()))
                            .GroupBy(s => s.OrderDate.Value.Date)
                            .Select(t => new
                            {
                                SalesDay = t.Key.Date,
                                Sales = t.Sum(s => s.TotalOrderSale),
                            }).ToList();


            var emptyData = Enumerable.Range(0, (int)Math.Round((dateRepository.startDate().AddDays(7) - dateRepository.startDate()).TotalDays))
                .Select(i => new
                {
                    SalesDay = dateRepository.startDate().AddDays(i),
                    Sales = (int) 0
                });

            var thisWeeksSales = selectedNumberDates.Union(emptyData
                                .Where(e => !selectedNumberDates.Select(s => s.SalesDay).Contains(e.SalesDay)))
                                .Select(t => new
                                {
                                    SalesDay = t.SalesDay,
                                    Sales = t.Sales
                                })
                                .OrderBy(t => t.SalesDay)
                                .GroupBy(t => t.SalesDay.Date)
                                .Select(r => new
                                {
                                    X = r.Key.Date.ToString("ddd"),
                                    Y = r.Sum(t => t.Sales)
                                });

            return thisWeeksSales;
        }

        public IEnumerable<dynamic> GetChartDataForLastWeekSalesForDispensary(int dispensaryId)
        {
            var dispensary = _dataContext.Dispensaries.Find(dispensaryId);

            var selectedNumberDates = dispensary.Orders
                            .Where(s => s.DispensaryId == dispensaryId && s.OrderStatus == 3 && (s.OrderDate >= dateRepository.lastWeekStartDate() && s.OrderDate <= dateRepository.lastWeekEndDate()))
                            .GroupBy(s => s.OrderDate.Value.Date)
                            .Select(t => new
                            {
                                SalesDay = t.Key.Date,
                                Sales = t.Sum(s => s.TotalOrderSale),
                            }).ToList();


            var emptyData = Enumerable.Range(0, (int)Math.Round((dateRepository.lastWeekEndDate() - dateRepository.lastWeekStartDate()).TotalDays))
                .Select(i => new
                {
                    SalesDay = dateRepository.lastWeekStartDate().AddDays(i),
                    Sales = 0
                });

            var lastWeekSales = selectedNumberDates.Union(emptyData
                                .Where(e => !selectedNumberDates.Select(s => s.SalesDay).Contains(e.SalesDay)))
                                .Select(t => new
                                {
                                    SalesDay = t.SalesDay,
                                    Sales = t.Sales
                                })
                                .OrderBy(t => t.SalesDay)
                                .GroupBy(t => t.SalesDay.Date)
                                .Select(r => new
                                {
                                    X = r.Key.Date.ToString("ddd"),
                                    Y = r.Sum(t => t.Sales)
                                });

            return lastWeekSales;
        }

        public IEnumerable<dynamic> GetChartDataForLastTwoWeeksSalesForDispensary(int dispensaryId)
        {
            var dispensary = _dataContext.Dispensaries.Find(dispensaryId);

            var selectedNumberDates = dispensary.Orders
                            .Where(s => s.DispensaryId == dispensaryId && s.OrderStatus == 3 && (s.OrderDate >= dateRepository.lastTwoWeeksStartDate() && s.OrderDate <= dateRepository.lastWeekStartDate()))
                            .GroupBy(s => s.OrderDate.Value.Date)
                            .Select(t => new
                            {
                                SalesDay = t.Key.Date,
                                Sales = t.Sum(s => s.TotalOrderSale),
                            }).ToList();


            var emptyData = Enumerable.Range(0, (int)Math.Round((dateRepository.lastWeekStartDate() - dateRepository.lastTwoWeeksStartDate()).TotalDays))
                .Select(i => new
                {
                    SalesDay = dateRepository.lastTwoWeeksStartDate().AddDays(i),
                    Sales = 0
                });

            var lastTwoWeeksSales = selectedNumberDates.Union(emptyData
                                .Where(e => !selectedNumberDates.Select(s => s.SalesDay).Contains(e.SalesDay)))
                                .Select(t => new
                                {
                                    SalesDay = t.SalesDay,
                                    Sales = t.Sales
                                })
                                .OrderBy(t => t.SalesDay)
                                .GroupBy(t => t.SalesDay.Date)
                                .Select(r => new
                                {
                                    X = r.Key.Date.ToString("ddd"),
                                    Y = r.Sum(t => t.Sales)
                                });

            return lastTwoWeeksSales;
        }

        public IEnumerable<dynamic> GetChartDataForThisWeekOrdersForDispensary(int dispensaryId)
        {
            var dispensary = _dataContext.Dispensaries.Find(dispensaryId);

            var selectedNumberDates = dispensary.Orders
                            .Where(s => s.DispensaryId == dispensaryId && s.OrderStatus == 3 && (s.OrderDate >= dateRepository.startDate() && s.OrderDate <= dateRepository.todaysDate()))
                            .GroupBy(s => s.OrderDate.Value.Date)
                            .Select(t => new
                            {
                                SalesDay = t.Key.Date,
                                Orders = t.Count()
                            }).ToList();


            var emptyData = Enumerable.Range(0, (int)Math.Round((dateRepository.startDate().AddDays(7) - dateRepository.startDate()).TotalDays))
                .Select(i => new
                {
                    SalesDay = dateRepository.startDate().AddDays(i),
                    Orders = (int)0
                });

            var thisWeeksSales = selectedNumberDates.Union(emptyData
                                .Where(e => !selectedNumberDates.Select(s => s.SalesDay).Contains(e.SalesDay)))
                                .Select(t => new
                                {
                                    SalesDay = t.SalesDay,
                                    Orders = t.Orders
                                })
                                .OrderBy(t => t.SalesDay)
                                .GroupBy(t => t.SalesDay.Date)
                                .Select(r => new
                                {
                                    X = r.Key.Date.ToString("ddd"),
                                    Y = r.Sum(t => t.Orders)
                                });

            return thisWeeksSales;
        }

        public IEnumerable<dynamic> GetChartDataForLastWeekOrdersForDispensary(int dispensaryId)
        {
            var dispensary = _dataContext.Dispensaries.Find(dispensaryId);

            var selectedNumberDates = dispensary.Orders
                            .Where(s => s.DispensaryId == dispensaryId && s.OrderStatus == 3 && (s.OrderDate >= dateRepository.lastWeekStartDate() && s.OrderDate <= dateRepository.lastWeekEndDate()))
                            .GroupBy(s => s.OrderDate.Value.Date)
                            .Select(t => new
                            {
                                SalesDay = t.Key.Date,
                                Orders = t.Count()
                            }).ToList();


            var emptyData = Enumerable.Range(0, (int)Math.Round((dateRepository.lastWeekEndDate() - dateRepository.lastWeekStartDate()).TotalDays))
                .Select(i => new
                {
                    SalesDay = dateRepository.lastWeekStartDate().AddDays(i),
                    Orders = 0
                });

            var lastWeekSales = selectedNumberDates.Union(emptyData
                                .Where(e => !selectedNumberDates.Select(s => s.SalesDay).Contains(e.SalesDay)))
                                .Select(t => new
                                {
                                    SalesDay = t.SalesDay,
                                    Orders = t.Orders
                                })
                                .OrderBy(t => t.SalesDay)
                                .GroupBy(t => t.SalesDay.Date)
                                .Select(r => new
                                {
                                    X = r.Key.Date.ToString("ddd"),
                                    Y = r.Sum(t => t.Orders)
                                });

            return lastWeekSales;
        }

        public IEnumerable<dynamic> GetChartDataForLastTwoWeeksOrdersForDispensary(int dispensaryId)
        {
            var dispensary = _dataContext.Dispensaries.Find(dispensaryId);

            var selectedNumberDates = dispensary.Orders
                            .Where(s => s.DispensaryId == dispensaryId && s.OrderStatus == 3 && (s.OrderDate >= dateRepository.lastTwoWeeksStartDate() && s.OrderDate <= dateRepository.lastWeekStartDate()))
                            .GroupBy(s => s.OrderDate.Value.Date)
                            .Select(t => new
                            {
                                SalesDay = t.Key.Date,
                                Orders = t.Count()
                            }).ToList();


            var emptyData = Enumerable.Range(0, (int)Math.Round((dateRepository.lastWeekStartDate() - dateRepository.lastTwoWeeksStartDate()).TotalDays))
                .Select(i => new
                {
                    SalesDay = dateRepository.lastTwoWeeksStartDate().AddDays(i),
                    Orders = 0
                });

            var lastTwoWeeksSales = selectedNumberDates.Union(emptyData
                                .Where(e => !selectedNumberDates.Select(s => s.SalesDay).Contains(e.SalesDay)))
                                .Select(t => new
                                {
                                    SalesDay = t.SalesDay,
                                    Orders = t.Orders
                                })
                                .OrderBy(t => t.SalesDay)
                                .GroupBy(t => t.SalesDay.Date)
                                .Select(r => new
                                {
                                    X = r.Key.Date.ToString("ddd"),
                                    Y = r.Sum(t => t.Orders)
                                });

            return lastTwoWeeksSales;
        }
    }
}