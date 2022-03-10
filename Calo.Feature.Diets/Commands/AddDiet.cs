using Calo.Core.Entities;
using Calo.Core.Models;
using Calo.Data;
using FluentValidation;
using MediatR;

namespace Calo.Feature.Diets.Commands
{
    public class AddDiet
    {
        public class Command : IRequest<RequestStatus>
        {
            public string Name { get; set; }
            public int DayKcal { get; set; }
            public int? Carbohydrates { get; set; }
            public int? Fiber { get; set; }
            public int? Protein { get; set; }
            public int? Fats { get; set; }
            public int? Vitamins { get; set; }
            public int? Minerals { get; set; }
            public Guid UserId { get; set; }
        }

        public class AddMealValidator : AbstractValidator<Command>
        {
            public AddMealValidator()
            {
                RuleFor(x => x.UserId)
                    .NotEmpty()
                    .NotNull()
                    .WithMessage("You have to add user id");

                RuleFor(x => x.Name)
                    .NotEmpty()
                    .NotNull()
                    .WithMessage("You Have to add name of meal")
                    .MaximumLength(250)
                    .WithMessage("Max length: 250 characters");

                RuleFor(x => x.DayKcal)
                    .NotEmpty()
                    .NotNull()
                    .WithMessage("You Have to add day kcal")
                    .GreaterThan(0)
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
                var diet = new Diet
                {
                    Name = request.Name,
                    DayKcal = request.DayKcal,
                    Carbohydrates = request.Carbohydrates,
                    Fiber = request.Fiber,
                    Protein = request.Protein,
                    Fats = request.Fats,
                    Minerals = request.Minerals,
                    UserId = request.UserId,
                    Vitamins = request.Vitamins,
                    CreatedDate = DateTime.Now,
                };

                await this.dbContext.AddAsync(diet, cancellationToken);
                await dbContext.SaveChangesAsync(cancellationToken);

                return new RequestStatus(true, "Added new diet");
            }
        }
    }
}
