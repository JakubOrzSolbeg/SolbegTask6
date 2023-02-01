using DTOs.Responses;

namespace Services.Interfaces;

public interface ITransferService
{
    public Task RenewSubscriptions();
    public Task<ApiResultBase<List<PaymentOverview>>> GetUserPayments(int userId, int start = 0, int end = 10);
}