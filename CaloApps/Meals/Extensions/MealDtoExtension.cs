using CaloApps.Data.Models;
using CaloApps.Meals.Models;
using static CaloApps.Meals.Queries.GetMeals;

namespace CaloApps.Meals.Extensions
{
    public static class MealDtoExtension
    {
        public static IQueryable<MealDto> SelectMealDto(this IQueryable<Meal> meals)
        {
            var result = meals.Select(m => new MealDto
            {
                Kcal = m.Kcal,
                Name = m.Name,
                Date = m.Date,
                Id = m.Id
            });

            return result;
        }
    }
}
