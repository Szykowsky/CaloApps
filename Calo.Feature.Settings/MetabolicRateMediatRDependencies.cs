using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
using MediatR;
using System.Reflection;

namespace Calo.Feature.MetabolicRate;

public static class MetabolicRateMediatRDependencies
{
    public static IServiceCollection RegisterRequestUserAdditionalDataHandlers(
        this IServiceCollection services)
    {
        return services
            .AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()))
            .AddValidatorsFromAssembly(typeof(MetabolicRateMediatRDependencies).Assembly);
    }
}