
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
using System.Reflection;

namespace Calo.Feature.Meals;

public static class MealsMediatRDependencies
{
    public static IServiceCollection RegisterRequestMealsHandlers(
        this IServiceCollection services)
    {
        return services
            .AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()))
            .AddValidatorsFromAssembly(typeof(MealsMediatRDependencies).Assembly);
    }
}
