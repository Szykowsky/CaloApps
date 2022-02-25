using CaloApps.Data;
using CaloApps.Users.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CaloApps.Users.Commands
{
    public class LoginUser
    {
        public class Command : IRequest<LoginResultModel>
        {
            public string Login { get; set; }
            public string Password { get; set; }
        }

        public class LoginResultModel
        {
            public string Login { get; set; }
            public string Token { get; set; }
        }

        public class Handler : IRequestHandler<Command, LoginResultModel>
        {
            private readonly CaloContext dbContext;
            private readonly ITokenService tokenService;
            private readonly IPasswordService passwordService;

            public Handler(CaloContext dbContext, ITokenService tokenService, IPasswordService passwordService)
            {
                this.dbContext = dbContext;
                this.tokenService = tokenService;
                this.passwordService = passwordService;
            }

            public async Task<LoginResultModel> Handle(Command command, CancellationToken cancellationToken)
            {
                var user = await this.dbContext.Users.Where(x => x.Login == command.Login).FirstOrDefaultAsync(cancellationToken);
                if(user == null)
                {
                    return null;
                }

                var passwordHash = this.passwordService.PreparePasswordHash(command.Password, user.Salt);
                if(passwordHash != user.PasswordHash)
                {
                    return null;
                }

                var token = this.tokenService.GetToken(user.Login, user.Id);

                return new LoginResultModel
                {
                    Login = user.Login,
                    Token = token,
                };
            }
        }
    }
}
