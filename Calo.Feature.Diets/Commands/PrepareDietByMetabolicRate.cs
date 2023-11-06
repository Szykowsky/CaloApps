using Calo.Core.Entities;
using Calo.Core.Models;
using Calo.Data;
using FluentValidation;
using MediatR;

namespace Calo.Feature.Diets.Commands
{
    public class PrepareDietByMetabolicRate
    {
        public class Command : IRequest<RequestStatus>
        {
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
                var metabolicRate = this.dbContext.MetabolicRate
                   .Where(x => x.UserId == request.UserId && x.IsActive)
                   .SingleOrDefault();

                if(metabolicRate is null)
                {
                    return new RequestStatus(false, "Canno find metabolic rate");
                }    

                var diet = new Diet
                {
                    Name = "Diet created by metabolic rate",
                    DayKcal = metabolicRate.ActiveMetabolicRate,
                    Carbohydrates = null,
                    Fiber = null,
                    Protein = null,
                    Fats = null,
                    Minerals = null,
                    UserId = request.UserId,
                    Vitamins = null,
                    CreatedDate = DateTime.Now,
                };

                await this.dbContext.AddAsync(diet, cancellationToken);
                await dbContext.SaveChangesAsync(cancellationToken);

                return new RequestStatus(true, "Added new diet");
            }
        }
    }
}
