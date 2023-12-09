using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
using MediatR;

namespace Calo.Feature.MetabolicRate;

public static class MetabolicRateMediatRDependencies
{
    public static IServiceCollection RegisterRequestUserAdditionalDataHandlers(
        this IServiceCollection services)
    {
        return services
            .AddMediatR(typeof(MetabolicRateMediatRDependencies).Assembly)
            .AddValidatorsFromAssembly(typeof(MetabolicRateMediatRDependencies).Assembly);
    }
}