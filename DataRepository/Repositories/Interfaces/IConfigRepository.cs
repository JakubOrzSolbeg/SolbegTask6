using DataRepository.Entities;

namespace DataRepository.Repositories.Interfaces;

public interface IConfigRepository
{
    public Task<Config> GetConfig();
    public Task<Config> UpdateConfig(Config config);
}