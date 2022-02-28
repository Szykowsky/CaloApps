﻿using Calo.Core.Models;
using Calo.Data;
using Calo.Feature.Meals.Extensions;
using Calo.Feature.Meals.Helpers;
using Calo.Feature.Meals.Models;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Calo.Feature.Meals.Queries
{
    public class GetMeals
    {
        public class Query : IRequest<QueryMealsResult>
        {
            public Guid DietId { get; set; }
            public MealModels.Filter? MealsFilterModel { get; set; }
        }

        public class QueryMealsResult
        {
            public IEnumerable<MealModels.Dto> QueryResult { get; set; }
            public PaginationBase PaginationBase { get; set; }

        }

        public class GetMealsValidator : AbstractValidator<Query>
        {
            public GetMealsValidator()
            {
                RuleFor(x => x.DietId)
                    .NotEmpty()
                    .NotNull()
                    .WithMessage(ErrorMessage.NotNullDietId);

                When(x => x.MealsFilterModel != null, () =>
                {
                    RuleFor(z => z.MealsFilterModel.DateType)
                        .NotNull()
                        .WithMessage(ErrorMessage.NotNullDateType);
                    RuleFor(z => z.MealsFilterModel.DayNumber)
                        .InclusiveBetween(1, 31)
                        .WithMessage(ErrorMessage.DateNumberRange);
                    RuleFor(z => z.MealsFilterModel.MonthNumber)
                        .InclusiveBetween(1, 12)
                        .WithMessage(ErrorMessage.MonthNumberRange);
                });
            }
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

                if(request.MealsFilterModel == null || request.MealsFilterModel.DateType == MealModels.DateType.None)
                {
                    return await meals
                        .SelectMealDto()
                        .GetPagedResult(1,10, cancellationToken);
                }

                meals = meals.GetMealsQueryFilter(request.MealsFilterModel);

                return await meals
                    .SelectMealDto()
                    .GetPagedResult(1, 10, cancellationToken);
            }
        }
    }
}
