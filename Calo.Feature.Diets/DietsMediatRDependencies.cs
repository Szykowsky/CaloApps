using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Calo.Feature.Diets;

public static class DietsMediatRDependencies
{
    public static IServiceCollection RegisterRequestDietsHandlers(
        this IServiceCollection services)
    {
        return services
            .AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()))
            .AddValidatorsFromAssembly(typeof(DietsMediatRDependencies).Assembly);
    }
}