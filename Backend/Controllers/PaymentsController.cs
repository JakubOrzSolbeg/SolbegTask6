using Backend.Utils;
using DataRepository.Entities;
using DataRepository.Repositories.Interfaces;
using DTOs.Requests;
using DTOs.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace Backend.Controllers;

[ApiController]
[Route("[controller]/[action]")]
[Authorize]
public class PaymentsController : ControllerBase
{
    private readonly ISubscriptionService _subscriptionsService;
    private readonly ITransferService _transferService;
    private readonly TokenUtil _tokenUtil;
    public PaymentsController(ISubscriptionService subscriptionService, ITransferService transferService, TokenUtil tokenUtil)
    {
        _subscriptionsService = subscriptionService;
        _transferService = transferService;
        _tokenUtil = tokenUtil;
    }

    [HttpPost]
    public async Task<ActionResult<ApiResultBase<bool>>> AddSubscription(
        SubscriptionCredentials subscriptionCredentials)
    {
        int userId = _tokenUtil.GetUserId(HttpContext);
        return await _subscriptionsService.AddSubscription(userId, subscriptionCredentials);
    }

    [HttpPost]
    public async Task<ActionResult<ApiResultBase<bool>>> CancelSubscription(int subscriptionId)
    {
        int userId = _tokenUtil.GetUserId(HttpContext);
        return await _subscriptionsService.CancelSubscription(userId, subscriptionId);
    }

    [HttpGet]
    public ActionResult<ApiResultBase<Dictionary<int, string>>> SubscriptionTypes()
    {
        return _subscriptionsService.GetSubscriptionTypes();
    }

    [HttpGet]
    public async Task<ActionResult<ApiResultBase<CategoriesOverview>>> PaymentCategories()
    {
        int userId = _tokenUtil.GetUserId(HttpContext);
        return await _subscriptionsService.GetCategories(userId);
    }

    [HttpGet]
    public async Task<ActionResult<ApiResultBase<List<PaymentOverview>>>> Payments(int? start, int? end)
    {
        int userId = _tokenUtil.GetUserId(HttpContext);
        return await _transferService.GetUserPayments(userId, start ?? 0, end ?? 10);
    }

    [HttpGet]
    public async Task<ActionResult<ApiResultBase<SubscriptionsOverview>>> Subscriptions()
    {
        int userId = _tokenUtil.GetUserId(HttpContext);
        return await _subscriptionsService.GetSubscriptions(userId);
    }

}