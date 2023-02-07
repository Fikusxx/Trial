using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;


namespace Trial.Core;


public static class CoreServicesRegistration
{
    public static IServiceCollection AddCoreServices(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());    

        services.AddMediatR(Assembly.GetExecutingAssembly());

        return services;
    }
}
