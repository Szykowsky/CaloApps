using Calo.Core.Entities;
using Calo.Core.Models;
using Calo.Data;
using Calo.Feature.UserSettings.Helpers;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calo.Feature.UserSettings.Commands
{
    public class CreateOrUpdateUserAdditionalData
    {
        public class Command : IRequest<RequestStatus>
        {
            public Gender Gender { get; set; }
            public int Weight { get; set; }
            public int Growth { get; set; }
            public int Age { get; set; }
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
                var userSetting = this.dbContext.Users
                    .Where(x => x.Id == request.UserId)
                    .Select(x => x.Setting)
                    .SingleOrDefault();

                if(userSetting == null)
                {
                    var newSetting = CreateSetting(request);
                    await this.dbContext.AddAsync(newSetting, cancellationToken);
                    await this.dbContext.SaveChangesAsync(cancellationToken);

                    return new RequestStatus(true, "Added new settings");
                }

                userSetting.Growth = request.Growth;
                userSetting.Age = request.Age;
                userSetting.Weight = request.Weight;
                userSetting.Gender = request.Gender;

                this.dbContext.Update(userSetting);
                await this.dbContext.SaveChangesAsync(cancellationToken);

                return new RequestStatus(true, "Updated settings");
            }

            private static UserAdditionalData CreateSetting(Command command)
            {
                return new UserAdditionalData
                {
                    CreatedDate = DateTime.Now,
                    Age = command.Age,
                    Gender = command.Gender,
                    Growth = command.Growth,
                    Weight = command.Weight,
                };
            }
        }
    }
}
