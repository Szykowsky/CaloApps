using MediatR;
using CaloApps.Data.Models;
using CaloApps.Data;
using CaloApps.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace CaloApps.Users.Commands
{
    public class CreateUser
    {
        public class Command : IRequest<CreateUserResult> 
        {
            public string NickName { get; set; }
        }

        public class CreateUserResult
        {
            public Guid? NewUserId { get; set; }
            public RequestStatus RequestStatus { get; set; }
        }

        public class Handler : IRequestHandler<Command, CreateUserResult>
        {
            private readonly CaloContext dbContext;

            public Handler(CaloContext dbContext)
            {
                this.dbContext = dbContext;
            }

            public async Task<CreateUserResult> Handle(Command command, CancellationToken cancellationToken)
            {
                var isUserExists = await dbContext.Users.AnyAsync(u => u.NickName == command.NickName, cancellationToken);
                if(isUserExists)
                {
                    return new CreateUserResult
                    {
                        NewUserId = null,
                        RequestStatus = new RequestStatus(false, "User name already exists")
                    };
                }

                var user = new User
                {
                    NickName = command.NickName,
                };

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
}
