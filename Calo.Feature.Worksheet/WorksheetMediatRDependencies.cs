using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Calo.Feature.Worksheet;

public static class WorksheetMediatRDependencies
{
    public static IServiceCollection RegisterRequestWorksheetHandlers(
        this IServiceCollection services)
    {
        return services
            .AddMediatR(typeof(WorksheetMediatRDependencies).Assembly)
            .AddValidatorsFromAssembly(typeof(WorksheetMediatRDependencies).Assembly);
    }
}