using DevFreela.Application.Commands.InsertProject;
using DevFreela.Application.Models;
using DevFreela.Application.Services;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DevFreela.Application;

public static class ApplicationModule
{
    public static IServiceCollection AddAplication(this IServiceCollection services)
    {
        services.AddServices()
                .AddHandler();
      
        return services;
    }
    private static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IProjectService, ProjectService>();
        services
            .AddTransient<IPipelineBehavior<InsertProjectCommand, ResultViewModel<Guid>>,
                ValidateInsertProjectCommandBehavior>();
        return services;
    }

    private static IServiceCollection AddHandler(this IServiceCollection services)
    {
        services.AddMediatR(config => config.RegisterServicesFromAssemblyContaining<InsertProjectCommand>());
        return services;
    }
}