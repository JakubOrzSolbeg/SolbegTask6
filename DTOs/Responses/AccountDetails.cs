namespace DTOs.Responses;

public class AccountDetails
{
    public string Login { get; set; } = null!;
    public DateTime RegisterTime { get; set; }
    public string Permissions { get; set; } = null!;
    public int Balance { get; set; }
    public int TodayAccountChange { get; set; } = 0;
    public int? DailySpendingLimit { get; set; }
}