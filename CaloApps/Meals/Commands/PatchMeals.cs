using Calo.SharedModels;
using CaloApps.Data;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;

namespace CaloApps.Meals.Commands
{
    public class PatchMeals
    {
        public class Command : IRequest<PatchMealsResult>
        {
            public Guid DietId { get; set; }
            public JsonPatchDocument<IDictionary<Guid, MealModels.Patch>> PatchMealsModel { get; set; }
        }

        public class PatchMealsResult
        {
            public bool Success { get; set; }
        }

        public class Handler : IRequestHandler<Command, PatchMealsResult>
        {
            private readonly CaloContext dbContext;

            public Handler(CaloContext dbContext)
            {
                this.dbContext = dbContext;
            }
            public async Task<PatchMealsResult> Handle(Command request, CancellationToken cancellationToken)
            {
                // TODO security risk should also get by user id
                var meals = await this.dbContext.Meals
                    .Where(x => x.DietId == request.DietId)
                    .AsNoTracking()
                    .ToListAsync(cancellationToken);

                var mealPatchListModel = meals.Select(x => new MealModels.Patch
                {
                    DietId = x.DietId,
                    Date = x.Date,
                    Kcal = x.Kcal,
                    Name = x.Name,
                    Id = x.Id,
                }).ToDictionary(x => x.Id);

                request.PatchMealsModel.ApplyTo(mealPatchListModel);

                var mealsResult = mealPatchListModel.Select(x => new Data.Models.Meal
                {
                    Id = x.Value.Id,
                    Name = x.Value.Name,
                    Date = x.Value.Date,
                    Kcal = x.Value.Kcal,
                    DietId = x.Value.DietId,
                }).ToList();

                this.dbContext.Meals.UpdateRange(mealsResult);
                await this.dbContext.SaveChangesAsync(cancellationToken);

                return new PatchMealsResult
                {
                    Success = true,
                };
            }

        }
    }
}
