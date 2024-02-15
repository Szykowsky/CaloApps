using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Calo.Feature.Notifications;

public static class NotificationsMediatRDependencies
{
    public static IServiceCollection RegisterRequestNotificationsHandlers(
        this IServiceCollection services)
    {
        return services
            .AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()))
            .AddValidatorsFromAssembly(typeof(NotificationsMediatRDependencies).Assembly);
    }
}
