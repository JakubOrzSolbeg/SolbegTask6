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
            .SumAsync(transfer => transfer.Subscription.Amount);
    }
}