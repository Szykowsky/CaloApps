using Calo.Core.Models;
using Calo.Data;
using Calo.Feature.MetabolicRate.Helpers;
using Calo.Feature.MetabolicRate.Models;
using Calo.Feature.MetabolicRate.Services;
using FluentValidation;
using MediatR;

namespace Calo.Feature.MetabolicRate.Commands
{
    public class CreateMetabolicRate
    {
        public class Command : MetabolicRateModel.CreateModel,  IRequest<RequestStatus>
        {

        }

        public class CreateMetabolicRateValidator : AbstractValidator<Command>
        {
            public CreateMetabolicRateValidator()
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
            private readonly IMetabolicRateService metabolicRateService;
            public Handler(CaloContext dbContext, IMetabolicRateService metabolicRateService)
            {
                this.dbContext = dbContext;
                this.metabolicRateService = metabolicRateService;
            }

            public async Task<RequestStatus> Handle(Command request, CancellationToken cancellationToken)
            {
                var metabolicRate = this.dbContext.MetabolicRate
                    .Where(x => x.UserId == request.UserId && x.IsActive)
                    .SingleOrDefault();

                if (metabolicRate != null)
                {
                    metabolicRate.SetIsActive(false);
                    metabolicRate.SetModifiedDate();
                    this.dbContext.Update(metabolicRate);
                }

                var newMetabolicRate = this.CreateMetabolicRate(request);
                await this.dbContext.AddAsync(newMetabolicRate, cancellationToken);
                await this.dbContext.SaveChangesAsync(cancellationToken);

                return new RequestStatus(true, "Updated MetabolicRate");
            }

            private Domain.Entities.MetabolicRate.MetabolicRate CreateMetabolicRate(Command command)
            {
                var bmr = this.metabolicRateService.CalculateBMR(command.Gender, command.Formula, command.Weight, command.Growth, command.Age);
                return new Domain.Entities.MetabolicRate.MetabolicRate(
                    command.Gender,
                    command.Activity,
                    command.Formula,
                    command.Weight,
                    command.Growth,
                    command.Age,
                    bmr,
                    this.metabolicRateService.CalculateAMR(bmr, command.Activity),
                    command.UserId);
            }
        }
    }
}
