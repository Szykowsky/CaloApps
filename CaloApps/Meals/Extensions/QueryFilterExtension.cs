using Calo.SharedModels;
using CaloApps.Data.Models;

namespace CaloApps.Meals.Extensions
{
    public static class QueryFilterExtension
    {
        public static IQueryable<Meal> GetMealsQueryFilter(this IQueryable<Meal> meals, MealModels.Filter mealsFilterModel) =>
            mealsFilterModel.DateType switch
            {
                MealModels.DateType.Day => meals.Where(m => m.Date.Day == mealsFilterModel.DayNumber),
                MealModels.DateType.Month => meals.Where(m => m.Date.Month == mealsFilterModel.MonthNumber),
                MealModels.DateType.DayMonth => meals.Where(m => m.Date.Day == mealsFilterModel.DayNumber && m.Date.Month == mealsFilterModel.MonthNumber)
            };
    }
}
