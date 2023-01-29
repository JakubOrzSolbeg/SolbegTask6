using DataRepository.DbContext;
using DataRepository.Entities;
using DataRepository.Repositories.Implementations;
using DataRepository.Repositories.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace DataRepository;

public static class Setup
{
    public static IServiceCollection AddDataRepository(this IServiceCollection services)
    {
        services.AddDbContext<MainDbContext1>();
        services.AddScoped<IRepository<BankUser>, Repository<BankUser>>();
        services.AddScoped<IRepository<Category>, Repository<Category>>();
        services.AddScoped<IRepository<Subscription>, Repository<Subscription>>();
        services.AddScoped<ITransferRepository, TransferRepository>();
        services.AddScoped<IConfigRepository, ConfigRepository>();
        return services;
    }
}