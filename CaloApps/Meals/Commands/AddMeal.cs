using CaloApps.Data;
using CaloApps.Data.Models;
using CaloApps.Shared.Models;
using FluentValidation;
using MediatR;

namespace CaloApps.Meals.Commands
{
    public class AddMeal
    {
        public class Command : IRequest<RequestStatus>
        {
            public int Kcal { get; set; }
            public string Name { get; set; }
            public DateTime Date { get; set; }
            public Guid DietId { get; set; }
        }

        public class AddMealValidator : AbstractValidator<Command>
        {
            public AddMealValidator()
            {
                RuleFor(x => x.DietId)
                    .NotEmpty()
                    .NotNull()
                    .WithMessage("You have to add diet id");

                RuleFor(x => x.Name)
                    .NotEmpty()
                    .NotNull()
                    .WithMessage("You Have to add name of meal")
                    .MaximumLength(250)
                    .WithMessage("Max length: 250 characters");

                RuleFor(x => x.Kcal)
                    .NotEmpty()
                    .NotNull()
                    .WithMessage("You Have to add kcal")
                    .GreaterThanOrEqualTo(0)
                    .WithMessage("Kcal must be grather than 0");
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
                var meal = new Meal
                {
                    Kcal = request.Kcal,
                    Name = request.Name,
                    Date = request.Date,
                    DietId = request.DietId,
                };

                await this.dbContext.AddAsync(meal, cancellationToken);
                await this.dbContext.SaveChangesAsync(cancellationToken);

                return new RequestStatus(true, "Added new meal");
            }

        }
    }
}
