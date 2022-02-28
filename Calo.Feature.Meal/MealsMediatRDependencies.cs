
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation;

namespace Calo.Feature.Meals
{
    public static class MealsMediatRDependencies
    {
        public static IServiceCollection RegisterRequestMealsHandlers(
            this IServiceCollection services)
        {
            return services
                .AddMediatR(typeof(MealsMediatRDependencies).Assembly)
                .AddValidatorsFromAssembly(typeof(MealsMediatRDependencies).Assembly);
        }
    }
}
