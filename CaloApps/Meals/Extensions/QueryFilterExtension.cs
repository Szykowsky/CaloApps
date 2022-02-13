using CaloApps.Data.Models;
using CaloApps.Meals.Models;

namespace CaloApps.Meals.Extensions
{
    public static class QueryFilterExtension
    {
        public static IQueryable<Meal> GetMealsQueryFilter(this IQueryable<Meal> meals, MealsFilter mealsFilterModel) =>
            mealsFilterModel.DateType switch
            {
                DateType.Day => meals.Where(m => m.Date.Day == mealsFilterModel.DayNumber),
                DateType.Month => meals.Where(m => m.Date.Month == mealsFilterModel.MonthNumber),
                DateType.DayMonth => meals.Where(m => m.Date.Day == mealsFilterModel.DayNumber && m.Date.Month == mealsFilterModel.MonthNumber)
            };
    }
}
