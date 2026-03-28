using DevFreela.Application.Commands.Project.InsertProject;
using DevFreela.Application.Models;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DevFreela.Application;

public static class ApplicationModule
{
    public static IServiceCollection AddAplication(this IServiceCollection services)
    {
        services.AddServices()
                .AddValidators()
                .AddHandler();
      
        return services;
    }
    private static IServiceCollection AddServices(this IServiceCollection services)
    {
        
        services
            .AddTransient<IPipelineBehavior<InsertProjectCommand, ResultViewModel<Guid>>,
                ValidateInsertProjectCommandBehavior>();
        return services;
    }

    private static IServiceCollection AddValidators(this IServiceCollection services)
    {
        services.AddFluentValidationAutoValidation().AddValidatorsFromAssemblyContaining<InsertProjectCommand>();
        return services;
    }

    private static IServiceCollection AddHandler(this IServiceCollection services)
    {
        services.AddMediatR(config => config.RegisterServicesFromAssemblyContaining<InsertProjectCommand>());
        return services;
    }
}