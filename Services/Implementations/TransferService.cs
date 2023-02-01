using DataRepository.Repositories.Interfaces;
using DTOs.Responses;
using Services.Interfaces;

namespace Services.Implementations;

public class TransferService : ITransferService
{
    private readonly ITransferRepository _transferRepository;

    public TransferService(ITransferRepository transferRepository)
    {
        _transferRepository = transferRepository;
    }
    
    public async Task RenewSubscriptions()
    {
        throw new NotImplementedException();
    }

    public async Task<ApiResultBase<List<PaymentOverview>>> GetUserPayments(int userId, int start = 0, int end = 10)
    {
        var payments = (await _transferRepository.GetTransfer(userId, start, end))
            .Select(transfer => new PaymentOverview()
            {
                PaymentId = transfer.Id,
                PaymentTime = transfer.TransferTime,
                Amount = (transfer.Subscription.Category.IsIncome ? 1 : -1) * transfer.Subscription.Amount,
                Category = transfer.Subscription.Category.Name,
                PaymentName = transfer.Subscription.Name
            })
            .ToList();

        return new ApiResultBase<List<PaymentOverview>>()
        {
            IsSuccess = true,
            Body = payments
        };
    }
}