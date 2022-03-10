using Calo.Core.Models;
using Calo.Data;
using Calo.Feature.Users.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Calo.Feature.Users.Commands
{
    public class RefreshUserToken
    {
        public class Command : IRequest<RefreshUserTokenResultModel>
        {
            public string AccessToken { get; set; }
            public string RefreshToken { get; set; }
        }

        public class RefreshUserTokenResultModel
        {
            public string AccessToken { get; set; }
            public string RefreshToken { get; set; }
        }

        public class Handler : IRequestHandler<Command, RefreshUserTokenResultModel>
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

            public async Task<RefreshUserTokenResultModel> Handle(Command command, CancellationToken cancellationToken)
            {
                var principal = this.tokenService.GetPrincipalFromToken(command.AccessToken, this.appSettings.JWTSecurity.AccessTokenSecret);
                if(principal == null || principal?.Identity?.Name == null)
                {
                    return null;
                }

                var user = await this.dbContext.Users
                    .Where(x => x.Login == principal.Identity.Name)
                    .FirstOrDefaultAsync(cancellationToken);
                if (user == null || user.RefreshToken != command.RefreshToken)
                {
                    return null;
                }

                var isRefreshTokenValid = this.tokenService.Validate(command.RefreshToken, this.appSettings.JWTSecurity.RefreshTokenSecret);
                if(!isRefreshTokenValid)
                {
                    return null;
                }

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.GivenName, user.Login),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                };
                var accessToken = this.tokenService.GetToken(claims, this.appSettings.JWTSecurity.AccessTokenSecret, this.appSettings.JWTSecurity.AccessTokenExpiredTime);
                var refreshToken = this.tokenService.GetToken(claims, this.appSettings.JWTSecurity.RefreshTokenSecret, this.appSettings.JWTSecurity.RefreshTokenExpiredTime);

                user.RefreshToken = refreshToken;
                this.dbContext.Update(user);
                await this.dbContext.SaveChangesAsync(cancellationToken);

                return new RefreshUserTokenResultModel
                {
                    AccessToken = accessToken,
                    RefreshToken = refreshToken,
                };
            }
        }
    }
}
