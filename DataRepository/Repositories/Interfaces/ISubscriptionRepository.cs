using DataRepository.Entities;

namespace DataRepository.Repositories.Interfaces;

public interface ISubscriptionRepository : IRepository<Subscription>
{
    public Task<List<Subscription>> GetUserCurrentSubscriptions(int userId, bool isIncome = false);
    public Task<Dictionary<bool, List<Subscription>>> GetUserSubscriptions(int userId);
}