namespace DTOs.Responses;

public class PaymentOverview
{
    public int PaymentId { get; set; }
    public string PaymentName { get; set; } = null!;
    public int Amount { get; set; }
    public DateTime PaymentTime { get; set; }
    public bool SinglePayment { get; set; } = true;
}