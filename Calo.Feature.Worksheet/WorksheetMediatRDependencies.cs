using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Calo.Feature.Worksheet;

public static class WorksheetMediatRDependencies
{
    public static IServiceCollection RegisterRequestWorksheetHandlers(
        this IServiceCollection services)
    {
        return services
            .AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()))
            .AddValidatorsFromAssembly(typeof(WorksheetMediatRDependencies).Assembly);
    }
}