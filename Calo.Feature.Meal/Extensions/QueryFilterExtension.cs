using Calo.Core.Entities;
using Calo.Feature.Meals.Models;

namespace Calo.Feature.Meals.Extensions
{
    public static class QueryFilterExtension
    {
        public static IQueryable<Meal> GetMealsQueryFilter(this IQueryable<Meal> meals, MealModels.Filter? mealsFilterModel)
        {
            if(mealsFilterModel == null)
            {
                return meals;
            }

            if(mealsFilterModel.DayNumber is not null)
            {
                meals = meals.Where(m => m.Date.Day == mealsFilterModel.DayNumber);
            }
            if(mealsFilterModel.MonthNumber is not null)
            {
                meals = meals.Where(m => m.Date.Month == mealsFilterModel.MonthNumber);
            }

            return meals;
        }

    }
}
