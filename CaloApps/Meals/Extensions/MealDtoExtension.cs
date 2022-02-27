﻿using Calo.Core.Entities;
using Calo.SharedModels;

namespace CaloApps.Meals.Extensions
{
    public static class MealDtoExtension
    {
        public static IQueryable<MealModels.Dto> SelectMealDto(this IQueryable<Meal> meals)
        {
            var result = meals.Select(m => new MealModels.Dto
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
