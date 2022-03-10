﻿using Calo.Core.Entities;
using Calo.Core.Models;
using Calo.Data;
using Calo.Feature.Meals.Helpers;
using FluentValidation;
using MediatR;

namespace Calo.Feature.Meals.Commands
{
    public class AddMeal
    {
        public class Command : IRequest<RequestStatus>
        {
            public int Kcal { get; set; }
            public string Name { get; set; }
            public DateTime Date { get; set; }
            public Guid DietId { get; set; }
            public Guid UserId { get; set; }
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

                var meal = new Meal
                {
                    Kcal = request.Kcal,
                    Name = request.Name,
                    Date = request.Date,
                    DietId = request.DietId,
                    CreatedDate = DateTime.Now,
                };

                await this.dbContext.AddAsync(meal, cancellationToken);
                await this.dbContext.SaveChangesAsync(cancellationToken);

                return new RequestStatus(true, "Added new meal");
            }

        }
    }
}
