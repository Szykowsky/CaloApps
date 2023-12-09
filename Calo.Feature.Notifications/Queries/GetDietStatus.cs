using Calo.Core.Entities;
using Calo.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calo.Feature.Notifications.Queries;

public class GetDietStatus
{
    public class Query : IRequest<QueryDietStatusResult>
    {
        public Guid UserId { get; set; }
    }

    public class QueryDietStatusResult
    {
        public IEnumerable<DailyStatus> DailyStatus { get; set; } = new List<DailyStatus>();
        public MonthlyStatus MonthlyStatus { get; set; }
    }

    public class DailyStatus
    {
        public int Day { get; set; }
        public int KcalConsumed { get; set; }
        public int KcalRemaining { get; set; }
    }

    public class MonthlyStatus
    {
        public int Month { get; set; }
        public int KcalConsumed { get; set; }
        public int KcalRemaining { get; set; }
        public int KcalLimit { get; set; }
        public IList<int> DaysOverDailyLimit { get; set; }
        public IList<int> DaysNotReported { get; set; }
    }

    public class Handler : IRequestHandler<Query, QueryDietStatusResult>
    {
        private readonly CaloContext dbContext;

        public Handler(CaloContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<QueryDietStatusResult> Handle(Query request, CancellationToken cancellationToken)
        {
            var queryDataResult = await this.dbContext.Diets
                .Where(x => x.UserId == request.UserId && x.User.SelectedDietId == x.Id)
                .Take(1)
                .SelectMany(x => x.Meals)
                .Include(x => x.Diet)
                .Where(x => x.Date.Month == DateTime.Now.Month)
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            var daysOverLimit = new List<int>();
            var dailyStatusList = new List<DailyStatus>();
            var monthStatus = PrepareBasicMonthStatus(queryDataResult);

            foreach (var meal in queryDataResult)
            {
                var dailyStatus = dailyStatusList
                    .Where(x => x.Day == meal.Date.Day)
                    .FirstOrDefault();

                if (dailyStatus == null)
                {
                    dailyStatus = new DailyStatus
                    {
                        Day = meal.Date.Day
                    };
                    dailyStatusList.Add(dailyStatus);
                }
                dailyStatus.KcalConsumed += meal.Kcal;
                dailyStatus.KcalRemaining = meal.Diet.DayKcal - dailyStatus.KcalConsumed;

                if (dailyStatus.KcalRemaining < 0)
                {
                    dailyStatus.KcalRemaining = 0;
                    if (!daysOverLimit.Contains(meal.Date.Day))
                    {
                        daysOverLimit.Add(meal.Date.Day);
                    }
                }

                PrepareMonthStatusKcalData(ref monthStatus, meal);
            }

            if (dailyStatusList.Count != DateTime.Now.Day)
            {
                monthStatus.DaysNotReported = PrepareDaysNotReported(dailyStatusList);
            }
            monthStatus.DaysOverDailyLimit = daysOverLimit;

            return new QueryDietStatusResult
            {
                DailyStatus = dailyStatusList,
                MonthlyStatus = monthStatus,
            };
        }

        private static MonthlyStatus PrepareBasicMonthStatus(IList<Meal> meals)
        {
            var actualMonth = DateTime.Now.Month;
            var daysInMonth = DateTime.DaysInMonth(DateTime.Now.Year, actualMonth);
            var monthStatus = new MonthlyStatus
            {
                KcalRemaining = (meals?.FirstOrDefault()?.Diet.DayKcal ?? 0) * daysInMonth,
                KcalLimit = (meals?.FirstOrDefault()?.Diet.DayKcal ?? 0) * daysInMonth,
                Month = DateTime.Now.Month
            };
            return monthStatus;
        }

        private static void PrepareMonthStatusKcalData(ref MonthlyStatus monthStatus, Meal meal)
        {
            monthStatus.KcalConsumed += meal.Kcal;
            monthStatus.KcalRemaining -= meal.Kcal;

            if (monthStatus.KcalRemaining < 0)
            {
                monthStatus.KcalRemaining = 0;
            }
        }

        private static IList<int> PrepareDaysNotReported(IList<DailyStatus> dailyStatusList)
        {
            var daysCalendarList = new List<int>();
            for (int i = 1; i <= DateTime.Now.Day; i++)
            {
                daysCalendarList.Add(i);
            }

            daysCalendarList = daysCalendarList
                .Except(dailyStatusList.Select(x => x.Day))
                .ToList();
            return daysCalendarList;
        }
    }
}
