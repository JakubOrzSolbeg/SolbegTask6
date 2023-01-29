using DataRepository.Entities;

namespace DataRepository.Repositories.Interfaces;

public interface ITransferRepository : IRepository<Transfer>
{
    public Task<int> GetUserTodaySpending(int userId);
}