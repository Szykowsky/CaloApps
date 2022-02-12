using CaloApps.Data;
using CaloApps.Meals.Extensions;
using CaloApps.Meals.Models;
using CaloApps.Shared.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CaloApps.Meals.Queries
{
    public class GetMeals
    {
        public class Query : IRequest<QueryMealsResult>
        {
            public Guid DietId { get; set; }
            public MealsFilter? MealsFilterModel { get; set; }
        }

        public class QueryMealsResult
        {
            public IEnumerable<MealDto> QueryResult { get; set; }
            public PaginationBase PaginationBase { get; set; }

        }
        public class Handler : IRequestHandler<Query, QueryMealsResult>
        {
            private readonly CaloContext dbContext;

            public Handler(CaloContext dbContext)
            {
                this.dbContext = dbContext;
            }

            public async Task<QueryMealsResult> Handle(Query request, CancellationToken cancellationToken)
            {
                var meals = this.dbContext.Meals
                    .OrderBy(m => m.Date)
                    .Where(m => m.DietId == request.DietId)
                    .AsQueryable();

                if(request.MealsFilterModel == null)
                {
                    return await meals.SelectMealDto().GetPagedResult(1,10, cancellationToken);
                }

                if(request.MealsFilterModel.DateType != DateType.None)
                {
                    switch (request.MealsFilterModel.DateType)
                    {
                        case DateType.Day:
                            meals = meals.Where(m => m.Date.Day == request.MealsFilterModel.DayNumber);
                            break;
                        case DateType.Month:
                            meals = meals.Where(m => m.Date.Month == request.MealsFilterModel.MonthNumber);
                            break;
                        case DateType.DayMonth:
                            meals = meals.Where(m => m.Date.Day == request.MealsFilterModel.DayNumber && m.Date.Month == request.MealsFilterModel.MonthNumber);
                            break;
                    }
                }

                return await meals
                    .SelectMealDto()
                    .GetPagedResult(1, 10, cancellationToken);
            }
        }
    }
}
