using System.Text.Json;
using DataRepository.Entities;
using DataRepository.Repositories.Interfaces;
using DTOs.Enums;
using DTOs.Requests;
using DTOs.Responses;
using Services.Interfaces;

namespace Services.Implementations;

public class SubscriptionService : ISubscriptionService
{
    private readonly ISubscriptionRepository _subscriptionRepository;
    private readonly IRepository<Category> _categoryRepository;
    private readonly IRepository<BankUser> _accountRepository;
    private readonly ITransferRepository _transferRepository;
    public SubscriptionService(ISubscriptionRepository subscriptionRepository, IRepository<Category> categoryRepository,
        IRepository<BankUser> accountRepository, ITransferRepository transferRepository)
    {
        _subscriptionRepository = subscriptionRepository;
        _categoryRepository = categoryRepository;
        _accountRepository = accountRepository;
        _transferRepository = transferRepository;
    }
    
    public async Task<ApiResultBase<CategoriesOverview>> GetCategories(int userId)
    {
        Console.WriteLine(userId);
        var categories = (await _categoryRepository.GetAllByPredicate(category =>
                (!category.IsCustom) || (category.IsCustom && category.CreatorId.Equals(userId))))
            .GroupBy(category => category.IsIncome)
            .ToDictionary(keySelector: grouping => grouping.Key,
                grouping => grouping.Select(category => category.Name).ToList());
        return new ApiResultBase<CategoriesOverview>()
        {
            IsSuccess = true,
            Body = new CategoriesOverview()
            {
                IncomeCategories = categories[true],
                OutcomeCategories = categories[false]
            }
        };
    }

    public async Task<ApiResultBase<SubscriptionsOverview>> GetSubscriptions(int userId)
    {
        var subDict = await _subscriptionRepository.GetUserSubscriptions(userId);
        SubscriptionsOverview result = new SubscriptionsOverview()
        {
            Incomes = subDict.ContainsKey(true)? subDict[true].Select(ConvertSubscriptionDetails).ToList() : new List<SubscriptionDetails>(),
            Outcomes = subDict.ContainsKey(false)? subDict[false].Select(ConvertSubscriptionDetails).ToList() : new List<SubscriptionDetails>()
        };
        return new ApiResultBase<SubscriptionsOverview>()
        {
            IsSuccess = true,
            Body = result
        };
    }

    public ApiResultBase<Dictionary<int, string>> GetSubscriptionTypes()
    {
        var types = Enum.GetValues(typeof(SubscriptionType))
            .Cast<SubscriptionType>()
            .ToDictionary( v => (int)v, type => type.ToString());
        return new ApiResultBase<Dictionary<int, string>>()
        {
            IsSuccess = true,
            Body = types
        };

    }

    private static SubscriptionDetails ConvertSubscriptionDetails(Subscription sub)
    {
        return new SubscriptionDetails()
        {
            SubscriptionId = sub.Id,
            StartTime = sub.SubscriptionStart,
            EndTime = sub.SubscriptionEnd,
            Amount = sub.Amount,
            SubscriptionName = sub.Name,
            SubscriptionType = sub.SubscriptionType,
            Comment = sub.Comment,
            CategoryName = sub.Category.Name
        };
    }

    public async Task<ApiResultBase<bool>> CancelSubscription(int userId, int subscriptionId)
    {
        Subscription? subscription = await _subscriptionRepository
            .GetByPredicate(sub => sub.UserId.Equals(userId) && sub.Id.Equals(subscriptionId));
        if (subscription == null)
        {
            return new ApiResultBase<bool>()
            {
                IsSuccess = false,
                Errors = "Subscription not found"
            };
        }
        
        subscription.SubscriptionEnd = DateTime.Today;
        await _subscriptionRepository.Update(subscription);
        return new ApiResultBase<bool>()
        {
            IsSuccess = true
        };
    }

    public async Task<ApiResultBase<bool>> AddSubscription(int userId, SubscriptionCredentials subCredentials)
    {
        BankUser? user = await _accountRepository.GetById(userId);
        if (user == null)
        {
            return new ApiResultBase<bool>()
            {
                IsSuccess = false,
                Errors = "User does not exists"
            };
        }
        
        if (user.SpendingLimit != null && !subCredentials.IsIncome)
        {
            int todaySpending = await _transferRepository.GetUserTodaySpending(userId);
            if (user.SpendingLimit - todaySpending < subCredentials.Amount)
            {
                return new ApiResultBase<bool>()
                {
                    IsSuccess = false,
                    Errors = "Today spending limit reached!"
                };
            }
        }

        int currentUserAccount = await _transferRepository.GetCurrentUserAccount(userId);

        if (currentUserAccount < subCredentials.Amount && !subCredentials.IsIncome)
        {
            return new ApiResultBase<bool>()
            {
                IsSuccess = false,
                Errors = "Not enough currency to afford this payment"
            };
        }
        
        Category? category = await _categoryRepository.GetByPredicate(cat =>
            (cat.IsCustom == false || cat.CreatorId.Equals(userId)) && 
            cat.IsIncome.Equals(subCredentials.IsIncome) && 
            cat.Name.Equals(subCredentials.CategoryName)
            );
        if (category == null)
        {
            category = new Category()
            {
                IsIncome = subCredentials.IsIncome,
                Name = subCredentials.CategoryName,
                CreatorId = userId,
                IsCustom = true
            };
            await _categoryRepository.Add(category);
        }
        
        Subscription newSubscription = new Subscription()
        {
            SubscriptionStart = DateTime.Now,
            SubscriptionEnd = subCredentials.EndTime,
            SubscriptionType = subCredentials.Type,
            Name = subCredentials.SubscriptionName,
            Category = category,
            Amount = subCredentials.Amount,
            UserId = userId,
            Comment = subCredentials.Comment
        };

        await _subscriptionRepository.Add(newSubscription);

        int payment = currentUserAccount +
                      ((subCredentials.IsIncome) ? subCredentials.Amount : -1 * subCredentials.Amount);
        
        Transfer firstPayment = new Transfer()
        {
            SubscriptionId = newSubscription.Id,
            UserId = userId,
            TransferTime = DateTime.Now,
            TransferType = subCredentials.Type == SubscriptionType.Single? TransferType.Single : TransferType.Subscription,
            AccountAfter = payment
        };
        await _transferRepository.Add(firstPayment);

        return new ApiResultBase<bool>()
        {
            IsSuccess = true,
        };
    }
}