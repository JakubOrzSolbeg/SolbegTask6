using DTOs.Enums;

namespace DTOs.Responses;

public class SubscriptionDetails
{
    public int SubscriptionId { get; set; }
    public string SubscriptionName { get; set; }
    public string CategoryName { get; set; }
    public string? Comment { get; set; }
    public SubscriptionType SubscriptionType { get; set; }
    public int Amount { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime? EndTime { get; set; }
}