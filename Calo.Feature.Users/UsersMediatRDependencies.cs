using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Calo.Feature.Users
{
    public static class UsersMediatRDependencies
    {
        public static IServiceCollection RegisterRequestUsersHandlers(
            this IServiceCollection services)
        {
            return services
                .AddMediatR(typeof(UsersMediatRDependencies).Assembly);
        }
    }
}
