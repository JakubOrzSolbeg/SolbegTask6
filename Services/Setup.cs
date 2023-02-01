using DTOs.Enums;
using Microsoft.Extensions.DependencyInjection;
using Services.Implementations;
using Services.Interfaces;

namespace Services;

public static class Setup
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IAccountService, AccountService>();
        services.AddScoped<ISubscriptionService, SubscriptionService>();
        services.AddScoped<ITransferService, TransferService>();
        services.AddSingleton<ITokenService, TokenService>();
        
        return services;
    }
}