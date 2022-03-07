using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
using MediatR;

namespace Calo.Feature.UserSettings
{
    public static class SettingsMediatRDependencies
    {
        public static IServiceCollection RegisterRequestSettingsHandlers(
            this IServiceCollection services)
        {
            return services
                .AddMediatR(typeof(SettingsMediatRDependencies).Assembly)
                .AddValidatorsFromAssembly(typeof(SettingsMediatRDependencies).Assembly);
        }
    }
}