using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Calo.Feature.Users;

public static class UsersMediatRDependencies
{
    public static IServiceCollection RegisterRequestUsersHandlers(
        this IServiceCollection services)
    {
        return services
            .AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()))
            .AddValidatorsFromAssembly(typeof(UsersMediatRDependencies).Assembly); ;
    }
}
