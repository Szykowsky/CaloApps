using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
