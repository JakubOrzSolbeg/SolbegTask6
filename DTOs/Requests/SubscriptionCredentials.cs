using System.ComponentModel.DataAnnotations;
using DTOs.Enums;

namespace DTOs.Requests;

public class SubscriptionCredentials
{
    [Required]
    public DateTime StartTime { get; set; } = DateTime.Now;
    public DateTime? EndTime { get; set; } = DateTime.Now;
    [Required]
    public SubscriptionType Type { get; set; } = SubscriptionType.Single;

    [Required] 
    public string SubscriptionName { get; set; } = "Unknown";
    [Required]
    public string CategoryName { get; set; } = "Unknown";

    [Required] 
    [Range(1, Int32.MaxValue)] 
    public int Amount { get; set; } = 1;
    [Required]
    public bool IsIncome { get; set; } = false;
    public string? Comment { get; set; }
}