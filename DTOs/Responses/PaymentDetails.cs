namespace DTOs.Responses;

public class PaymentDetails
{
    public PaymentOverview Overview { get; set; } = null!;
    public SubscriptionDetails SubscriptionDetails { get; set; } = null!;
}