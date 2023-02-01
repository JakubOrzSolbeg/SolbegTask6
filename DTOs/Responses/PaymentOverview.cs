namespace DTOs.Responses;

public class PaymentOverview
{
    public int PaymentId { get; set; }
    public string PaymentName { get; set; } = null!;
    public int Amount { get; set; }
    public string Category { get; set; } = "Unknown";
    public DateTime PaymentTime { get; set; }

}