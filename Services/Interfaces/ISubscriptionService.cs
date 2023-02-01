using DTOs.Requests;
using DTOs.Responses;

namespace Services.Interfaces;

public interface ISubscriptionService
{
    public Task<ApiResultBase<CategoriesOverview>> GetCategories(int userId); 
    public Task<ApiResultBase<SubscriptionsOverview>> GetSubscriptions(int userId);

    public ApiResultBase<Dictionary<int, string>> GetSubscriptionTypes();
    public Task<ApiResultBase<bool>> CancelSubscription(int userId, int subscriptionId);
    public Task<ApiResultBase<bool>> AddSubscription(int userId, SubscriptionCredentials subCredentials);
}