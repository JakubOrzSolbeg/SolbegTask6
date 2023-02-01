using DataRepository.DbContext;
using DataRepository.Entities;
using DataRepository.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataRepository.Repositories.Implementations;

public class TransferRepository : Repository<Transfer>, ITransferRepository
{
    public TransferRepository(MainDbContext1 mainDbContext) : base(mainDbContext)
    {
    }

    public async Task<int> GetUserTodaySpending(int userId)
    {
        return await MainDbContext.Transfers.Where(
                transfer => transfer.UserId.Equals(userId) && transfer.TransferTime.Date.Equals(DateTime.Now.Date)
            )
            .Include(transfer => transfer.Subscription)
            .ThenInclude(subscription => subscription.Category)
            .SumAsync(transfer => (transfer.Subscription.Category.IsIncome? 1 : -1 ) * transfer.Subscription.Amount);
    }

    public async Task<int> GetCurrentUserAccount(int userId)
    {
        Transfer? lastTransfer = await MainDbContext.Transfers
            .OrderBy(transfer => transfer.Id)
            .LastOrDefaultAsync(transfer => transfer.UserId.Equals(userId));
        if (lastTransfer == null)
        {
            return 0;
        }

        return lastTransfer.AccountAfter;
    }

    public async Task<List<Transfer>> GetTransfer(int userId, int startFrom = 0, int endTo = 10)
    {
        return await MainDbContext.Transfers
            .Where(transfer => transfer.UserId.Equals(userId))
            .Skip(startFrom)
            .Take(endTo - startFrom)
            .Include(transfer => transfer.Subscription)
            .ThenInclude(subscription => subscription.Category )
            .ToListAsync();
    }
}