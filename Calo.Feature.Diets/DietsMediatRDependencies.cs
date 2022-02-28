using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Calo.Feature.Diets
{
    public static class DietsMediatRDependencies
    {
        public static IServiceCollection RegisterRequestDietsHandlers(
            this IServiceCollection services)
        {
            return services
                .AddMediatR(typeof(DietsMediatRDependencies).Assembly);
        }
    }
}