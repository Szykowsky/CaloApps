﻿using Calo.Core.Entities;
using Calo.Core.Models;
using Calo.Data;
using Calo.Feature.Meals.Helpers;
using Calo.Feature.Meals.Models;
using FluentValidation;
using MediatR;

namespace Calo.Feature.Meals.Commands;

public class AddMeal
{
    public class Command : MealModels.CreateOrUpdate, IRequest<RequestStatus>
    {

    }

    public class AddMealValidator : AbstractValidator<Command>
    {
        public AddMealValidator()
        {
            RuleFor(x => x.DietId)
                .NotEmpty()
                .NotNull()
                .WithMessage(ErrorMessage.NotNullDietId);

            RuleFor(x => x.Name)
                .NotEmpty()
                .NotNull()
                .WithMessage(ErrorMessage.NotNullName)
                .MaximumLength(250)
                .WithMessage(ErrorMessage.MaxLengthName);

            RuleFor(x => x.Kcal)
                .NotEmpty()
                .NotNull()
                .WithMessage(ErrorMessage.NotNullKcal)
                .GreaterThanOrEqualTo(0)
                .WithMessage(ErrorMessage.GratherThanKcal);
        }
    }

    public class Handler : IRequestHandler<Command, RequestStatus>
    {
        private readonly CaloContext dbContext;

        public Handler(CaloContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<RequestStatus> Handle(Command request, CancellationToken cancellationToken)
        {
            var isLoggedUserDiet = this.dbContext.Diets.Any(x => x.UserId == request.UserId && x.Id == request.DietId);
            if(!isLoggedUserDiet)
            {
                return new RequestStatus(false, "Can not add meal");
            }

            var meal = new Meal(request.Kcal, request.Name, request.Date, request.DietId);;

            await this.dbContext.AddAsync(meal, cancellationToken);
            await this.dbContext.SaveChangesAsync(cancellationToken);

            return new RequestStatus(true, "Added new meal");
        }

    }
}
