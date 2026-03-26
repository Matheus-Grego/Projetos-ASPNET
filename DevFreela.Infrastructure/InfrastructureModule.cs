using System.Text;
using DevFreela.Domain.Repositories;
using DevFreela.Infrastructure.Auth;
using DevFreela.Infrastructure.Cache;
using DevFreela.Infrastructure.Notifications;
using DevFreela.Infrastructure.Persistance;
using DevFreela.Infrastructure.Persistance.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using SendGrid.Extensions.DependencyInjection;

namespace DevFreela.Infrastructure;

public static class InfrastructureModule
{
    public static IServiceCollection AddInfrasctucture(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddRepositories()
            .AddCacheService()
            .AddAuth(configuration)
            .AddData(configuration)
            .AddEmailService(configuration);
        return services;
    }

    private static IServiceCollection AddData(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection"); 
        services.AddDbContext<DevFreelaDbContext>(o => o.UseNpgsql(connectionString));
        return services;
    }
    
    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IProjectRepository, ProjectRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ISkillRepository, SkillRepository>();

        return services;
    }

    private static IServiceCollection AddAuth(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IAuthService, AuthService>();
        
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(o =>
            {
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer =  true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["Jwt:Issuer"],
                    ValidAudience = configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]))
                    
                };
            });
        return services;
    }

    private static IServiceCollection AddCacheService(this IServiceCollection services)
    {
        services.AddScoped<IRecoveryPasswordCache, RecoveryPasswordCache>();
        return services;
    }

    private static IServiceCollection AddEmailService(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSendGrid(o =>
        {
            o.ApiKey = configuration.GetValue<string>("SendGrid:ApiKey");
        });
        services.AddScoped<IEmailService, EmailService>();
        return services;
    }
}