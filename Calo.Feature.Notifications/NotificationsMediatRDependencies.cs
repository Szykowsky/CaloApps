using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Calo.Feature.Notifications
{
    public static class NotificationsMediatRDependencies
    {
        public static IServiceCollection RegisterRequestNotificationsHandlers(
            this IServiceCollection services)
        {
            return services
                .AddMediatR(typeof(NotificationsMediatRDependencies).Assembly)
                .AddValidatorsFromAssembly(typeof(NotificationsMediatRDependencies).Assembly);
        }
    }
}
