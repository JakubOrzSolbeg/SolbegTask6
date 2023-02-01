using DataRepository.Entities;

namespace DataRepository.Repositories.Interfaces;

public interface ITransferRepository : IRepository<Transfer>
{
    public Task<int> GetUserTodaySpending(int userId);
    public Task<int> GetCurrentUserAccount(int userId);
    public Task<List<Transfer>> GetTransfer(int userId, int startFrom = 0, int endTo = 10);

}