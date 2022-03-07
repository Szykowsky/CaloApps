using Calo.Core.Entities;
using Calo.Core.Models;
using Calo.Data;
using Calo.Feature.MetabolicRate.Helpers;
using Calo.Feature.MetabolicRate.Models;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calo.Feature.MetabolicRate.Commands
{
    public class CreateMetabolicRate
    {
        public class Command : IRequest<RequestStatus>
        {
            public Gender Gender { get; set; }
            public int Weight { get; set; }
            public int Growth { get; set; }
            public int Age { get; set; }
            public Activity Activity { get; set; }
            public Guid UserId { get; set; }
        }

        public class AddMealValidator : AbstractValidator<Command>
        {
            public AddMealValidator()
            {
                RuleFor(x => x.UserId)
                    .NotEmpty()
                    .NotNull()
                    .WithMessage(ErrorMessages.UserIdNotNull);

                RuleFor(x => x.Weight)
                    .GreaterThan(0)
                    .WithMessage(x => ErrorMessages.PrepareMessage(nameof(x.Weight), "0"));

                RuleFor(x => x.Growth)
                    .GreaterThan(0)
                    .WithMessage(x => ErrorMessages.PrepareMessage(nameof(x.Growth), "0"));

                RuleFor(x => x.Age)
                    .GreaterThan(18)
                    .WithMessage(x => ErrorMessages.PrepareMessage(nameof(x.Age), "18"));
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
                    .Where(x => x.UserId == request.UserId)
                    .Where(metabolicRate => metabolicRate.Gender == request.Gender &&
                            metabolicRate.Weight == request.Weight &&
                            metabolicRate.Growth == request.Growth &&
                            metabolicRate.Age == request.Age &&
                            metabolicRate.Activity == request.Activity)
                    .SingleOrDefault();

                if(metabolicRate != null)
                {
                    return new RequestStatus(false, "MetabolicRate exist");
                }

                var newMetabolicRate = CreateMetabolicRate(request);
                await this.dbContext.AddAsync(newMetabolicRate, cancellationToken);
                await this.dbContext.SaveChangesAsync(cancellationToken);

                return new RequestStatus(true, "Updated MetabolicRate");
            }

            private static Core.Entities.MetabolicRate CreateMetabolicRate(Command command)
            {
                var bmr = CalculateBMR(command);
                return new Core.Entities.MetabolicRate
                {
                    CreatedDate = DateTime.Now,
                    Age = command.Age,
                    Gender = command.Gender,
                    Growth = command.Growth,
                    Weight = command.Weight,
                    UserId = command.UserId,
                    BasalMetabolicRate = bmr,
                    ActiveMetabolicRate = CalculateAMR(bmr, command.Activity)
                };
            }

            private static int CalculateBMR(Command command) =>
                command.Gender switch
                {
                    Gender.Male => Convert.ToInt32(88.362f + (13.397f * command.Weight) + (4.799f * command.Growth) + (5.677f * command.Age)),
                    Gender.Female => Convert.ToInt32(447.593f + (9.247f * command.Weight) + (3.098f * command.Growth) + (4.330f * command.Age)),
                };

            private static int CalculateAMR(int bmr, Activity activity) =>
                activity switch
                {
                    Activity.Sedentary => Convert.ToInt32(bmr * 1.2),
                    Activity.LightlyActive => Convert.ToInt32(bmr * 1.375),
                    Activity.ModeratelyActive => Convert.ToInt32(bmr * 1.55),
                    Activity.Active => Convert.ToInt32(bmr * 1.725),
                    Activity.VeryActive => Convert.ToInt32(bmr * 1.9),
                };
        }
    }
}
