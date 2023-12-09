using Calo.Core.Models;
using Calo.Data;
using Calo.Feature.Users.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Security.Claims;

namespace Calo.Feature.Users.Commands;

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
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }

    public class Handler : IRequestHandler<Command, LoginResultModel>
    {
        private readonly CaloContext dbContext;
        private readonly ITokenService tokenService;
        private readonly AppSettings appSettings;

        public Handler(CaloContext dbContext, ITokenService tokenService, IOptions<AppSettings> appSettings)
        {
            this.dbContext = dbContext;
            this.tokenService = tokenService;
            this.appSettings = appSettings.Value;
        }

        public async Task<LoginResultModel> Handle(Command command, CancellationToken cancellationToken)
        {
            var user = await this.dbContext.Users.Where(x => x.Login == command.Login).FirstOrDefaultAsync(cancellationToken);
            if (user == null)
            {
                return null;
            }

            var isCorrectPassword = user.IsPasswordCorrect(command.Password, this.appSettings.Pepper);
            if (!isCorrectPassword)
            {
                return null;
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Login),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            };
            var accessToken = this.tokenService.GetToken(claims, this.appSettings.JWTSecurity.AccessTokenSecret, this.appSettings.JWTSecurity.AccessTokenExpiredTime);
            var refreshToken = this.tokenService.GetToken(claims, this.appSettings.JWTSecurity.RefreshTokenSecret, this.appSettings.JWTSecurity.RefreshTokenExpiredTime);

            user.UpdateRefreshToken(refreshToken);
            this.dbContext.Update(user);
            await this.dbContext.SaveChangesAsync(cancellationToken);

            return new LoginResultModel
            {
                Login = user.Login,
                AccessToken = accessToken,
                RefreshToken = refreshToken,
            };
        }
    }
}
