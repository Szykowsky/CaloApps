using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
using MediatR;

namespace Calo.Feature.UserSettings
{
    public static class UserAdditionalDataMediatRDependencies
    {
        public static IServiceCollection RegisterRequestUserAdditionalDataHandlers(
            this IServiceCollection services)
        {
            return services
                .AddMediatR(typeof(UserAdditionalDataMediatRDependencies).Assembly)
                .AddValidatorsFromAssembly(typeof(UserAdditionalDataMediatRDependencies).Assembly);
        }
    }
}