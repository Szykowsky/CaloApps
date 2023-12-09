using MediatR;
using Microsoft.EntityFrameworkCore;
using Calo.Data;
using Calo.Core.Entities;
using Calo.Core.Models;
using Calo.Feature.Users.Services;
using FluentValidation;
using Calo.Feature.Users.Helpers;
using Microsoft.Extensions.Options;

namespace Calo.Feature.Users.Commands;

public class CreateUser
{
    public class Command : IRequest<CreateUserResult>
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }

    public class CreateUserResult
    {
        public Guid? NewUserId { get; set; }
        public RequestStatus RequestStatus { get; set; }
    }

    public class GetCreateUserValidator : AbstractValidator<Command>
    {
        public GetCreateUserValidator()
        {
            RuleFor(x => x.Login)
                .NotEmpty()
                .NotNull().WithMessage(ErrorMessage.NotNullLogin)
                .MinimumLength(2).WithMessage(ErrorMessage.MinLengthLogin);
            RuleFor(x => x.Password)
                .NotEmpty()
                .NotNull().WithMessage(ErrorMessage.NotNullPassword)
                .MinimumLength(6).WithMessage(ErrorMessage.MinLengthPassword);

            RuleFor(x => x.ConfirmPassword)
                .Equal(x => x.Password).WithMessage(ErrorMessage.PasswordsMatch);
        }
    }

    public class Handler : IRequestHandler<Command, CreateUserResult>
    {
        private readonly CaloContext dbContext;
        private readonly AppSettings appSettings;

        public Handler(CaloContext dbContext, IOptions<AppSettings> appSettings)
        {
            this.dbContext = dbContext;
            this.appSettings = appSettings.Value;
        }

        public async Task<CreateUserResult> Handle(Command command, CancellationToken cancellationToken)
        {
            var isUserExists = await dbContext.Users.AnyAsync(u => u.Login == command.Login, cancellationToken);
            if (isUserExists)
            {
                return new CreateUserResult
                {
                    NewUserId = null,
                    RequestStatus = new RequestStatus(false, "User name already exists")
                };
            }
            var user = new User(command.Login);
            user.GenerateSalt();
            user.SetPasswordHash(command.Password, this.appSettings.Pepper);

            await this.dbContext.Users.AddAsync(user, cancellationToken);
            await this.dbContext.SaveChangesAsync(cancellationToken);

            return new CreateUserResult
            {
                NewUserId = user.Id,
                RequestStatus = new RequestStatus(true, "Added new user")
            };
        }
    }
}
