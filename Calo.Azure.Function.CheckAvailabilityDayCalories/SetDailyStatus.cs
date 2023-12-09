using System;
using System.Collections.Generic;
using System.Linq;
using Calo.Core.Entities;
using Calo.Data;
using Calo.Domain.Entities;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Calo.Azure.Function.CheckAvailabilityDayCalories;

public class SetDailyStatus
{
    private readonly CaloContext caloContext;

    public SetDailyStatus(CaloContext caloContext)
    {
        this.caloContext = caloContext;
    }

    [FunctionName("SetDailyStatus")]
    public void Run([TimerTrigger("0 */1 * * * *")] TimerInfo myTimer, ILogger log)
    {
        log.LogInformation($"C# Timer trigger function started at: {DateTime.Now}");

        var today = DateTime.Now;
        var userIds = this.caloContext.Users.Select(x => x.Id).ToList();

        foreach (var userId in userIds)
        {
            var dailyStatus = this.caloContext.DailyStatuses
                .Where(x => x.Day == today.Day && x.Month == today.Month && x.Year == today.Year && x.UserId == userId)
                .FirstOrDefault();

            var meals = this.caloContext.Meals
                .Include(x => x.Diet)
                .Where(x => x.Diet.UserId == userId && x.Date.Day == today.Day && x.Date.Month == today.Month && x.Date.Year == today.Year)
                .ToList();

            if (meals.Count > 0)
            {
                if (dailyStatus == null)
                {
                    dailyStatus = new DailyStatus(Guid.NewGuid(), today.Day, today.Month, today.Year, 0, meals.FirstOrDefault().Diet.DayKcal, userId);
                    this.caloContext.DailyStatuses.Add(dailyStatus);
                    this.caloContext.SaveChanges();
                }
                else
                {
                    meals = meals.Where(x => x.DailyStatusId != dailyStatus.Id).ToList();
                }

                foreach (var meal in meals)
                {
                    dailyStatus.UpdateKcal(
                        dailyStatus.KcalConsumed += meal.Kcal,
                        dailyStatus.KcalRemaining -= meal.Kcal);

                    meal.UpdateDailyStatusId(dailyStatus.Id);

                }

                this.caloContext.DailyStatuses.Update(dailyStatus);
                this.caloContext.Meals.UpdateRange(meals);
                this.caloContext.SaveChanges();
            }
        }
        log.LogInformation($"C# Timer trigger function started at: {DateTime.Now}");
    }
}
