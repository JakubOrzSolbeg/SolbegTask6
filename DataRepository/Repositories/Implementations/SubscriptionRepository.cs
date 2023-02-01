using DataRepository.DbContext;
using DataRepository.Entities;
using DataRepository.Repositories.Interfaces;
using DTOs.Enums;
using Microsoft.EntityFrameworkCore;

namespace DataRepository.Repositories.Implementations;

public class SubscriptionRepository : Repository<Subscription>, ISubscriptionRepository
{
    public SubscriptionRepository(MainDbContext1 mainDbContext) : base(mainDbContext)
    {
    }

    public async Task<List<Subscription>> GetUserCurrentSubscriptions(int userId, bool isIncome = false)
    {
        return await MainDbContext.Subscriptions
            .Where(sub =>
                (sub.SubscriptionEnd == null || sub.SubscriptionEnd > DateTime.Now) &&
                sub.UserId.Equals(userId))
            .Include(sub => sub.Category)
            .Where(sub => sub.Category.IsIncome == isIncome)
            .ToListAsync();
    }

    public async Task<Dictionary<bool, List<Subscription>>> GetUserSubscriptions(int userId)
    {
        return await MainDbContext.Subscriptions
            .Where(sub =>
                (sub.SubscriptionEnd == null || sub.SubscriptionEnd.Value.Date >= DateTime.Now.Date) &&
                sub.SubscriptionType != SubscriptionType.Single &&
                sub.UserId.Equals(userId))
            .Include(sub => sub.Category)
            .GroupBy(sub => sub.Category.IsIncome)
            .ToDictionaryAsync(keySelector: grouping => grouping.Key, grouping => grouping.ToList());
    }
}