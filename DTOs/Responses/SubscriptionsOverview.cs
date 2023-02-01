namespace DTOs.Responses;

public class SubscriptionsOverview
{
    public List<SubscriptionDetails> Incomes { get; set; } = null!;
    public List<SubscriptionDetails> Outcomes { get; set; } = null!;
}