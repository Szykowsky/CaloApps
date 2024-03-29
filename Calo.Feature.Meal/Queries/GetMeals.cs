﻿using Calo.Core.Models;
using Calo.Data;
using Calo.Feature.Meals.Extensions;
using Calo.Feature.Meals.Helpers;
using Calo.Feature.Meals.Models;
using FluentValidation;
using MediatR;

namespace Calo.Feature.Meals.Queries;

public class GetMeals
{
    public class Query : IRequest<QueryMealsResult>
    {
        public Guid DietId { get; set; }
        public Guid UserId { get; set; }
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
            return await this.dbContext.Meals
                .OrderBy(m => m.Date)
                .Where(m => m.DietId == request.DietId && m.Diet.UserId == request.UserId)
                .GetMealsQueryFilter(request.MealsFilterModel)
                .SelectMealDto()
                .GetPagedResult(1, 10, cancellationToken);
        }
    }
}
