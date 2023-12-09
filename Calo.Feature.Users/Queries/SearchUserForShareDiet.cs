﻿using Calo.Data;
using Calo.Feature.Users.Models;
using FluentValidation;
using MediatR;

namespace Calo.Feature.Users.Queries
{
    public class SearchUserForShareDiet
    {
        public record Query(Guid UserId, string Login) : IRequest<UserDtos.Base> { }

        public class SearchUserForShareDietValidator : AbstractValidator<Query>
        {
            public SearchUserForShareDietValidator()
            {
                RuleFor(x => x.UserId)
                    .NotEmpty()
                    .NotNull()
                    .WithMessage("");

                RuleFor(x => x.Login)
                    .NotEmpty()
                    .NotNull()
                    .Length(3)
                    .WithMessage("");
            }
        }

        public class Handler : IRequestHandler<Query, UserDtos.Base>
        {
            private readonly CaloContext dbContext;

            public Handler(CaloContext dbContext)
            {
                this.dbContext = dbContext;
            }

            public async Task<UserDtos.Base> Handle(Query request, CancellationToken cancellationToken)
            {
            }
        }
    }
}
