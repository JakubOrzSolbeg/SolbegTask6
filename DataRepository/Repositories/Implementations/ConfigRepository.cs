using DataRepository.DbContext;
using DataRepository.Entities;
using DataRepository.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataRepository.Repositories.Implementations;

public class ConfigRepository : IConfigRepository
{
    protected readonly MainDbContext1 MainDbContext;
    
    public ConfigRepository(MainDbContext1 mainDbContext)
    {
        MainDbContext = mainDbContext;
    }
    
    public async Task<Config> GetConfig()
    {
        return await MainDbContext.Config.FirstAsync(c => c.Index.Equals(1));
    }

    public async Task<Config> UpdateConfig(Config config)
    {
        MainDbContext.Config.Update(config);
        await MainDbContext.SaveChangesAsync();
        return config;
    }
}