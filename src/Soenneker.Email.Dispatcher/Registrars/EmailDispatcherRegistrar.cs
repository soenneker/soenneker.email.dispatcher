using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Soenneker.Email.Dispatcher.Abstract;
using Soenneker.Email.Util.Registrars;

namespace Soenneker.Email.Dispatcher.Registrars;

/// <summary>
/// Determines email dispatching/routing
/// </summary>
public static class EmailDispatcherRegistrar
{
    /// <summary>
    /// Adds <see cref="IEmailDispatcher"/> as a singleton service. <para/>
    /// </summary>
    /// <param name="services">Service collection that receives the registration.</param>
    /// <returns>The same service collection, so additional registrations can be chained.</returns>
    public static IServiceCollection AddEmailDispatcherAsSingleton(this IServiceCollection services)
    {
        services.AddEmailUtilAsSingleton().TryAddSingleton<IEmailDispatcher, EmailDispatcher>();

        return services;
    }

    /// <summary>
    /// Adds <see cref="IEmailDispatcher"/> as a scoped service. <para/>
    /// </summary>
    /// <param name="services">Service collection that receives the registration.</param>
    /// <returns>The same service collection, so additional registrations can be chained.</returns>
    public static IServiceCollection AddEmailDispatcherAsScoped(this IServiceCollection services)
    {
        services.AddEmailUtilAsScoped().TryAddScoped<IEmailDispatcher, EmailDispatcher>();

        return services;
    }
}
