using Calo.Core.Models;
using Calo.Data;
using Calo.Feature.MetabolicRate.Helpers;
using Calo.Feature.MetabolicRate.Models;
using Calo.Feature.MetabolicRate.Services;
using FluentValidation;
using MediatR;

namespace Calo.Feature.MetabolicRate.Commands;

public class UpdateMetabolicRate
{
    public class Command : MetabolicRateModel.UpdateModel, IRequest<RequestStatus>
    {

    }

    public class CreateMetabolicRateValidator : AbstractValidator<Command>
    {
        public CreateMetabolicRateValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .NotNull()
                .WithMessage(ErrorMessages.IdNotNull);

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
                    .Where(x => x.UserId == request.UserId && x.Id == request.Id)
                    .FirstOrDefault();

                if (metabolicRate == null)
                {
                    return new RequestStatus(false, "Metbolic rate do not exist");
                }

                var bmr = this.metabolicRateService.CalculateBMR(request.Gender, request.Formula, request.Weight, request.Growth, request.Age);
                var amr = this.metabolicRateService.CalculateAMR(bmr, request.Activity);
                metabolicRate.Update(request.Gender, request.Activity, request.Formula, request.Weight, request.Growth, request.Age, bmr, amr);

                this.dbContext.Update(metabolicRate);
                await this.dbContext.SaveChangesAsync(cancellationToken);

                return new RequestStatus(true, "Updated MetabolicRate");
            }
        }
    }
}
