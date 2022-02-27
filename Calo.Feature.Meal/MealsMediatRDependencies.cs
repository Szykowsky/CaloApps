﻿
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Calo.Feature.Meals
{
    public static class MealsMediatRDependencies
    {
        public static IServiceCollection RegisterRequestHandlers(
            this IServiceCollection services)
        {
            return services
                .AddMediatR(typeof(MealsMediatRDependencies).Assembly);
        }
    }
}
